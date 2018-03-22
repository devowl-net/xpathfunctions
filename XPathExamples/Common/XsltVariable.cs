using System;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace XPathExamples.Common
{
    /// <summary>
    /// <see cref="IXsltContextVariable"/>
    /// </summary>
    public class XsltVariable : IXsltContextVariable
    {
        /// <summary>
        /// Not used
        /// </summary>
        public bool IsLocal => false;

        /// <summary>
        /// Not used
        /// </summary>
        public bool IsParam => false;

        /// <summary>
        /// Not used
        /// </summary>
        public XPathResultType VariableType => XPathResultType.Any;

        /// <summary>
        /// Gets the value of the variable specified
        /// </summary>
        /// <param name="xsltContext">Context in which this variable is used</param>
        /// <returns>Value of the variable</returns>
        public object Evaluate(XsltContext xsltContext)
        {
            throw new NotSupportedException();
        }
    }
}