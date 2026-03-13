using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkForcs2;

internal class Square: Shape
{
    public Square(double[] edges) : base(edges){
        if (IsValid())
        {
            setType( "square");
        }
        else
        {
            setType( "invalid shape");
        }
    }
    public override double GetArea()
    {
        return GetEdges()[0] * GetEdges()[0];
    }
    public override bool IsValid()
    {
        double[] temps=GetEdges();
        return temps.Length == 4
            && temps[0] == temps[1]
            && temps[0] == temps[2]
            && temps[0] == temps[3];
    }
}
