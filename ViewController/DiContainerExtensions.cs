using System;
using Jamjardavies.Zenject.Pooling;
using Jamjardavies.Zenject.ViewController;
using UnityEngine;
using Zenject;

public static class DiContainerExtensions
{
    public static void BindController<TController>(this DiContainer container)
        where TController : IController
    {
        container.BindAllInterfaces<TController>().To<TController>().AsSingle().NonLazy();
		container.Bind<TController>().To<TController>().AsSingle().NonLazy();
    }

    public static void BindViewController<TView, TController>(this DiContainer container, GameObject viewPrefab)
        where TView : View
        where TController : Controller
    {
        container.Bind<TView>().FromPrefab(viewPrefab).AsSingle().WhenInjectedInto<TController>();
        container.BindController<TController>();
    }

    public static void BindViewController<TView, TController>(this DiContainer container, TView viewPrefab)
        where TView : View
        where TController : Controller
    {
        container.Bind<TView>().FromPrefab(viewPrefab).AsSingle().WhenInjectedInto<TController>();
        container.BindController<TController>();
    }

    public static void BindViewController<TView, TController>(this DiContainer container, string viewPath)
        where TView : View
        where TController : Controller
    {
        container.Bind<TView>().FromPrefabResource(viewPath).AsSingle().WhenInjectedInto<TController>();
        container.BindController<TController>();
    }

    public static void BindTransientViewController<TView, TController>(this DiContainer container, GameObject viewPrefab)
        where TView : View
        where TController : Controller
    {
        container.BindTransientViewController<TView, TController, TController>(viewPrefab);
    }

    public static void BindTransientViewController<TView, TController, TInjectTo>(this DiContainer container, GameObject viewPrefab)
        where TView : View
        where TController : Controller
    {
        container.Bind<TView>().FromPrefab(viewPrefab).AsTransient().WhenInjectedInto<TInjectTo>();
        container.Bind<TController>().AsTransient();
    }

    public static void BindTransientViewController<TView, TController>(this DiContainer container, string viewPath)
        where TView : View
        where TController : Controller
    {
        container.BindTransientViewController<TView, TController, TController>(viewPath);
    }

    public static void BindTransientViewController<TView, TController, TInjectTo>(this DiContainer container, string viewPath)
        where TView : View
        where TController : Controller
    {
        container.Bind<TView>().FromPrefabResource(viewPath).AsTransient().WhenInjectedInto<TInjectTo>();
        container.Bind<TController>().AsTransient();
    }

    public static void BindPool<TPool, TPoolItem>(this DiContainer container, string containerName, int poolSize)
        where TPoolItem : IPoolItem
        where TPool : IPool<TPoolItem>
    {
        container.Bind<string>().FromInstance(containerName).WhenInjectedInto<TPool>();
        container.Bind<int>().FromInstance(poolSize).WhenInjectedInto<TPool>();

        container.Bind<TPool>().AsTransient();
    }
}
