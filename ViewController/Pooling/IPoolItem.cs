using UnityEngine;

namespace Jamjardavies.Zenject.Pooling
{
    public interface IPoolItem
    {
        Transform Transform { get; }

        void Initialise();
        void OnDestroy();
    }
}
