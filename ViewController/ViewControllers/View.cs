using UnityEngine;

namespace Jamjardavies.Zenject.ViewController
{
    public class View : MonoBehaviour
    {
        protected virtual GameObject GameObject
        {
            get { return gameObject; }
        }

        public virtual void Show()
        {
            GameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            GameObject.SetActive(false);
        }
    }
}
