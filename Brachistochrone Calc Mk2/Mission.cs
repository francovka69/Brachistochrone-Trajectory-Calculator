using System;
using System.Collections.Generic;
using System.Text;

namespace Brachistochrone_Calc_Mk2;
public record class Mission
{
    public string Ship { get; init; }

    public string Target { get; init; }

    public override string ToString()
    {
        return $"{Ship} -> {Target}";
    }
}