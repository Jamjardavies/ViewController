using StickSports.Zenject.Pooling;
using Zenject;

public static class DiContainerExtensions
{
    public static void BindPool<TPool, TPoolItem>(this DiContainer container, string containerName, int poolSize)
        where TPoolItem : IPoolItem
        where TPool : IPool<TPoolItem>
    {
        container.Bind<string>().FromInstance(containerName).WhenInjectedInto<TPool>();
        container.Bind<int>().FromInstance(poolSize).WhenInjectedInto<TPool>();

        container.Bind<TPool>().AsTransient();
    }
}
