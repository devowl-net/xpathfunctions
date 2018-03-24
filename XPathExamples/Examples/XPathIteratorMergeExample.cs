using System.Xml.XPath;

using XPathExamples.Common;

namespace XPathExamples.Examples
{
    /// <summary>
    /// <see cref="XPathNodeIterator"/> merged select results.
    /// </summary>
    public class XPathIteratorMergeExample : ExampleBase
    {
        /// <inheritdoc/>
        public override string Name => @"""XPathNodeIterator"" merge several query results";

        /// <inheritdoc/>
        public override void Execute()
        {
            var sourceXml = @"<?xml version=""1.0""?>
<catalog>
    <book id=""bk101"">
        <author gender=""male"">Gambardella, Matthew</author>
        <title>XML Developer's Guide</title>
        <genre>Computer</genre>
        <price>44.95</price>
    </book>
    <book id=""bk102"">
        <author gender=""female"">Olga</author>
        <title>C++ Developer's Guide</title>
        <genre>Computer</genre>
        <price>55.25</price>
    </book>
    <book id=""bk103"">
        <author gender=""female"">Mike</author>
        <title>C# Developer's Guide</title>
        <genre>Computer</genre>
        <price>23.55</price>
    </book>
</catalog>";

            var document = XPathDocumentFromString(sourceXml);
            var navigator = document.CreateNavigator();
            var query1 = @"/catalog/book[@id = 'bk101']/author/text()";
            var query2 = @"/catalog/book[@id != 'bk101']/author/text()";

            Print("[XML]", sourceXml);
            Print("[Query1]", query1);
            Print("[Query2]", query2);

            Print("Merging", "...");

            var result1 = navigator.Select(query1);
            var result2 = navigator.Select(query2);
            var merged = new XPathNodeIteratorCombine(result1, result2);
            Print("[Result]", merged);
        }
    }
}