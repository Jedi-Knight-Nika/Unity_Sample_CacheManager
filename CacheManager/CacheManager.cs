using UnityEngine;
using System.Collections.Generic;

namespace Nikolla_L
{
    public class CacheManager : ScriptableObject
    {
        private static CacheManager instance = null;
        private Dictionary<string, GameObjectStore> cache;

        private CacheManager()
        {
            InitCacheManager();
        }

        void InitCacheManager()
        {
            cache = new Dictionary<string, GameObjectStore>();
        }

        /// <summary>
		/// Gets the instance
		/// </summary>
        public static CacheManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = CreateInstance("CacheManager") as CacheManager;
                }

                return instance;
            }
        }

        /// <summary>
		/// Determines whether this instance has inactive objects the category
		/// </summary>
		/// <param name="category">Category</param>
        public bool HasInactiveObjects(string category)
        {
            if (!cache.ContainsKey(category))
                return false;

            return cache[category].HasUnusedItems();
        }

        /// <summary>
		/// Gets from cache
		/// </summary>
		/// <param name="category">Category</param>
        private GameObject GetFromCache(string category)
        {
            return cache[category].GetUnuseditem();
        }

        /// <summary>
		/// Adds gameObject to cache
		/// </summary>
		/// <param name="category">Category</param>
		/// <param name="item">Item</param>
		void AddToCache(string category, GameObject item)
        {
            ICacheable cch = item.GetComponent<ICacheable>();

            GameObjectStore store;
            if (!cache.ContainsKey(category))
            {
                store = new GameObjectStore();
            }
            else
                store = cache[category];

            store.Add(item);
            cache[category] = store;
        }

        /// <summary>
		/// Gets the cached gameObject of the category or creates a new one the game object
		/// </summary>
		/// <param name="category">Category</param>
		/// <param name="item">Item</param>
		public GameObject GetOrCreate(GameObject item)
        {
            string category = item.GetComponent<ICacheable>().GetCacheCategory();

            if (HasInactiveObjects(category))
            {
                return GetFromCache(category);
            }

            GameObject newItem;

            if (item != null)
            {
                newItem = Instantiate(item);
                AddToCache(category, newItem);
            }
            else
            {
                newItem = GameObject.CreatePrimitive(PrimitiveType.Cube);
            }

            return newItem;
        }

        /// <summary>
        /// Evict item from cache
        /// </summary>
        /// <param name="category">Category</param>
        /// <param name="item">Item</param>
        public void Evict(GameObject item)
        {
            ICacheable cch = item.GetComponent<ICacheable>();

            string category = cch.GetCacheCategory();

            if (cache.ContainsKey(category))
            {
                cache[category].Evict(item);

                if (cache[category].Count() == 0)
                    cache.Remove(category);
            }
        }

        public void PurgeCategory(string category)
        {
            if (cache.ContainsKey(category))
            {
                cache[category] = new GameObjectStore();
            }
        }

        /// <summary>
        /// Frees the item. It will be available from GetOrCreate once again
        /// </summary>
        /// <param name="category">Category</param>
        /// <param name="item">Item</param>
        public void FreeItem(string category, GameObject item)
        {
            if (cache.ContainsKey(category))
            {
                item.SetActive(false);
            }
        }
    }
}

