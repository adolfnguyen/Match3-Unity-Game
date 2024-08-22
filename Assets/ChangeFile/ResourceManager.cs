using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static NormalItem;
using static BonusItem;

public class ResourceManager : MonoBehaviour
{
    protected static ResourceManager instance;
    private bool initialized = false;

    public static ResourceManager Instance
    {
        get
        {
            if (instance != null) return instance;
            instance = FindAnyObjectByType<ResourceManager>();
            return instance;
        }
    }

    Dictionary<string, GameObject> listNormalItems;
    GameObject cell;
    NewTextureDataSO newTextureDataSO;
    public GameObject itemsPreb;
    ObjectPool itemsPool;
    public GameObject Cell { get { if (cell == null) cell = Resources.Load<GameObject>(Constants.PREFAB_CELL_BACKGROUND); return cell; } }

    public NewTextureDataSO NewTextureDataSO { get { if (newTextureDataSO == null) newTextureDataSO = Resources.Load<NewTextureDataSO>("NewTextureDataSO"); return newTextureDataSO; } }
    Dictionary<string, ObjectPool> listPoolItems = new Dictionary<string, ObjectPool>();
    /*void GetListNormalItems()
    {
        listNormalItems = new Dictionary<string, GameObject>();
        List<NormalItem.eNormalType> eNormalTypes = Enum.GetValues(typeof(eNormalType)).Cast<eNormalType>().ToList();
        for (int i = 0; i < eNormalTypes.Count; i++)
        {
            eNormalType type = eNormalTypes[i];
            string objname = "";
            switch (type)
            {
                case eNormalType.TYPE_ONE:
                    objname = Constants.PREFAB_NORMAL_TYPE_ONE;
                    break;
                case eNormalType.TYPE_TWO:
                    objname = Constants.PREFAB_NORMAL_TYPE_TWO;
                    break;
                case eNormalType.TYPE_THREE:
                    objname = Constants.PREFAB_NORMAL_TYPE_THREE;
                    break;
                case eNormalType.TYPE_FOUR:
                    objname = Constants.PREFAB_NORMAL_TYPE_FOUR;
                    break;
                case eNormalType.TYPE_FIVE:
                    objname = Constants.PREFAB_NORMAL_TYPE_FIVE;
                    break;
                case eNormalType.TYPE_SIX:
                    objname = Constants.PREFAB_NORMAL_TYPE_SIX;
                    break;
                case eNormalType.TYPE_SEVEN:
                    objname = Constants.PREFAB_NORMAL_TYPE_SEVEN;
                    break;
            }
            GameObject obj = Resources.Load<GameObject>(objname);
            listNormalItems.Add(objname, obj);
            listPoolItems.Add(objname, new ObjectPool(obj, this.transform));

        }
        List<BonusItem.eBonusType> eBonusTypes = Enum.GetValues(typeof(eBonusType)).Cast<eBonusType>().ToList();
        for (int i = 0; i < eBonusTypes.Count; i++)
        {
            eBonusType type = eBonusTypes[i];
            string objname = "";
            switch (type)
            {
                case eBonusType.NONE:
                    break;
                case eBonusType.HORIZONTAL:
                    objname = Constants.PREFAB_BONUS_HORIZONTAL;
                    break;
                case eBonusType.VERTICAL:
                    objname = Constants.PREFAB_BONUS_VERTICAL;
                    break;
                case eBonusType.ALL:
                    objname = Constants.PREFAB_BONUS_BOMB;
                    break;
            }
            GameObject obj = Resources.Load<GameObject>(objname);
            listNormalItems.Add(objname, obj);
            listPoolItems.Add(objname, new ObjectPool(obj, this.transform));
        }
    }*/
    void GetNormalItems()
    {
        itemsPreb = Resources.Load<GameObject>(Constants.PREFAB_NORMAL_TYPE_ONE);
        itemsPool = new ObjectPool(itemsPreb, this.transform);
    }
    public GameObject GetObjInPool()
    {
        if(itemsPreb == null) GetNormalItems();
        return itemsPool.GetObjInPool();
    }

    public void ReturnObject(GameObject obj)
    {
        itemsPool.RemoveObj(obj);
    }
}
