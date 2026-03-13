using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkForcs2;

internal class Circle:Shape
{
    public Circle(double[] edges) : base(edges) 
    {
        if (IsValid())
        {
            setType( "circle");
        }
        else
        {
            setType( "invalid shape");
        }
    }
    public override double GetArea()
    {
        return (GetEdges()[0]*GetEdges()[0])/(4*Math.PI); //这里edge表示边长，圆只有一条边
    }
    public override bool IsValid()
    {
        double[] temps = GetEdges();
        return temps.Length == 1 && temps[0] > 0;
    }
}
