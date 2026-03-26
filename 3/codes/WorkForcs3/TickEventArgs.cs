using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkForcs3;

internal class TickEventArgs:EventArgs
{
    public DateTime CurrentTime 
    {
        get;
    }
    public TickEventArgs(DateTime currentTime)
    {
        CurrentTime = currentTime;
    }
}
