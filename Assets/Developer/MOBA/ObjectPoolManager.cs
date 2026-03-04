using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


namespace Team3.MOBA
{
    public class ObjectPoolManager : MonoBehaviour
    {
        public static ObjectPoolManager Instance { get; private set; }

        [System.Serializable]
        public class PoolEntry
        {
            public SOVFX vfxData;
            public int defaultCapacity = 10;
            public int maxSize = 50;
        }

        [SerializeField]
        private List<PoolEntry> poolEntries;

        private Dictionary<int, IObjectPool<GameObject>> pools;

        private void Awake()
        {
            if (Instance != null) { Destroy(gameObject); return; }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            pools = new Dictionary<int, IObjectPool<GameObject>>();

            foreach (var entry in poolEntries)
            {
                var pool = new ObjectPool<GameObject>(
                    () => Instantiate(entry.vfxData.VFX),
                    obj => obj.SetActive(true),
                    obj => obj.SetActive(false),
                    obj => Destroy(obj),
                    false,
                    entry.defaultCapacity,
                    entry.maxSize
                );

                pools.Add(entry.vfxData.ID, pool);
            }
        }

        public GameObject GetPooledObject(int id)
        {
            if (pools.ContainsKey(id))
            {
                return pools[id].Get();
            }

            Debug.LogWarning($"No pool found for ID {id}");
            return null;
        }

        public void ReturnObject(int id, GameObject obj)
        {
            if (pools.ContainsKey(id))
            {
                pools[id].Release(obj);
            }
            else
            {
                Destroy(obj); // fallback
            }
        }
    }


}


