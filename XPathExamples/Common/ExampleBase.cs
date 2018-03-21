using System.IO;
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
        /// Create <see cref="XPathDocument"/> from <see cref="xmlSource"/> string.
        /// </summary>
        /// <param name="xmlSource">Source xml string.</param>
        /// <returns><see cref="XPathDocument"/> instance.</returns>
        protected XPathDocument XPathDocumentFromString(string xmlSource)
        {
            using (var stringReader = new StringReader(xmlSource))
            {
                return new XPathDocument(stringReader);
            }
        }
    }
}