using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkForcs3;

internal class AlarmEventArgs:EventArgs
{
    public DateTime CurrentTime 
    {
        get;
    }
    public AlarmEventArgs(DateTime currentTime)
    {
        CurrentTime = currentTime;
    }
}
