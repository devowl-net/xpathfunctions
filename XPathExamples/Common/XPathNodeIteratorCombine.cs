using System.Collections.Generic;
using System.Xml.XPath;

namespace XPathExamples.Common
{
    /// <summary>
    /// <see cref="XPathNodeIterator"/> container.
    /// </summary>
    public class XPathNodeIteratorCombine : XPathNodeIterator
    {
        private readonly XPathNodeIterator[] _nodeIterators;

        private readonly int _maxIndexValue;

        private readonly IDictionary<int, XPathNodeIterator> _iteratorNodeMapping;

        private int _currentPosition = -1;

        /// <summary>
        /// Создание экземпляра класса <see cref="XPathNodeIteratorCombine"/>.
        /// </summary>
        public XPathNodeIteratorCombine(params XPathNodeIterator[] nodeIterators)
        {
            _nodeIterators = nodeIterators;
            _iteratorNodeMapping = new Dictionary<int, XPathNodeIterator> { { _currentPosition, null } };

            foreach (var iterator in _nodeIterators)
            {
                if (iterator.Count > 0)
                {
                    for (int i = 0; i < iterator.Count; i++)
                    {
                        _iteratorNodeMapping.Add(_maxIndexValue, iterator);
                        _maxIndexValue++;
                    }
                }
            }
        }

        /// <inheritdoc/>
        public override XPathNavigator Current => _iteratorNodeMapping[_currentPosition]?.Current;

        /// <inheritdoc/>
        public override int CurrentPosition => _currentPosition;

        /// <inheritdoc/>
        public override XPathNodeIterator Clone()
        {
            return new XPathNodeIteratorCombine(_nodeIterators);
        }

        /// <inheritdoc/>
        public override bool MoveNext()
        {
            _currentPosition++;
            return _currentPosition != _maxIndexValue && _iteratorNodeMapping[_currentPosition].MoveNext();
        }
    }
}