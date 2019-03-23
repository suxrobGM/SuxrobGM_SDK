using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SuxrobGM_SDK.Utils
{
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
