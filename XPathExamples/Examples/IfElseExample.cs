using System;
using System.Collections.Generic;
using System.Xml.XPath;
using System.Xml.Xsl;

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
            var sourceXml = 
@"<?xml version=""1.0""?>
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
    /catalog/book/author/@gender = 'female' and /catalog/book/genre/text() = 'Computer',
    '[true]  author is male',
    if-else
    (
        /catalog/book/price > 20,
        '[true] too expansive',
        '[false] cheap price'
    )
)";
            SysConsole.WriteInfoLine("[XML]");
            Print(sourceXml);
            SysConsole.WriteInfoLine("[Query]");
            Print(query);
            SysConsole.WriteInfoLine("[Result]");
            
            var result = navigator.Evaluate(query, new IfElseXsltContext());
            Print(result);
        }
    }

    /// <summary>
    /// Custom XsltContext.
    /// </summary>
    public class IfElseXsltContext : XsltContext
    {
        private readonly IDictionary<string, IXsltContextFunction> _functions;

        /// <summary>
        /// <see cref="IfElseXsltContext"/> constructor.
        /// </summary>
        public IfElseXsltContext()
        {
            _functions = new Dictionary<string, IXsltContextFunction> { { "if-else", new FuncIfElse() } };
        }

        /// <inheritdoc/>
        public override bool Whitespace => true;

        /// <inheritdoc/>
        public override IXsltContextVariable ResolveVariable(string prefix, string name)
        {
            return null;
        }

        /// <inheritdoc/>
        public override IXsltContextFunction ResolveFunction(string prefix, string name, XPathResultType[] args)
        {
            IXsltContextFunction function;
            if (!_functions.TryGetValue(name, out function))
            {
                throw new ArgumentNullException($"Function \"{name}\" not founded");
            }

            return function;
        }

        /// <inheritdoc/>
        public override bool PreserveWhitespace(XPathNavigator node)
        {
            return true;
        }

        /// <inheritdoc/>
        public override int CompareDocument(string baseUri, string nextbaseUri)
        {
            return 0;
        }
    }

    /// <summary>
    /// if-else function context.
    /// </summary>
    public class FuncIfElse : IXsltContextFunction
    {
        /// <inheritdoc/>
        public int Minargs => 3;

        /// <inheritdoc/>
        public int Maxargs => 3;

        /// <inheritdoc/>
        public XPathResultType ReturnType => XPathResultType.Any;

        /// <inheritdoc/>
        public XPathResultType[] ArgTypes => new XPathResultType[0];

        /// <inheritdoc/>
        public object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
        {
            return (bool)args[0] ? args[1] : args[2];
        }
    }
}