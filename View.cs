using JetBrains.Annotations;
using UnityEngine;

namespace Jamjardavies.Zenject.ViewController
{
    public class View : MonoBehaviour, IView
    {
        [UsedImplicitly]
        private void Awake()
        {
            // Do nothing.
        }

        [UsedImplicitly]
        private void Start()
        {
            // Do nothing.
        }

        public virtual GameObject GameObject
        {
            get { return gameObject; }
        }

        public virtual bool IsVisible
        {
            get { return gameObject.activeInHierarchy; }
        }

        public virtual void Show()
        {
            GameObject.SetActive(true);
        }

        public void Show(bool shown)
        {
            if (shown)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        public virtual void Hide()
        {
            GameObject.SetActive(false);
        }
    }
}
