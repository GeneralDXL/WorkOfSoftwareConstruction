using System;
using System.Threading;

namespace WorkForcs3;

internal class Clock
{
    private Thread thread;
    private volatile bool isRunning;
    private int tickInterval;

    private DateTime AlarmTime { get; set; }
    public int TickInterval
    {
        get => tickInterval;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Tick interval must be greater than zero.");
            tickInterval = value;
        }
    }

    public event EventHandler<TickEventArgs> Tick;
    public event EventHandler<AlarmEventArgs> Alarm;

    public Clock(DateTime alarmTime,int tickInterval=1000)// Default to 1 second 1 tick
    {
        TickInterval = tickInterval; 
        AlarmTime= alarmTime;
    }

    public void Start()
    {
        if (isRunning)
            return;
        isRunning = true;
        thread = new Thread(Run);
        thread.IsBackground = true;
        thread.Start();
    }

    public void Stop()
    {
        isRunning = false;
        thread?.Join(TimeSpan.FromSeconds(1));
        Console.WriteLine("Clock stopped . Press any keys to exit...");
    }

    public bool IsRunning()
    {
        return isRunning;
    }

    private void Run()
    {   while (isRunning)
        {
            DateTime currentTime = DateTime.Now;
            Tick?.Invoke(this, new TickEventArgs(currentTime));
            if ( currentTime >= AlarmTime)
            {
                Alarm?.Invoke(this, new AlarmEventArgs(currentTime));
                isRunning = false;
                break;
            }
            Thread.Sleep(TickInterval);
        }
        if(!isRunning)
            Stop();
    }
}
