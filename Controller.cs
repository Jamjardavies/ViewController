using UnityEngine;
using Zenject;

namespace Jamjardavies.Zenject.ViewController
{
    public interface IController : IInitializable, System.IDisposable { }

    public interface IViewController : IController
    {
        IView View { get; }
    }

    public abstract class Controller : IController
    {
        [Inject]
        protected Controller()
        {

        }

        public virtual void Initialize()
        {
            Initialise();
        }

        public virtual void Dispose()
        {
            OnDestroy();
        }

        public abstract void Initialise();
        public abstract void OnDestroy();

        public class TransientFactory : IFactory<System.Type, IController>
        {
            [Inject]
            private DiContainer m_container = null;

            [Inject]
            public TransientFactory()
            {
                // #JD 09/05/2016: This is required for IL2CPP.
            }

            public IController Create(System.Type type)
            {
                return Create(type, null);
            }

            public IController Create(System.Type type, params object[] args)
            {
                // #JD 17/03/2016: Only create if we have the controller bound.
                if (!m_container.HasBinding(new InjectContext(m_container, type)))
                {
                    return null;
                }

                if (args == null)
                {
                    return (IController)m_container.Instantiate(type);
                }

                return (IController)m_container.Instantiate(type, args);
            }
        }
    }

    public abstract class Controller<T> : Controller, IViewController
        where T : IView
    {
        [Inject]
        private T m_view = default(T);
        
        private event System.Action<Controller<T>> m_disposed = delegate { };
        
        public event System.Action<Controller<T>> Disposed
        {
            add { m_disposed += value; }
            remove { m_disposed -= value; }
        }

        public T View
        {
            get { return m_view; }
        }

        IView IViewController.View
        {
            get { return m_view; }
        }

        [Inject]
        protected Controller()
        {

        }

        public override void Dispose()
        {
            base.Dispose();

            Object.Destroy(View.GameObject);
            
            m_disposed.Invoke(this);
        }
    }
}