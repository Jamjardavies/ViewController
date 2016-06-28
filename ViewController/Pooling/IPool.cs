using System;
using System.Collections.Generic;

using Zenject;

namespace Jamjardavies.Zenject.Pooling
{
    public interface IPool<T> : IFactory<T>, IInitializable, IDisposable, IEnumerable<T>
        where T : IPoolItem
    {

    }
}
