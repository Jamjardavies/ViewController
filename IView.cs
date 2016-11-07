using UnityEngine;

namespace Jamjardavies.Zenject.ViewController
{
    public interface IView
    {
        GameObject GameObject { get; }
        bool IsVisible { get; }

        void Show();
        void Show(bool shown);
        void Hide();
    }
}