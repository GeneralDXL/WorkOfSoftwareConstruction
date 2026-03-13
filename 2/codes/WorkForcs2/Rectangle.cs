using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkForcs2;

internal class Rectangle:Shape
{
    public Rectangle(double[] edges) : base(edges)
    {
        if (IsValid())
        {
            setType( "rectangle");
        }
        else
        {
            setType( "invalid shape");
        }
    }
    public override double GetArea()
    {
        double[] temps = GetEdges();
        return temps[0] * temps[1];
    }
    public override bool IsValid()
    {
        double[] temps = GetEdges();
        return temps.Length == 4
            && temps[0] == temps[2]
            && temps[1] == temps[3];
    }
}
