using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Linq;

namespace Brachistochrone_Calc_Mk2.ModelClasses
{
    public class OverviewPrintout
    {
        public OverviewPrintout(double sDistance, double aDistance, double lDistance, double sTime, double aTime, double lTime, string ship, string target)
        {
            SDistance = sDistance;
            ADistance = aDistance;
            LDistance = lDistance;
            STime = sTime;
            ATime = aTime;
            LTime = lTime;
            Ship = ship;
            Target = target;
        }

        public double SDistance { get; set; }
        public double ADistance { get; set; }
        public double LDistance { get; set; }
        public double STime { get; set; }
        public double ATime { get; set; }
        public double LTime { get; set; }

        public string Ship { get; set; }
        public string Target { get; set; }

        public void Printout(string filename)
        {
            string report =
            $"""
            ========================================
                        Flight summary
            ========================================

            Ship:
            {Ship}

            Target:
            {Target}

            ----------------------------------------
            DISTANCES
            ----------------------------------------

            Shortest Distance:
            {SDistance:F2} km

            Average Distance:
            {ADistance:F2} km

            Longest Distance:
            {LDistance:F2} km

            ----------------------------------------
            TRAVEL TIMES
            ----------------------------------------

            Shortest Route Time:
            {STime:F2} days

            Average Route Time:
            {ATime:F2} days

            Longest Route Time:
            {LTime:F2} days

            ========================================
            Generated:
            {DateTime.Now}
            ========================================
            """;

            File.WriteAllText(filename, report);

            
        }

        public override string ToString()
        {
            return $"the ship {Ship} is going on a journey to {Target} that is {ADistance} far from earth";
        }
    }
}
