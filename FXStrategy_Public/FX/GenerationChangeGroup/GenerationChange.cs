using FX.FitnessValue;
using FX.Operators;
using FX.Trees;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FX.GenerationChangeGroup
{
    public class GenerationChange
    {
        private Tree[] Population;
        private Tree[] Children;

        private static Random rand;
        private const double RateOfCross = 0.90;
        private const int TournamentSize = 5;
        private const int MaxRecord = 60 * 12;
        private const int MinRecord = 5 * 12;
        private const string PopulationFilePath = @"../../Log/population";

        //仮パラメータ
        int MaxHeight = 5;
        bool isFull = false;
        static bool isTenAfter = false;

        string buySign = "";
        DataTable buyTable = null;

        public Tree BestIndividual
        {
            get
            {
                Population = Population.OrderByDescending(e => e.EvalValue).ToArray();
                return Population[0];
            }
        }

        public GenerationChange(int nPop, bool isTenAfter = false, string buySign = "")
        {
            Population = new Tree[nPop];
            Children = new Tree[nPop * 2];
            rand = new Random();
            GenerationChange.isTenAfter = isTenAfter;
            this.buySign = buySign;
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialization(bool continued)
        {
            if (!continued || !File.Exists(PopulationFilePath))
            {
                for (int i = 0; i < Population.Length; )
                {
                    for (int height = 1; height <= MaxHeight; height++)
                    {
                        Population[i] = new Tree();
                        Population[i].Initialize(height, isFull, buySign);
                        i++;
                    }
                    isFull = !isFull;
                }
                Parallel.For(0, Population.Length, i =>
                {
                    CalcFitness(Population[i],buySign);
                });
                //for (int i = 0; i < Population.Length; i++)
                //{
                //    CalcFitness(Population[i], Population[i].SellSQLString == null);
                //}
            }
        }

        /// <summary>
        /// 通常の1ループ
        /// </summary>
        public void OneIteration()
        {
            GeneticOperation();

            var errorList = new SynchronizedCollection<int>();
            var option = new ParallelOptions();
            option.MaxDegreeOfParallelism = 8;
            Parallel.For(0, Children.Length,option, i =>
            {
                CalcFitness(Children[i], buySign);
            });

            foreach (var index in errorList)
                CalcFitness(Children[index], buySign);

            Selection();
        }

        /// <summary>
        /// |C|だけ個体を生成
        /// </summary>
        private void GeneticOperation()
        {
            var Parent = new Tree[2];
            Func<int, Tree> Selector = SelectByRoulette;
            for (var i = 0; i < Children.Length;)
            {
                double p = rand.NextDouble();
                Parent[0] = Selector(TournamentSize);


                if (p < RateOfCross && i < Children.Length - 1)
                {
                    // 交叉
                    Parent[1] = Selector(TournamentSize);

                    var children = GeneticOperator.Crossover(Parent, buySign);

                    for (var j = 0; j < children.Length; j++)
                    {
                        if (children[j].ToString() == Parent[j].ToString())
                            children[j] = GeneticOperator.Mutation(children[j], buySign);
                    }

                    Children[i] = children[0];
                    Children[i + 1] = children[1];
                    i += 2;
                }
                else
                {
                    // 突然変異
                    Children[i] = GeneticOperator.Mutation(Parent[0], buySign);
                    i++;
                }
            }
        }

        /// <summary>
        /// 各個体の評価 修正PFで評価
        /// </summary>
        private void CalcFitness(Tree individual, string buySQL)
        {
            var fitness = new Fitness();
            var sqlConn = @"Data Source=.\SQLEXPRESS;Initial Catalog=FXData;Integrated Security=True;Connection Timeout=30;";
            var db = new DataBase(sqlConn);
            var condition = " NextPrice * 3 >= TenMaxDD AND NextPrice * 2 > TenAfterPrice AND TenAfterPrice > NextPrice / 2";
            var afterPrice = "TenAfterPrice";
            var maxDD = "TenMaxDD";

            if (buySQL == "")
            {
                var gprSql = $"SELECT TOP {MaxRecord + 1} AsOfDate, NextPrice, {afterPrice}, {maxDD} FROM IndexData WHERE {individual.BuySQLString} AND {condition}";
                var table = db.ExecuteReader(gprSql);
                var tradeCount = table == null || table.Rows == null ? 0 : table.Rows.Count;
                //極端に少ないものは除外
                //極端に多いものは除外
                if (tradeCount < MinRecord || tradeCount > MaxRecord)
                {
                    individual.FitnessInfo = null;
                    return;
                }

                var win = table.AsEnumerable().Where(row => double.Parse(row["NextPrice"].ToString()) < double.Parse(row[afterPrice].ToString())).Count();
                fitness.TradeCount = tradeCount;
                fitness.WinCount = win;

                var startDate = new DateTime(2002, 1, 1);
                var endDate = new DateTime(2016, 12, 31);

                //BRACテスト
                while (startDate < endDate)
                {
                    var filterData = table.AsEnumerable().Where(row => DateTime.Parse(row["AsOfDate"].ToString()) < startDate || startDate.AddYears(1) < DateTime.Parse(row["AsOfDate"].ToString()));
                    try
                    {
                        var gain = filterData.Select(row => double.Parse(row[afterPrice].ToString()) / double.Parse(row["NextPrice"].ToString()) - 1.0).Average();
                        var pain = filterData.Select(row => 1.0 - double.Parse(row[maxDD].ToString()) / double.Parse(row["NextPrice"].ToString())).Average();
                        var tradeNum = filterData.Count();

                        fitness.TradeCountList.Add(tradeCount - tradeNum);
                        fitness.PFList.Add(gain / pain);
                        fitness.GainList.Add(gain);
                        fitness.PainList.Add(pain);
                        startDate = startDate.AddYears(1);
                    }
                    catch (Exception e)
                    {
                        individual.FitnessInfo = null;
                        return;
                    }
                }
            }
            else
            {
                if (buyTable == null)
                {
                    var gprSql = $"SELECT TOP {MaxRecord + 1} CodeNum, AsOfDate, NextPrice, {afterPrice}, {maxDD}  FROM StockData WHERE {buySign} AND {condition}";
                    buyTable = db.ExecuteReader(gprSql);
                }

                fitness.TradeCount = buyTable.Rows.Count;
                var win = 0;

                var startDate = new DateTime(2007, 1, 1);
                var endDate = new DateTime(2016, 12, 31);

                //AsOfDate,NextPrice,SellPrice,TenMaxDD
                //始値で買い，売りサインの次の日の始値で売る
                var list = new SynchronizedCollection<object[]>();
                //foreach(DataRow row in table.Rows)
                Parallel.ForEach(buyTable.AsEnumerable(), row =>
                {
                    var sql = $"SELECT TOP 1 AsOfDate, NextPrice FROM StockData WHERE CodeNum = '{row["CodeNum"].ToString()}' AND AsOfDate > '{DateTime.Parse(row["AsOfDate"].ToString())}' AND AsOfDate < '{DateTime.Parse(row["AsOfDate"].ToString()).AddDays(afterPrice == "TenAfterPrice" ? 20 : 10)}' AND {individual.SellSQLString} AND {condition} AND NowPrice > {row["NextPrice"].ToString()}";
                    var temp = db.ExecuteReader(sql);
                    sql = $"SELECT TOP 1 AsOfDate, NextPrice FROM StockData WHERE CodeNum = '{row["CodeNum"].ToString()}' AND AsOfDate > '{DateTime.Parse(row["AsOfDate"].ToString())}' AND AsOfDate < '{DateTime.Parse(row["AsOfDate"].ToString()).AddDays(afterPrice == "TenAfterPrice" ? 20 : 10)}' AND {individual.SellLCSQLString} AND {condition} AND NowPrice < {row["NextPrice"].ToString()}";
                    var temp2 = db.ExecuteReader(sql);

                    if ((temp == null || temp.Rows == null || temp.Rows.Count == 0) & (temp2 == null || temp2.Rows == null || temp2.Rows.Count == 0))
                    {
                        //データなし
                        list.Add(new object[] { DateTime.Parse(row["AsOfDate"].ToString()), double.Parse(row["NextPrice"].ToString()), double.Parse(row[afterPrice].ToString()), double.Parse(row[maxDD].ToString()) });
                    }
                    else
                    {
                        DataTable result = null;
                        if (temp == null || temp.Rows == null || temp.Rows.Count == 0)
                            result = temp2;
                        else if (temp2 == null || temp2.Rows == null || temp2.Rows.Count == 0)
                            result = temp;
                        else
                            result = DateTime.Parse(temp.Rows[0]["AsOfDate"].ToString()) < DateTime.Parse(temp2.Rows[0]["AsOfDate"].ToString()) ? temp : temp2;
                        //データあり
                        var sql2 = $"SELECT MIN(LowValue) FROM Code{row["CodeNum"].ToString()} WHERE Date > '{DateTime.Parse(row["AsOfDate"].ToString())}' AND Date <= '{DateTime.Parse(result.Rows[0]["AsOfDate"].ToString())}'";
                        var dd = db.ExecuteScalar(sql2, -1.0);
                        var sql3 = $"SELECT ShareUnitNum FROM ShareUnitTable WHERE CodeNum = '{row["CodeNum"].ToString()}'";
                        var unit = db.ExecuteScalar(sql3, -1);
                        if (dd == -1 || unit == -1)
                        {
                            list.Add(new object[] { DateTime.Parse(row["AsOfDate"].ToString()), double.Parse(row["NextPrice"].ToString()), double.Parse(row[afterPrice].ToString()), double.Parse(row[maxDD].ToString()) });
                            return;
                        }
                        list.Add(new object[] { DateTime.Parse(row["AsOfDate"].ToString()), double.Parse(row["NextPrice"].ToString()), double.Parse(result.Rows[0]["NextPrice"].ToString()), dd * unit });
                    }
                });

                //BRACテスト
                while (startDate < endDate)
                {
                    var filterData = list.Where(row => (DateTime)row[0] < startDate || startDate.AddYears(1) < (DateTime)row[0]);
                    try
                    {
                        var gain = filterData.Select(row => (double)row[2] / (double)row[1] - 1.0).Average();
                        var pain = filterData.Select(row => 1.0 - (double)row[3] / (double)row[1]).Average();
                        var tradeNum = filterData.Count();

                        fitness.TradeCountList.Add(fitness.TradeCount - tradeNum);
                        fitness.PFList.Add(gain / pain);
                        fitness.GainList.Add(gain);
                        fitness.PainList.Add(pain);
                        startDate = startDate.AddYears(1);
                    }
                    catch (Exception e)
                    {
                        individual.FitnessInfo = null;
                        return;
                    }
                }

                fitness.WinCount = list.Where(row => (double)row[1] < (double)row[2]).Count();
            }

            individual.FitnessInfo = fitness;
        }

        public Tree CalcFitness(string buySql, string sellSql = null)
        {
            var tree = new Tree();
            tree.BuySQLString = buySql;
            tree.SellSQLString = sellSql;
            CalcFitness(tree, buySign);
            return tree;
        }

        /// <summary>
        /// MGGで親子から|P|だけ次世代へ
        /// </summary>
        private void Selection()
        {
            Population = Population.Concat(Children).OrderByDescending(e => e.EvalValue).Take(Population.Length).ToArray();
        }


        private Tree SelectByTournament(int sampleSize)
        {
            Shuffle(sampleSize);
            var candidate = new Tree[sampleSize];
            for (int i = 0; i < candidate.Length; i++)           
                candidate[i] = Population[i];
         
            return candidate.OrderByDescending(e => e.EvalValue).First();
        }

        private Tree SelectByRoulette(int dummy)
        {
            var evalArray = Population.Where(tree => tree.EvalValue > 0 ).Select(tree => tree.EvalValue).OrderByDescending(d => d).ToArray();
            var sum = evalArray.Sum();
            var rand = new Random().NextDouble() * sum;
            for (int i = 0; i < evalArray.Length; i++)
            {
                rand -= evalArray.ElementAt(i);
                if (rand < 0)
                    return Population[i];
            }
            return Population.Last();
        }

        private void Shuffle(int size)
        { 
            for (int i = 0; i < size; i++)
            {
                int index = rand.Next(Population.Length - i) + i;
                Swap(i, index);
            }
        }

        /**
         * index1とindex2にある要素を入れ替える
         *
         * @param index1
         * @param index2
         */
        private void Swap(int index1, int index2)
        {
            var node1 = Population[index1];
            var node2 = Population[index2];
            Population[index2] = node1;
            Population[index1] = node2;
        }

        public void SavePopulation(int generation)
        {
            //SQL文のみを出力
            try
            {
                using (var sw = new StreamWriter(PopulationFilePath + generation + ".xml", false, new UTF8Encoding(false)))
                {
                    var serializer = new XmlSerializer(typeof(string[]), new Type[] { typeof(string) });
                    serializer.Serialize(sw, Population.Select(i => i.BuySQLString).ToArray());
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("SavePopulation:エラー");
            }
        }

        public void LoadPopulation()
        {
            if (File.Exists(PopulationFilePath))
            {
                using (var fs = new FileStream(PopulationFilePath, FileMode.Open, FileAccess.Read))
                {
                    var bf = new BinaryFormatter();
                    //読み込んで逆シリアル化する
                    Population = (Tree[])bf.Deserialize(fs);
                }
            }
        }

        internal void ShowBest(int generation)
        {
            Console.WriteLine($"{generation} :\n {BestIndividual.ToString()}");
            Console.WriteLine($"{generation} :\n {BestIndividual.FitnessInfo.ToString()}");
            using (var writer = new StreamWriter(@"./bestIndividual.txt", true, Encoding.UTF8))
            {
                writer.WriteLine($"{generation} :\n {BestIndividual.ToString()}");
            }
        }

        public void TestGenOperator()
        {
            GeneticOperation();
        }
    }
}
