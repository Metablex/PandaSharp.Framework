using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace PandaSharp.Framework.Services.Response
{
    public abstract class ListResponseBase<T> : IEnumerable<T>
    {
        private readonly List<T> _items = new List<T>();

        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        [ExcludeFromCodeCoverage]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void AddResponses(IEnumerable<T> responses)
        {
            _items.AddRange(responses);
        }

        public void AddResponse(T response)
        {
            _items.Add(response);
        }
    }
}