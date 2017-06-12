using System;
using System.Collections.Generic;
using System.Linq;

namespace FX.FitnessValue
{
    [Serializable]
    public class Fitness
    {
        public Fitness()
        {
            TradeCountList = new List<double>();
            PFList = new List<double>();
            GainList = new List<double>();
            PainList = new List<double>();
        }

        /// <summary>
        /// 引き分け考慮せず
        /// </summary>
        public int TradeCount { get; set; }

        public double TradePerMonth { get { return TradeCount / (12 * 15 * 1.0); } }

        public List<double> TradeCountList { get; set; }

        public int WinCount { get; set; }

        public int LoseCount { get { return TradeCount - WinCount; } }

        public double WinRatio { get { return (double)WinCount / TradeCount; } }

        public double PFRatio { get { return PFList.Average(); } }

        public List<double> PFList { get; set; }

        //1年あたりのPFの総和を1年あたりのトレード数で割る
        public double Gain { get { return GainList.Average(); } }

        public List<double> GainList { get; set; }

        public double Pain { get { return PainList.Average(); } }

        public List<double> PainList { get; set; }

        /// <summary>
        /// PFの標準偏差
        /// </summary>
        public double PFSigma { get { return Math.Sqrt(PFList.Select(pf => (pf - PFRatio) * (pf - PFRatio)).Sum() / PFList.Count); } }

        /// <summary>
        /// 最終的な評価値
        /// </summary>
        public double EvalValue { get { return PFRatio - 2 * PFSigma; } }

        public override string ToString()
        {
            return $@"TradeCount:{TradeCount},
                    Trade/M:{TradePerMonth},
                    WinRatio:{WinRatio},
                    PF:{PFRatio},
                    Sigma:{PFSigma},
                    Gain:{Gain},
                    Pain:{Pain}";
        }
    }
}
