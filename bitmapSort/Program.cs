using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitmapSort
{
    class Program
    {
        static void Main(string[] args)
        {
            var loader = new FakeLoader();
            var sw = new Stopwatch();
            var arrayToSort = loader.LoadBytes().ToArray();
            Console.Write("Array to be sorted : ");
            Console.WriteLine(string.Join(",", arrayToSort.Take(10)));

            Console.WriteLine();

            //bitmap sort
            Console.WriteLine("Performing bitmap sort");
            sw.Start();
            var bitmap = new BitArray(10000000);
            foreach (var loadedByte in arrayToSort)
            {
                bitmap[loadedByte] = true;
            }
            var sortedArray = new int[1000000];
            var pointerToSort = 0;
            for (int i = 0; i < 10000000; i++)
            {
                if (bitmap[i]) sortedArray[pointerToSort++] = i;
            }
            var timeTaken = sw.ElapsedMilliseconds;
            sw.Stop();
            Console.WriteLine($"Sort Complete in {timeTaken}");
            Console.WriteLine(string.Join(",", sortedArray.Take(10)));
            Console.WriteLine();

            Console.WriteLine("Now performing system based sort.");
            sw.Reset();
            sw.Start();
            var systemSorted = arrayToSort.OrderBy(a => a).ToArray();
            var timeTaken2 = sw.ElapsedMilliseconds;
            Console.WriteLine($"Sort Complete in {timeTaken2}");
            Console.WriteLine(string.Join(",", systemSorted.Take(10)));
            Console.WriteLine();

            Console.ReadLine();
        }
    }

    public class FakeLoader
    {
        public int[] LoadBytes()
        {
            //Note, random elements are only allowed to appear once in the resultset
            var random = new Random(DateTime.Now.Millisecond);
            var array = new int[1000000];
            var keepTrack = new BitArray(10000000);
            for (int i = 0; i < 1000000; i++)
            {
                int nextRandom;
                do
                {
                    nextRandom = random.Next(0, 10000000);
                } while (keepTrack[nextRandom]);
                keepTrack[nextRandom] = true;
                array[i] = nextRandom;
            }
            return array;
        }
    }
}