using Prometheus;

namespace Popcorn.Monitoring
{
    // QueryCounter will count the number of times the query function is invoked.
    // This will help us understand how many times a specific query is executed.
    public class PopcornCounter
    {
        public static string CounterName { get; set; }
        public static string CounterDescription { get; set; } = string.Empty;


        public static void Count()
        {
            Counter pCounter = Metrics.CreateCounter(CounterName, CounterDescription);
            pCounter.Inc();
        }
    }
}
