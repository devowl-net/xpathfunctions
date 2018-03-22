using System;
using System.IO;
using System.Linq;
using System.Xml.XPath;

namespace XPathExamples.Common
{
    /// <summary>
    /// Example base class.
    /// </summary>
    public abstract class ExampleBase
    {
        /// <summary>
        /// Name.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Execute example.
        /// </summary>
        public abstract void Execute();

        /// <summary>
        /// Print <see cref="XPathNodeIterator"/> value.
        /// </summary>
        /// <param name="result">Evaluation result.</param>
        protected void Print(object result)
        {
            if (result == null)
            {
                return;
            }

            var iterator = result as XPathNodeIterator;
            if (iterator != null)
            {
                while (iterator.MoveNext())
                {
                    var current = iterator.Current;
                    Console.WriteLine(current.Value);
                }
            }
            else
            {
                Console.WriteLine(result);
            }
        }

        /// <summary>
        /// Create <see cref="XPathDocument"/> from <see cref="xmlSource"/> string.
        /// </summary>
        /// <param name="xmlSource">Source xml string.</param>
        /// <returns><see cref="XPathDocument"/> instance.</returns>
        protected static XPathDocument XPathDocumentFromString(string xmlSource)
        {
            using (var stringReader = new StringReader(xmlSource))
            {
                return new XPathDocument(stringReader);
            }
        }
    }
}