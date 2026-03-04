using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KekwDetlef.Utils.Serializables
{
    [Serializable]
    public class SerializableDictionary<KeyType, ValueType> : ISerializationCallbackReceiver, IDictionary<KeyType, ValueType> where KeyType : notnull
    {
        [SerializeField] private List<KeyType> keys = new();
        [SerializeField] private List<ValueType> values = new();

        private Dictionary<KeyType, ValueType> dictionary = new();

        public void OnBeforeSerialize()
        {
            keys.Clear();
            values.Clear();

            foreach (var kvp in dictionary)
            {
                keys.Add(kvp.Key);
                values.Add(kvp.Value);
            }
        }

        public void OnAfterDeserialize()
        {
            dictionary = new Dictionary<KeyType, ValueType>();

            if (keys.Count != values.Count)
            {
                Debug.LogError($"SerializableDictionary: Key count ({keys.Count}) and value count ({values.Count}) do not match. Some values may have been lost.");
                return;
            }

            for (int i = 0; i < keys.Count; i++)
            {
                dictionary[keys[i]] = values[i];
            }
        }


        #region Dictionary Wrapper

        public ValueType this[KeyType key]
        {
            get => dictionary[key];
            set => dictionary[key] = value;
        }

        public ICollection<KeyType> Keys => dictionary.Keys;
        public ICollection<ValueType> Values => dictionary.Values;
        public int Count => dictionary.Count;
        public bool IsReadOnly => false;

        public void Add(KeyType key, ValueType value) => dictionary.Add(key, value);
        public bool ContainsKey(KeyType key) => dictionary.ContainsKey(key);
        public bool Remove(KeyType key) => dictionary.Remove(key);
        public bool TryGetValue(KeyType key, out ValueType value) => dictionary.TryGetValue(key, out value);

        public void Add(KeyValuePair<KeyType, ValueType> item) => dictionary.Add(item.Key, item.Value);
        public void Clear() => dictionary.Clear();
        public bool Contains(KeyValuePair<KeyType, ValueType> item) => dictionary.Contains(item);
        public void CopyTo(KeyValuePair<KeyType, ValueType>[] array, int arrayIndex) =>
            ((ICollection<KeyValuePair<KeyType, ValueType>>)dictionary).CopyTo(array, arrayIndex);
        public bool Remove(KeyValuePair<KeyType, ValueType> item) =>
            ((ICollection<KeyValuePair<KeyType, ValueType>>)dictionary).Remove(item);

        public IEnumerator<KeyValuePair<KeyType, ValueType>> GetEnumerator() => dictionary.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion
    }
}
