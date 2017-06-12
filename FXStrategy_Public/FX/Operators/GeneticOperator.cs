using FX.Element;
using FX.Trees;
using System;

namespace FX.Operators
{
    public class GeneticOperator
    {
        private static Random rand = new Random();

        /// <summary>
        /// 交叉メソッド．今回は親2体から子2体を創出する．
        /// </summary>
        /// <param name="parents"></param>
        /// <returns></returns>
        public static Tree[] Crossover(Tree[] parents, string buySign = "")
        {
            var offspring = new Tree[2];
            int idA, idB;
            //根ノードは交叉点としないようにする
            if (buySign == "")
            {
                BaseElement subBuyA = null;
                BaseElement subBuyB = null;

                // 条件を満たせばtrue
                bool offBuy1 = false, offBuy2 = false;
                do
                {
                    var cloneParent = new Tree[] { parents[0].Clone(), parents[1].Clone() };
                    idA = 1 + rand.Next(cloneParent[0].BuyNodeCount - 1);
                    idB = 1 + rand.Next(cloneParent[1].BuyNodeCount - 1);
                    if (offBuy1)
                    {
                        subBuyA = cloneParent[0].GetBuyNode(idA);
                        offspring[1] = cloneParent[1].ReplaceBuyNode(idB, subBuyA);
                        offspring[1].CacheClear();
                    }
                    else if (offBuy2)
                    {
                        subBuyB = cloneParent[1].GetBuyNode(idB);
                        offspring[0] = cloneParent[0].ReplaceBuyNode(idA, subBuyB);
                        offspring[0].CacheClear();
                    }
                    else
                    {
                        subBuyA = cloneParent[0].GetBuyNode(idA);
                        subBuyB = cloneParent[1].GetBuyNode(idB);
                        offspring[0] = cloneParent[0].ReplaceBuyNode(idA, subBuyB);
                        offspring[1] = cloneParent[1].ReplaceBuyNode(idB, subBuyA);
                        offspring[0].CacheClear();
                        offspring[1].CacheClear();
                    }
                    offBuy1 = !(offspring[0].BuyTreeHeight > 15 || offspring[0].BuyNodeCount > 150);
                    offBuy2 = !(offspring[1].BuyTreeHeight > 15 || offspring[1].BuyNodeCount > 150);
                }
                // ノード制限以上の個体は次世代に引き継がない
                while (!offBuy1 || !offBuy2);
            }
            else
            {
                //根ノードは交叉点としないようにする
                BaseElement subSellA = null;
                BaseElement subSellB = null;
                // 条件を満たせばtrue
                bool offSell1 = false, offSell2 = false;
                do
                {
                    var cloneParent = new Tree[] { parents[0].Clone(), parents[1].Clone() };
                    idA = 1 + rand.Next(cloneParent[0].SellNodeCount - 1);
                    idB = 1 + rand.Next(cloneParent[1].SellNodeCount - 1);
                    if (offSell1)
                    {
                        subSellA = cloneParent[0].GetSellNode(idA);
                        offspring[1] = cloneParent[1].ReplaceSellNode(idB, subSellA);
                        offspring[1].CacheClear();
                    }
                    else if (offSell2)
                    {
                        subSellB = cloneParent[1].GetSellNode(idB);
                        offspring[0] = cloneParent[0].ReplaceSellNode(idA, subSellB);
                        offspring[0].CacheClear();
                    }
                    else
                    {
                        subSellA = cloneParent[0].GetSellNode(idA);
                        subSellB = cloneParent[1].GetSellNode(idB);
                        offspring[0] = cloneParent[0].ReplaceSellNode(idA, subSellB);
                        offspring[1] = cloneParent[1].ReplaceSellNode(idB, subSellA);
                        offspring[0].CacheClear();
                        offspring[1].CacheClear();
                    }
                    offSell1 = !(offspring[0].SellTreeHeight > 15 || offspring[0].SellNodeCount > 150);
                    offSell2 = !(offspring[1].SellTreeHeight > 15 || offspring[1].SellNodeCount > 150);
                }
                // ノード制限以上の個体は次世代に引き継がない
                while (!offSell1 || !offSell2);

                offSell1 = false; offSell2 = false;
                do
                {
                    var cloneParent = new Tree[] { offspring[0].Clone(), offspring[1].Clone() };
                    idA = 1 + rand.Next(cloneParent[0].SellLCNodeCount - 1);
                    idB = 1 + rand.Next(cloneParent[1].SellLCNodeCount - 1);
                    if (offSell1)
                    {
                        subSellA = cloneParent[0].GetSellLCNode(idA);
                        offspring[1] = cloneParent[1].ReplaceSellLCNode(idB, subSellA);
                        offspring[1].CacheClear();
                    }
                    else if (offSell2)
                    {
                        subSellB = cloneParent[1].GetSellLCNode(idB);
                        offspring[0] = cloneParent[0].ReplaceSellLCNode(idA, subSellB);
                        offspring[0].CacheClear();
                    }
                    else
                    {
                        subSellA = cloneParent[0].GetSellLCNode(idA);
                        subSellB = cloneParent[1].GetSellLCNode(idB);
                        offspring[0] = cloneParent[0].ReplaceSellLCNode(idA, subSellB);
                        offspring[1] = cloneParent[1].ReplaceSellLCNode(idB, subSellA);
                        offspring[0].CacheClear();
                        offspring[1].CacheClear();
                    }
                    offSell1 = !(offspring[0].SellLCTreeHeight > 15 || offspring[0].SellLCNodeCount > 150);
                    offSell2 = !(offspring[1].SellLCTreeHeight > 15 || offspring[1].SellLCNodeCount > 150);
                }
                // ノード制限以上の個体は次世代に引き継がない
                while (!offSell1 || !offSell2);
            }

            return offspring;
        }

        /// <summary>
        /// 突然変異メソッド．実装上の問題で必ず非終端記号を突然変異点とする．
        /// </summary>
        /// <param name="tree"></param>
        /// <returns></returns>
        public static Tree Mutation(Tree tree, string buySign = "")
        {
            Tree newTree = null;
            if (buySign == "")
            {
                //Buy
                do
                {
                    newTree = tree.Clone();
                    var node = newTree.GetBuyNode(rand.Next(tree.BuyNodeCount));
                    while (node.IsTerminal)
                    {
                        node = newTree.GetBuyNode(rand.Next(tree.BuyNodeCount));
                    }
                    node.Initialize(5, false);
                    newTree.CacheClear();
                }
                while (newTree.BuyTreeHeight > 15 || newTree.BuyNodeCount > 150);
            }
            else
            {
                //Sell
                do
                {
                    newTree = tree.Clone();
                    var node = newTree.GetSellNode(rand.Next(tree.SellNodeCount));
                    while (node.IsTerminal)
                    {
                        node = newTree.GetSellNode(rand.Next(tree.SellNodeCount));
                    }
                    node.Initialize(5, false);
                    newTree.CacheClear();
                }
                while (newTree.SellTreeHeight > 15 || newTree.SellNodeCount > 150);

                var tempTree = newTree.Clone();
                do
                {
                    newTree = tempTree.Clone();
                    var node = newTree.GetSellLCNode(rand.Next(tree.SellLCNodeCount));
                    while (node.IsTerminal)
                    {
                        node = newTree.GetSellLCNode(rand.Next(tree.SellLCNodeCount));
                    }
                    node.Initialize(5, false);
                    newTree.CacheClear();
                }
                while (newTree.SellLCTreeHeight > 15 || newTree.SellLCNodeCount > 150);
            }
            return newTree;
        }
    }
}
