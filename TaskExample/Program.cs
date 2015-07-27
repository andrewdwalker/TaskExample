using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskExample
{
    // Fibonacci 
    /// <summary>
    /// ideas fix to use "yield" to generate multiple Fiboonacc
    /// </summary>
    class Program
    {
        static readonly double x = (1 + Math.Sqrt(5.0)) / 2.0;
        static readonly double y = (1 - Math.Sqrt(5.0)) / 2.0;
        static void Main(string[] args)
        {
            uint indexOfInterest = 100;
            Task<uint> FibonacciCalcTask = Task.Run<uint>(() => CalculateFib(indexOfInterest));
            Task<uint> FibonacciExactTask = Task.Run<uint>(() => FibonacciExact(indexOfInterest));
            try
            {
                Task.WaitAll(FibonacciCalcTask, FibonacciExactTask);

                Console.WriteLine("Calculated: " + FibonacciCalcTask.Result);
                Console.WriteLine("Exact: " + FibonacciExactTask.Result);
            }
            catch (AggregateException exception)
            {
                exception.Handle(eachException =>
                {
                    Console.WriteLine(
                        "ERROR: {0}",
                        eachException.Message);
                    return true;
                });
            }
        }

        static uint CalculateFib(uint index)
        {
            if (index == 0)
                return 0;
            if (index == 1)
                return 1;

            uint first = 0;
            uint second = 1;
            uint sum = 0;
            for (uint i = 2; i <= index; i++)
            {
                sum = checked(first + second);
                first = second;
                second = sum;
                
            }
            return sum;
        }

        static uint FibonacciExact(uint index)
        {
            return (uint)  ( (Math.Pow(x,index) - Math.Pow(y,index) ) / Math.Sqrt(5) );
        }

    }
}
