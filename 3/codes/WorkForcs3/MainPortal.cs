

namespace WorkForcs3;

public class MainPortal
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Enter the alarm time (HH:mm:ss):");
        string input = Console.ReadLine();
        DateTime alarmTime;
        while(!DateTime.TryParseExact(input, "HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out alarmTime))
        {
            Console.WriteLine("Invalid time format. Please use HH:mm:ss.");
            input= Console.ReadLine();
        }
        if(alarmTime < DateTime.Now)
        {
            alarmTime = alarmTime.AddDays(1);
            Console.WriteLine("Alarm time is in the past. Setting for the next day.");
        }

        Clock clock = new Clock(alarmTime);
        clock.Tick += (sender, e) =>
        {
            Console.WriteLine($"[Tick] Current time : {e.CurrentTime:HH:mm:ss}");
        };
        clock.Alarm += (sender, e) =>
        {
            Console.WriteLine($"[Alarm] Alarm! Now is the target time: {e.CurrentTime:HH:mm:ss}");
        };
        Console.WriteLine($"Clock started. Waiting for the alarm at {alarmTime:HH:mm:ss} . You can press any keys to stop this...");
        clock.Start();

        
       Console.ReadKey();
        if (clock.IsRunning())
        {
            Console.WriteLine("Alarm did not triggered, stopping the clock for user's mind...");
            clock.Stop();
        }
        Console.WriteLine("Goodbye!");
    }
}