using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    public interface IPool<T>
    {
        public List<T> Get(int count);

        public void Release(T item);

        public void Release(List<T> items);
    }
}
