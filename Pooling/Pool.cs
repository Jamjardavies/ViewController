using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace StickSports.Zenject.Pooling
{
    /// <summary>
    /// A standard pool that re-uses the oldest item when the pool is empty.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Pool<T> : IPool<T>
        where T : IPoolItem, new()
    {
        [Inject]
        private DiContainer m_container = null;

        [Inject]
        private string m_poolContainerName = "Pool";

        [Inject]
        private int m_poolSize = 4;

        private T[] m_itor;
        private Queue<T> m_items;
        private Queue<T> m_usedItems;

        private GameObject m_poolContainer;

        private bool m_disposed;

        public T this[int index]
        {
            get { return m_itor[index]; }
        }

        public int Count
        {
            get { return m_itor.Length; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < m_itor.Length; i++)
            {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        [Inject]
        public Pool() { }

        [Inject]
        public void Initialize()
        {
            m_items = new Queue<T>(m_poolSize);
            m_usedItems = new Queue<T>(m_poolSize);
            m_itor = new T[m_poolSize];

            m_poolContainer = m_container.CreateEmptyGameObject(m_poolContainerName);
            
            for (int i = 0; i < m_poolSize; i++)
            {
                T item = CreateItem();

                item.Transform.SetParent(m_poolContainer.transform, false);
                item.OnDestroy();

                m_items.Enqueue(item);
                m_itor[i] = item;
            }
        }
        
        public void Dispose()
        {
            if (m_disposed)
            {
                return;
            }

            m_items.Clear();
            m_usedItems.Clear();

            foreach (T item in m_itor)
            {
                item.OnDestroy();
            }

            m_items = null;
            m_usedItems = null;
            m_itor = null;

            Object.Destroy(m_poolContainer);

            m_disposed = true;
        }

        public T Create()
        {
            if (m_items.Count == 0)
            {
                T oldBall = m_usedItems.Dequeue();

                oldBall.OnDestroy();
                m_items.Enqueue(oldBall);
            }

            T ball = m_items.Dequeue();

            ball.Initialise();
            m_usedItems.Enqueue(ball);

            return ball;
        }

        private T CreateItem()
        {
            return m_container.Instantiate<T>();
        }
    }
}
