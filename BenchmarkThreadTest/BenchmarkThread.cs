using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkThreadTest
{
    [MemoryDiagnoser]
    public class BenchmarkThread
    {
        [Params(1_000, 100_000, 100_000_000, 1_000_000_000)]
        public Int64 _numberToFor;

        [Benchmark]
        public async Task MethodWithoutThreads()
        {
            //If there is an await inside the ForTest method, it will free up resources to go to the next one (it will run as asynchronous),
            //if not, it will run as if it were synchronized.
            Task t1 = ForTest(_numberToFor);
            Task t2 = ForTest(_numberToFor);

            await Task.WhenAll(t1, t2); //Useful if within the methods above there is a process with await (releases resource)
        }

        [Benchmark]
        public async Task MethodWithThreads()
        {
            Task t1 = Task.Run(() => ForTest(_numberToFor));
            Task t2 = Task.Run(() => ForTest(_numberToFor));

            await Task.WhenAll(t1, t2);
        }

        public async Task ForTest(Int64 pNumber)
        {
            for (Int64 i = 0; i < pNumber; i++)
            {
              
            }
        }       
    }
}
