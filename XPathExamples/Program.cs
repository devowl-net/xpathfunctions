using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using XPathExamples.Common;
using XPathExamples.Examples;

namespace XPathExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            var examples = new ExampleBase[]
            {
                new IfElseExample(),
            };

            while (true)
            {
                var choosenExample = QuestionManager.Choose(examples, example => example.Name, "Please choose an example");
                Console.WriteLine();
                choosenExample.Execute();
                Console.ReadKey();
                Console.WriteLine();
            }
        }
    }
}
