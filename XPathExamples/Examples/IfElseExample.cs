using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;

using XPathExamples.Common;

namespace XPathExamples.Examples
{
    /// <summary>
    /// if-else function example.
    /// </summary>
    public class IfElseExample : ExampleBase
    {
        /// <inheritdoc/>
        public override string Name => @"""if-else"" conditional function";

        /// <inheritdoc/>
        public override void Execute()
        {
            var sourceXml = @"
                <?xml version=""1.0""?>
                <catalog>
                    <book id=""bk101"">
                        <author gender=""male"">Gambardella, Matthew</author>
                        <title>XML Developer's Guide</title>
                        <genre>Computer</genre>
                        <price>44.95</price>
                        <publish_date>2000-10-01</publish_date>
                        <description>An in-depth look at creating applications 
                        with XML.</description>
                    </book>
                </catalog>";

            var document = XPathDocumentFromString(sourceXml);
            var navigator = document.CreateNavigator();
            var query = @"
                if-else
                (
                    /catalog/book/autor/@gender = 'male' and /catalog/book/genre/text() = 'Computer',
                    '[true]  Autor is male',
                    '[false] Autor is female'
                )";

            navigator.Select(query, CreateManager());
        }

        private IXmlNamespaceResolver CreateManager()
        {
            
        }
    }
}