using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class InventoryController : MonoBehaviour
{
    [System.Serializable]
    public class InventoryEvent
    {
        public string key;
        public UnityEvent OnAdd, OnRemove, OnUse;
    }

    public InventoryEvent[] inventoryEvents;
    public event System.Action OnInventoryLoaded;

    HashSet<string> m_InventoryItems = new HashSet<string>();

    public void AddItem(string key)
    {
        if (!m_InventoryItems.Contains(key))
        {
            m_InventoryItems.Add(key);
            var ev = GetInventoryEvent(key);
            if (ev != null) ev.OnAdd.Invoke();
        }
    }

    public void RemoveItem(string key)
    {
        if (m_InventoryItems.Contains(key))
        {
            var ev = GetInventoryEvent(key);
            if (ev != null) ev.OnRemove.Invoke();
            m_InventoryItems.Remove(key);
        }
    }

    public void UseItem(string key)
    {
        if (m_InventoryItems.Contains(key))
        {
            var ev = GetInventoryEvent(key);
            if (ev != null) ev.OnUse.Invoke();
        }
    }

    public void Clear()
    {
        m_InventoryItems.Clear();
    }

    InventoryEvent GetInventoryEvent(string key)
    {
        foreach (var iv in inventoryEvents)
        {
            if (iv.key == key) return iv;
        }
        return null;
    }
}

