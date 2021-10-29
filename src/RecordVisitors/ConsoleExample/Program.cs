
using System;

namespace ConsoleExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var r = new RecognizerPlugin.RecognizePlugins();
            foreach (var item in r.AllExtensions())
            {
                Console.WriteLine(item);
            }
        }
    }
}
