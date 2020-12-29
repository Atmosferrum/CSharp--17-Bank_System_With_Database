using System;
using System.Threading;
using System.Threading.Tasks;

namespace NumberCounter
{
    class Program
    {
        private static int result = 0;
        private static int start = 1_000_000_000;
        private static int end = 2_000_000_000;
        private static int theNumber;
        private static int numberOfProcessors;

        private static DateTime now;

        static void Main(string[] args)
        {
            now = DateTime.Now;

            Console.WriteLine($"Processors : {Environment.ProcessorCount}");

            numberOfProcessors = Environment.ProcessorCount;

            theNumber = (end - start) / numberOfProcessors;

            Task<int>[] allAvailableTasks = new Task<int>[numberOfProcessors];

            for (int i = 0; i < numberOfProcessors; i++)
            {
                allAvailableTasks[i] = Task.Factory.StartNew(Sum, i);
            }

            Task.WaitAll(allAvailableTasks);

            for (int i = 0; i < numberOfProcessors; i++)
            {
                result += allAvailableTasks[i].Result;
            }

            Console.WriteLine(result);
            Console.WriteLine($"Total time is :{(DateTime.Now - now).TotalSeconds} !!!");

            Console.ReadKey();
        }

        static int Sum(object o)
        {
            Console.WriteLine($"Thread # {Thread.CurrentThread.ManagedThreadId}");

            int tempResult = 0;

            for (int i = start + (theNumber * (int)o); i < start + (theNumber * ((int)o + 1)); i++)
            {
                int[] numbers = new int[10];

                int number = i;

                int tempIntSum = 0;

                for (int j = 0; j < numbers.Length; j++)
                {
                    numbers[j] = number % 10;
                    number /= 10;
                    tempIntSum += numbers[j];
                }

                if (numbers[0] != 0)
                    if (tempIntSum % numbers[0] == 0)
                        ++tempResult;
            }

            return tempResult;
        }
    }
}
