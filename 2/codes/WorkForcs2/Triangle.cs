using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkForcs2;

internal class Triangle: Shape
{
    public Triangle(double[] edges): base(edges)
    {
        if (IsValid())
        {
            setType( "triangle");
        }
        else
        {
            setType( "invalid shape");
        }
    }
    public override double GetArea()
    {
        double[] temps = GetEdges();
        double p = (temps[0] + temps[1] + temps[2]) / 2;
        return Math.Sqrt(p * (p - temps[0]) * (p - temps[1]) * (p - temps[2]));
    }
    public override bool IsValid()
    {
        double[] temps = GetEdges();
        return temps.Length == 3
            && temps[0] + temps[1] > temps[2]
            && temps[0] + temps[2] > temps[1]
            && temps[1] + temps[2] > temps[0];
    }
}
