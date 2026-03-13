using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkForcs2;

internal abstract class Shape
{
    private double[] edges;
    private string type="invalid shape";
    public Shape(double[] edges)
    {
        this.edges = edges;
    }
    public abstract double GetArea();
    public abstract bool IsValid();
    public double[] GetEdges()
    {
        return edges;
    }
    public string GetType()
    {
        return type;
    }

    protected void setType(string type)
    {
        this.type = type;
    }
}
