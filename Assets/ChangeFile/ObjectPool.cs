using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ObjectPool
{
    public System.Action<GameObject> OnObjRemoved;

    [SerializeField] List<GameObject> listCurrent;
    public List<GameObject> ListCurrent => listCurrent;

    readonly List<GameObject> listAvailable;
    public List<GameObject> ListAvailable => listAvailable;

    readonly GameObject prefab;
    readonly Transform spawnerTrans;

    public ObjectPool(GameObject prefab, Transform spawnerTrans)
    {
        listCurrent = new List<GameObject>();
        listAvailable = new List<GameObject>();

        this.prefab = prefab;
        this.spawnerTrans = spawnerTrans;
    }

    public GameObject GetObjInPool()
    {
        if (listAvailable.Count > 0)
        {
            var obj = listAvailable[0];

            listAvailable.Remove(obj);
            listCurrent.Add(obj);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            InitantiateObj();
            return GetObjInPool();
        }
    }


    public void InitantiateObj()
    {
        var obj = Object.Instantiate(prefab, spawnerTrans);
        listAvailable.Add(obj);
    }


    public void RemoveObj(GameObject obj)
    {
        if (!listCurrent.Contains(obj)) return;

        listCurrent.Remove(obj);
        listAvailable.Add(obj);
        obj.transform.SetParent(spawnerTrans);
        obj.gameObject.SetActive(false);
        OnObjRemoved?.Invoke(obj);
    }


    public void RemoveAllCurrentObj()
    {
        while (listCurrent.Count > 0)
        {
            RemoveObj(listCurrent[0]);
        }
    }

    public void DestroyListAvailable()
    {
        while (listAvailable.Count > 0)
        {
            Object.Destroy(listAvailable[0]);
            listAvailable.RemoveAt(0);
        }
    }
}
