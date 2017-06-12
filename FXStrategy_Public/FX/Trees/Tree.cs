using FX.Element;
using FX.FitnessValue;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace FX.Trees
{
    [Serializable]
    public class Tree
    {
        private const double Error = -100;

        public Tree()
        {
            buyTreeHeight = new Cache<int>();
            buyCount = new Cache<int>();
            buySql = new Cache<string>();

            sellTreeHeight = new Cache<int>();
            sellCount = new Cache<int>();
            sellSql = new Cache<string>();

            sellLCTreeHeight = new Cache<int>();
            sellLCCount = new Cache<int>();
            sellLCSql = new Cache<string>();
        }
        
        public Tree Clone()
        {
            object clone = null;
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, this);
                stream.Position = 0;
                clone = formatter.Deserialize(stream);
            }
            return (Tree)clone;
        }

        #region 評価値
        public double EvalValue { get { return FitnessInfo?.EvalValue ?? Error; }}

        public Fitness FitnessInfo { get; set; }
        #endregion

        #region 買いサイン
        private BaseElement BuyRoot { get; set; }

        private Cache<int> buyTreeHeight;
        public int BuyTreeHeight
        {
            get
            {
                if (buyTreeHeight.IsNotCached)
                    buyTreeHeight.Value = BuyRoot.Height;
                return buyTreeHeight.Value;
            }
        }

        private Cache<int> buyCount;
        public int BuyNodeCount
        {
            get
            {
                if (buyCount.IsNotCached)
                    buyCount.Value = BuyRoot.NodeCount;
                return buyCount.Value;
            }
        }

        private Cache<string> buySql;
        public string BuySQLString
        {
            get
            {
                if (buySql.IsNotCached)
                    buySql.Value = BuyRoot.ToSql();
                return buySql.Value;
            }
            internal set { buySql.Value = value; }
        }

        public BaseElement GetBuyNode(int index)
        {
            if (index == 0)
                return BuyRoot;

            BaseElement node;
            var queue = new Queue<BaseElement>();
            for (int i = 0; i < BuyRoot.ChildrenCount; i++)
            {
                queue.Enqueue(BuyRoot.ChildrenNodes[i]);
            }

            while (queue.Count != 0)
            {
                node = queue.Dequeue();
                index--;
                if (index == 0)
                    return node;

                for (int i = 0; i < node.ChildrenCount; i++)
                {
                    queue.Enqueue(node.ChildrenNodes[i]);
                }
            }

            return null;
        }

        public Tree ReplaceBuyNode(int index, BaseElement subTree)
        {
            var newTree = Clone();
            var queue = new Queue<BaseElement>();
            // 特殊処理
            if (index == 1 || index == 2)
            {
                newTree.BuyRoot.ChildrenNodes[index - 1] = subTree.Clone();
                // parentNodeの参照削除
                queue.Clear();
                queue.Enqueue(newTree.BuyRoot.ChildrenNodes[index - 1]);
                while (queue.Count != 0)
                {
                    var node = queue.Dequeue();
                    node.ParentNode = null;
                    for (int j = 0; j < node.ChildrenCount; j++)
                    {
                        queue.Enqueue(node.ChildrenNodes[j]);
                    }
                }
                return newTree;
            }

            var root = newTree.BuyRoot;            
            for (int i = 0; i < root.ChildrenCount; i++)
            {
                root.ChildrenNodes[i].ParentNode = BuyRoot;
                queue.Enqueue(root.ChildrenNodes[i]);
            }
            while (queue.Count != 0)
            {
                var tempNode = queue.Dequeue();
                index--;
                if (index == 0)
                {
                    BaseElement parent = tempNode.ParentNode;
                    for (int i = 0; i < parent.ChildrenCount; i++)
                    {
                        if (parent.ChildrenNodes[i].Equals(tempNode))
                        {
                            parent.ChildrenNodes[i] = subTree.Clone();
                            newTree.BuyRoot = root;
                            // parentNodeの参照削除
                            queue.Clear();
                            queue.Enqueue(newTree.BuyRoot);
                            while(queue.Count != 0)
                            {
                                var node = queue.Dequeue();
                                node.ParentNode = null;
                                for (int j = 0; j < node.ChildrenCount; j++)
                                {
                                    queue.Enqueue(node.ChildrenNodes[j]);
                                }
                            }
                            return newTree;
                        }
                    }
                }
                for (int i = 0; i < tempNode.ChildrenCount; i++)
                {
                    tempNode.ChildrenNodes[i].ParentNode = tempNode;
                    queue.Enqueue(tempNode.ChildrenNodes[i]);
                }
            }
            return null;
        }
        #endregion

        #region 売りサイン(利確版)
        private BaseElement SellRoot { get; set; }

        private Cache<int> sellTreeHeight;
        public int SellTreeHeight
        {
            get
            {
                if (sellTreeHeight.IsNotCached)
                    sellTreeHeight.Value = SellRoot == null ? 0 : SellRoot.Height;
                return sellTreeHeight.Value;
            }
        }

        private Cache<int> sellCount;
        public int SellNodeCount
        {
            get
            {
                if (sellCount.IsNotCached)
                    sellCount.Value = SellRoot == null ? 0 : SellRoot.NodeCount;
                return sellCount.Value;
            }
        }

        private Cache<string> sellSql;
        public string SellSQLString
        {
            get
            {
                if (sellSql.IsNotCached)
                    sellSql.Value = SellRoot?.ToSql();
                return sellSql.Value;
            }
            internal set { sellSql.Value = value; }
        }

        public BaseElement GetSellNode(int index)
        {
            if (index == 0)
                return SellRoot;

            BaseElement node;
            var queue = new Queue<BaseElement>();
            for (int i = 0; i < SellRoot.ChildrenCount; i++)
            {
                queue.Enqueue(SellRoot.ChildrenNodes[i]);
            }

            while (queue.Count != 0)
            {
                node = queue.Dequeue();
                index--;
                if (index == 0)
                    return node;

                for (int i = 0; i < node.ChildrenCount; i++)
                {
                    queue.Enqueue(node.ChildrenNodes[i]);
                }
            }

            return null;
        }

        public Tree ReplaceSellNode(int index, BaseElement subTree)
        {
            var newTree = Clone();
            // 特殊処理
            if (index == 1 || index == 2)
            {
                newTree.SellRoot.ChildrenNodes[index - 1] = subTree;
                return newTree;
            }

            var root = newTree.SellRoot;
            var queue = new Queue<BaseElement>();
            for (int i = 0; i < root.ChildrenCount; i++)
            {
                root.ChildrenNodes[i].ParentNode = SellRoot;
                queue.Enqueue(root.ChildrenNodes[i]);
            }
            while (queue.Count != 0)
            {
                var tempNode = queue.Dequeue();
                index--;
                if (index == 0)
                {
                    BaseElement parent = tempNode.ParentNode;
                    for (int i = 0; i < parent.ChildrenCount; i++)
                    {
                        if (parent.ChildrenNodes[i].Equals(tempNode))
                        {
                            parent.ChildrenNodes[i] = subTree.Clone();
                            newTree.SellRoot = root;
                            return newTree;
                        }
                    }
                }
                for (int i = 0; i < tempNode.ChildrenCount; i++)
                {
                    tempNode.ChildrenNodes[i].ParentNode = tempNode;
                    queue.Enqueue(tempNode.ChildrenNodes[i]);
                }
            }
            return null;
        }
        #endregion

        #region 売りサイン(損切り版)
        private BaseElement SellLCRoot { get; set; }

        private Cache<int> sellLCTreeHeight;
        public int SellLCTreeHeight
        {
            get
            {
                if (sellLCTreeHeight.IsNotCached)
                    sellLCTreeHeight.Value = SellLCRoot == null ? 0 : SellLCRoot.Height;
                return sellLCTreeHeight.Value;
            }
        }

        private Cache<int> sellLCCount;
        public int SellLCNodeCount
        {
            get
            {
                if (sellLCCount.IsNotCached)
                    sellLCCount.Value = SellLCRoot == null ? 0 : SellLCRoot.NodeCount;
                return sellLCCount.Value;
            }
        }

        private Cache<string> sellLCSql;
        public string SellLCSQLString
        {
            get
            {
                if (sellLCSql.IsNotCached)
                    sellLCSql.Value = SellLCRoot?.ToSql();
                return sellLCSql.Value;
            }
            internal set { sellLCSql.Value = value; }
        }

        public BaseElement GetSellLCNode(int index)
        {
            if (index == 0)
                return SellLCRoot;

            BaseElement node;
            var queue = new Queue<BaseElement>();
            for (int i = 0; i < SellLCRoot.ChildrenCount; i++)
            {
                queue.Enqueue(SellLCRoot.ChildrenNodes[i]);
            }

            while (queue.Count != 0)
            {
                node = queue.Dequeue();
                index--;
                if (index == 0)
                    return node;

                for (int i = 0; i < node.ChildrenCount; i++)
                {
                    queue.Enqueue(node.ChildrenNodes[i]);
                }
            }

            return null;
        }

        public Tree ReplaceSellLCNode(int index, BaseElement subTree)
        {
            var newTree = Clone();
            // 特殊処理
            if (index == 1 || index == 2)
            {
                newTree.SellLCRoot.ChildrenNodes[index - 1] = subTree;
                return newTree;
            }

            var root = newTree.SellLCRoot;
            var queue = new Queue<BaseElement>();
            for (int i = 0; i < root.ChildrenCount; i++)
            {
                root.ChildrenNodes[i].ParentNode = SellLCRoot;
                queue.Enqueue(root.ChildrenNodes[i]);
            }
            while (queue.Count != 0)
            {
                var tempNode = queue.Dequeue();
                index--;
                if (index == 0)
                {
                    BaseElement parent = tempNode.ParentNode;
                    for (int i = 0; i < parent.ChildrenCount; i++)
                    {
                        if (parent.ChildrenNodes[i].Equals(tempNode))
                        {
                            parent.ChildrenNodes[i] = subTree.Clone();
                            newTree.SellLCRoot = root;
                            return newTree;
                        }
                    }
                }
                for (int i = 0; i < tempNode.ChildrenCount; i++)
                {
                    tempNode.ChildrenNodes[i].ParentNode = tempNode;
                    queue.Enqueue(tempNode.ChildrenNodes[i]);
                }
            }
            return null;
        }
        #endregion

        public void CacheClear()
        {
            buyTreeHeight.Clear();
            buyCount.Clear();
            buySql.Clear();

            sellTreeHeight.Clear();
            sellCount.Clear();
            sellSql.Clear();

            sellLCTreeHeight.Clear();
            sellLCCount.Clear();
            sellLCSql.Clear();
        }

        public void Initialize(int depth, bool isFull, string buySign = "")
        {
            if (buySign == "")
            {
                BuyRoot = BaseElement.CreateLogic();
                BuyRoot.Initialize(depth - 1, isFull);
            }
            else
            {
                SellRoot = BaseElement.CreateLogic();
                SellRoot.Initialize(depth - 1, isFull);

                SellLCRoot = BaseElement.CreateLogic();
                SellLCRoot.Initialize(depth - 1, isFull);
            }
        }

        /// <summary>
        /// 個体が持つ各木が表す論理式を表示するメソッド
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return BuyRoot?.ToString() + Environment.NewLine + SellRoot?.ToString() + Environment.NewLine + SellLCRoot?.ToString();
        }

        /// <summary>
        /// 個体が持つBuy木構造を表示するメソッド
        /// </summary>
        /// <returns></returns>
        public string ToBuyTreeString()
        {
            string treeString = "";
            var root = BuyRoot;
            var rootHeight = BuyTreeHeight;
            var stack = new Stack<BaseElement>();
            var heightMap = new Dictionary<BaseElement, int>();
            heightMap.Add(root, 0);
            stack.Push(root);
            while(stack.Count != 0)
            {
                var node = stack.Pop();
                var dist = 0;
                heightMap.TryGetValue(node, out dist);
                treeString += repeat(dist) + node.SymbolString + "\n";

                for (int i = 0; i < node.ChildrenCount; i++)
                {
                    heightMap.Add(node.ChildrenNodes[i], dist + 1);
                    stack.Push(node.ChildrenNodes[i]);
                }
            }

            return treeString;
        }

        /// <summary>
        /// 個体が持つSell木構造を表示するメソッド
        /// </summary>
        /// <returns></returns>
        public string ToSellTreeString()
        {
            string treeString = "";
            var root = SellRoot;
            var rootHeight = SellTreeHeight;
            var stack = new Stack<BaseElement>();
            var heightMap = new Dictionary<BaseElement, int>();
            heightMap.Add(root, 0);
            stack.Push(root);
            while (stack.Count != 0)
            {
                var node = stack.Pop();
                var dist = 0;
                heightMap.TryGetValue(node, out dist);
                treeString += repeat(dist) + node.SymbolString + "\n";

                for (int i = 0; i < node.ChildrenCount; i++)
                {
                    heightMap.Add(node.ChildrenNodes[i], dist + 1);
                    stack.Push(node.ChildrenNodes[i]);
                }
            }

            return treeString;
        }

        private static string repeat(int count)
        {
            string result = "";
            for (int i = 0; i < count; i++)
            {
                if (i != count - 1)
                    result += "　　　　";
                else
                    result += "└ーーー";
            }
            return result;
        }
    }
}
