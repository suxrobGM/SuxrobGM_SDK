using System;
using System.Diagnostics;

namespace SuxrobGM.Sdk.Utils
{
    /// <summary>
    /// Utility that helps to measure the execution time of method
    /// </summary>
    public class Benchmark : IDisposable
    {
        private readonly Stopwatch timer = new Stopwatch();
        private readonly string benchmarkName;

        public Benchmark(string benchmarkName)
        {
            this.benchmarkName = benchmarkName;
            timer.Start();
        }

        public void Dispose()
        {
            timer.Stop();
            Console.WriteLine($"{benchmarkName} {timer.Elapsed}");
        }
    }
}
