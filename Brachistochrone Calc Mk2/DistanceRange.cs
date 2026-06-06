using System;
using System.Collections.Generic;
using System.Text;

namespace Brachistochrone_Calc_Mk2;

public struct DistanceRange
{
    public double Min { get; set; }
    public double Avg { get; set; }
    public double Max { get; set; }

    public override string ToString()
    {
        return $"Min: {Min} | Avg: {Avg} | Max: {Max}";
    }
}