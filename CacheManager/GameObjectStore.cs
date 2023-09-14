using UnityEngine;
using System.Collections.Generic;

namespace Nikolla_L
{
    /// <summary>
    /// Simple gameObject store
    /// </summary>
    internal class GameObjectStore
    {
        List<GameObject> CACHE_STORAGE;

        /// <summary>
        /// Initialize a new instance of the storage
        /// </summary>
        public GameObjectStore()
        {
            CACHE_STORAGE = new List<GameObject>();
        }

        /// <summary>
        /// Add the gameobject
        /// </summary>
        /// <param name="go">gameobject</param>
        public void Add(GameObject go)
        {
            CACHE_STORAGE.Add(go);
        }

        /// <summary>
        /// Determines whether this instance has unused cache items
        /// </summary>
        /// <returns><c>true</c> if this instance has unused items; otherwise, <c>false</c>.</returns>
        public bool HasUnusedItems()
        {
            foreach (GameObject item in CACHE_STORAGE)
            {
                ICacheable cacheable = item.GetComponent<ICacheable>();

                if (cacheable.IsFree())
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets an unused item
        /// </summary>
        /// <returns>An unused item</returns>
        public GameObject GetUnuseditem()
        {
            foreach (GameObject item in CACHE_STORAGE)
            {
                ICacheable cacheable = item.GetComponent<ICacheable>();

                if (cacheable.IsFree())
                {
                    return item;
                }
            }

            return null;
        }

        /// <summary>
        /// Count the amount of stored gameObjects
        /// </summary>
        public int Count()
        {
            return CACHE_STORAGE?.Count ?? 0;
        }

        /// <summary>
        /// Remove the gameobject from store
        /// </summary>
        /// <param name="go">gameobject</param>
        public void Evict(GameObject go)
        {
            if (CACHE_STORAGE.Contains(go))
                CACHE_STORAGE.Remove(go);
        }
    }
}

