using Jamjardavies.Zenject.Pooling;
using UnityEngine;

namespace Jamjardavies.Zenject.ViewController
{
    public abstract class PoolController<T> : Controller<T>, IPoolItem
        where T : View
    {
        public Transform Transform
        {
            get { return View.transform; }
        }
    }
}
