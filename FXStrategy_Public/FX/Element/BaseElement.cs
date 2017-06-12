using FX.Element.Function;
using FX.Element.Terminal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;


namespace FX.Element
{
    [Serializable]
    public abstract class BaseElement
    {
        private static Type[] functions =
        {
            typeof(AndElement),
            typeof(OrElement),
        };

        private static Type[] indexes =
        {
            typeof(MASignElement),
            typeof(BollingerSignElement),
            typeof(ParabolicSignElement),
            typeof(PivotSignElement),
            typeof(ROCSignElement),
            typeof(PsychologicalSignElement),
            typeof(DeviationSignElement),
            typeof(RSISignElement),
            typeof(RCISignElement),
            typeof(perRSignElement),
            typeof(StochasticSignElement),
            typeof(MACDSignElement),
            typeof(DMISignElement),
            typeof(MATrendElement),
            typeof(DMITrendElement),
            typeof(ParabolicTrendElement),
            typeof(MACDTrendElement),
            typeof(HLBandElement),
            typeof(TwoMAElement),
            typeof(FourWeeksRuleElement)
        };


        private int height;
        public int Height
        {
            get
            {
                var heights = ChildrenNodes.Select(c => c.Height + 1).ToArray();
                return height = heights.Length == 0 ? 0 : heights.Max();
            }
        }

        private int nodeCount;
        public int NodeCount
        {
            get
            {
                var count = ChildrenNodes.Select(c => c.NodeCount).Sum();
                return nodeCount = count + 1;
            }
        }

        private static Random rand = new Random();

        public BaseElement[] ChildrenNodes { get; set; }

        public BaseElement ParentNode { get; set; }

        public bool IsTerminal { get; internal set; }

        public string SymbolString { get; internal set; }

        public abstract int ChildrenCount { get;}

        public void Initialize(int depth, bool isFull)
        {
            if (depth <= 1)
            {
                ChildrenNodes = ChildrenNodes.Select(c => c = CreateIndex()).ToArray();
                return;
            }

            if(isFull)
            {
                ChildrenNodes = ChildrenNodes.Select(c => { c = CreateLogic(); c.Initialize(depth - 1, isFull); return c; }).ToArray();
            }
            else
            {
                ChildrenNodes = ChildrenNodes.Select(c =>
                {
                    //終端1/3，非終端2/3ずつの割合
                    if (rand.Next(3) == 0)
                    {
                        c = CreateIndex();
                    }
                    else
                    {
                        c = CreateLogic();
                        c.Initialize(depth - 1, isFull);
                    }
                    return c;
                }).ToArray();                
            }     
        }

        public abstract string ToSql();

        public static IndexElement CreateIndex()
        {
            return Activator.CreateInstance(indexes[rand.Next(indexes.Length)], rand) as IndexElement;
        }

        public static LogicElement CreateLogic()
        {
            return Activator.CreateInstance(functions[rand.Next(functions.Length)]) as LogicElement;
        }

        public override bool Equals(object obj)
        {
            var elem = obj as BaseElement;
            if (elem == null)
                return false;

            return elem.ToString() == ToString();
        }

        public BaseElement Clone()
        {
            object clone = null;
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, this);
                stream.Position = 0;
                clone = formatter.Deserialize(stream);
            }
            return (BaseElement)clone;
        }
    }
}
