using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.MaterialProperty;

public enum ProjectileType
{
    s_Bullet,
    m_Bullet,
    l_Bullet,
    lazer,
    bomb,
    ming,
    last
}

public class Pool : Singleton<Pool>
{
    public Dictionary<ProjectileType,Stack<GameObject>> poolMing = new Dictionary<ProjectileType, Stack<GameObject>> ();

    public GameObject[] poolPrefs = new GameObject[(int)ProjectileType.last];

    public void Get(ProjectileType prjtype,GameObject target)
    {
        Create(prjtype);
        poolMing[prjtype].Push(target);
        target.SetActive(false);
    }
    public void Give(ProjectileType prjtype, Transform targetTr)
    {
        Create(prjtype);

        GameObject gameObject;

        if (!poolMing[prjtype].TryPeek(out gameObject))
        { 
            gameObject =Instantiate(poolPrefs[(int)prjtype], transform);
        }
        else
        {
            poolMing[prjtype].Pop();
        }
        gameObject.transform.SetPositionAndRotation(targetTr.position, targetTr.rotation);
        gameObject.SetActive(true);
    }

    public void Create(ProjectileType prjtype)
    {
        if (!poolMing.ContainsKey(prjtype))
        {
            poolMing[prjtype] = new Stack<GameObject>();
        }
    }

}
