using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoSingleton<ObjectPooling>
{

   private Dictionary<GameObject, Stack<GameObject>> _dictionary = new Dictionary<GameObject, Stack<GameObject>>();
   
   [SerializeField] protected int poolSize;
   
   [Header("Initialize")] 
   [SerializeField] protected GameObject bullet;

   private void Start()
   {
      InitializeNewPool(bullet);
   }

   public virtual GameObject GetObject(GameObject prefab)
   {
      if (_dictionary.ContainsKey(prefab) == false)
      {
         InitializeNewPool(prefab);
      }

      if (_dictionary[prefab].Count == 0)
      {
         CreateNewObject(prefab);
      }

      GameObject objectToGet = _dictionary[prefab].Pop();
      objectToGet.SetActive(true);
      return objectToGet;
   }

   public void ReTurnObject(GameObject gameObject,float delay = 0.01f) => StartCoroutine(DelayReturn(delay,gameObject));
   
   private IEnumerator DelayReturn(float delay , GameObject objectToReturn)
   {
      yield return new WaitForSeconds(delay);
      ReturnToPool(objectToReturn);
   }
   
   protected virtual void  ReturnToPool(GameObject prefab)
   {
      GameObject newPrefab = prefab.GetComponent<PooledObject>().originalPrefab;
      
      prefab.SetActive(false);
      
      _dictionary[newPrefab].Push(prefab);
   }
   
   protected virtual void InitializeNewPool(GameObject prefab)
   {
      _dictionary[prefab] = new Stack<GameObject>();
      
      for (int i = 0; i < poolSize; i++)
      {
         CreateNewObject(prefab);
      }
   }

   protected void CreateNewObject(GameObject prefab)
   {
      GameObject newObject = Instantiate(prefab, transform);
      
      newObject.AddComponent<PooledObject>().originalPrefab = prefab;
      newObject.SetActive(false);
      
      _dictionary[prefab].Push(newObject);
   }
}
