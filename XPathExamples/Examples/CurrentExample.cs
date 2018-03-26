using System;
using System.Collections.Generic;
using System.Xml.XPath;
using System.Xml.Xsl;

using XPathExamples.Common;

namespace XPathExamples.Examples
{
    /// <summary>
    /// current function example.
    /// </summary>
    public class CurrentExample : ExampleBase
    {
        /// <inheritdoc/>
        public override string Name => @"""current"" function";

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
</catalog>";

            var document = XPathDocumentFromString(sourceXml);
            var navigator = document.CreateNavigator();
            var query1 = @"/catalog/book/author";
            var query2 = @"current()/@gender";

            Print("[XML]", sourceXml);
            Print("[Query1]", query1);
            var currentNode = navigator.SelectSingleNode(query1);

            Print("[SubQuery2]", query2);

            var result = navigator.Evaluate(query2, new CurrentXsltContext(currentNode));

            Print("[Result]", result);
        }
    }

    /// <summary>
    /// Custom XsltContext.
    /// </summary>
    public class CurrentXsltContext : XsltContext
    {
        private readonly IDictionary<string, IXsltContextFunction> _functions;

        /// <summary>
        /// <see cref="IfElseXsltContext"/> constructor.
        /// </summary>
        public CurrentXsltContext(XPathNavigator currentNode)
        {
            _functions = new Dictionary<string, IXsltContextFunction> { { "current", new FuncCurrent(currentNode) } };
        }

        /// <inheritdoc/>
        public override bool Whitespace => true;

        /// <inheritdoc/>
        public override IXsltContextVariable ResolveVariable(string prefix, string name) { return null; }

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
        public override bool PreserveWhitespace(XPathNavigator node) { return true; }

        /// <inheritdoc/>
        public override int CompareDocument(string baseUri, string nextbaseUri) { return 0; }
    }

    /// <summary>
    /// "current" function context.
    /// </summary>
    public class FuncCurrent : IXsltContextFunction
    {
        private readonly XPathNavigator _currentNode;

        /// <summary>
        /// <see cref="FuncCurrent"/> constructor.
        /// </summary>
        public FuncCurrent(XPathNavigator currentNode)
        {
            _currentNode = currentNode;
        }

        /// <inheritdoc/>
        public int Minargs => 0;

        /// <inheritdoc/>
        public int Maxargs => 0;

        /// <inheritdoc/>
        public XPathResultType ReturnType => XPathResultType.Any;

        /// <inheritdoc/>
        public XPathResultType[] ArgTypes => null;

        /// <inheritdoc/>
        public object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
        {
            return _currentNode.Select(".");
        }
    }
}