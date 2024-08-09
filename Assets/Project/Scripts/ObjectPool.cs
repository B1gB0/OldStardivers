using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class ObjectPool <T> where T : MonoBehaviour
{
    public T Prefab { get; }
    
    public bool AutoExpand { get; set; }
    
    public Transform Container { get; }

    private List<T> _pool;

    public ObjectPool(T prefab, int count)
    {
        this.Prefab = prefab;
        this.Container = null;
        this.CrateObjectPool(count);
    }

    public ObjectPool(T prefab, int count, Transform container)
    {
        this.Prefab = prefab;
        this.Container = container;
        this.CrateObjectPool(count);
    }

    public bool HasFreeElement(out T element)
    {
        foreach (var objects in _pool)
        {
            if (!objects.gameObject.activeInHierarchy)
            {
                element = objects;
                objects.gameObject.SetActive(true);
                
                return true;
            }
        }

        element = null;

        return false;
    }

    public T GetFreeElement()
    {
        if (this.HasFreeElement(out var element))
            return element;

        if (this.AutoExpand)
            return this.CreateObject(true);
        
        throw new Exception($"There is no free elements in pool of type {typeof(T)}");
    }

    private void CrateObjectPool(int count)
    {
        _pool = new List<T>();

        for (int i = 0; i < count; i++)
        {
            this.CreateObject();
        }
    }

    private T CreateObject(bool isActiveByDefault = false)
    {
        var createdObject = Object.Instantiate(this.Prefab, this.Container);
        createdObject.gameObject.SetActive(isActiveByDefault); 
        
        _pool.Add(createdObject);

        return createdObject;
    }
}
