using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Terminal.Gui;

namespace Brachistochrone_Calc_Mk2.ModelClasses
{
    public class Calculator
    {
        public double g { get; set; }
        public double MinD { get; set; }
        public double AvgD { get; set; }
        public double MaxD { get; set; }

        public double ResultMinT;
        public double ResultAvgT;
        public double ResultMaxT;
        private double a { get; set; }
        private double c = 299792458;

        public Calculator(double g, double minD, double avgD, double maxD)
        {
            this.g = g;
            MinD = minD * 1000;
            AvgD = avgD * 1000;
            MaxD = maxD * 1000;
            a = g / 0.98;
        }

        public void ActualCalc()
        {
            ResultMinT = (2 * Math.Sqrt(MinD / a)) / 86400;
            ResultAvgT = (2 * Math.Sqrt(AvgD / a)) / 86400;
            ResultMaxT = (2 * Math.Sqrt(MaxD / a)) / 86400;
        }

        public override string ToString()
        {
            return $"the time it takes to complete a brachistochrone trip is distance divided by acceleration squared times 2";
        }
    }
}
