using System;

using XPathExamples.Common;
using XPathExamples.Examples;

namespace XPathExamples
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var examples = new ExampleBase[]
            {
                new IfElseExample(),
                new StringFormatExample(),
                new CurrentExample(),
                new XPathIteratorMergeExample(), 
            };

            while (true)
            {
                var choosenExample = QuestionManager.Choose(
                    examples,
                    example => example.Name,
                    "Please choose an example");
                Console.WriteLine();
                choosenExample.Execute();
                Console.ReadKey();
                Console.WriteLine();
            }
        }
    }
}