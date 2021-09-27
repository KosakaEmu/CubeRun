using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class ObjectPool : MonoBehaviour
{
    private Dictionary<string, Queue<GameObject>> pool;

    void Awake()
    {
        pool = new Dictionary<string, Queue<GameObject>>();
    }

    public void AddObject(string name,GameObject go)
    {
        if (!pool.ContainsKey(name))
        {
            //Debug.Log(name);
            pool.Add(name, new Queue<GameObject>());
            //Debug.Log(go.name);
            pool[name].Enqueue(go);
            //Debug.Log(go.name);
            //Debug.Log(name);
            //Debug.Log(pool.Count);

            foreach (var item in pool[name])
            {
                //Debug.Log(item.ToString());
            }
            //go.SetActive(false);

        }
        else
        {
            //go.SetActive(false);
            //Debug.Log(go.name);
            //Debug.Log("wo cao nima ");
            //Debug.Log(pool.Count);
            pool[name].Enqueue(go);
            foreach (var item in pool[name])
            {
                //Debug.Log(item.ToString());
            }
            //go.SetActive(false);

        }
    }
    public GameObject GetObject(string name)
    {
        GameObject temp=null;
        if (pool[name].Count > 0)
        {
            temp= pool[name].Dequeue();
            //Debug.Log(temp.name);
            temp.name += "???";
            temp.SetActive(true);
            //Debug.Log(temp.name);

            //Debug.Log("getObject 执行了吗");
        }
        return temp;
    }
    public bool Data(string name)
    {
        //Debug.Log(name);
        //Debug.Log(pool.Count);
        //Debug.Log(pool.ContainsKey(name));

        if (pool.Count > 0&& pool.ContainsKey(name))
        {
            //Debug.Log("执行了吗data111");
            //if (!pool.ContainsKey(name))
            //{
            //    pool.Add(name, new Queue<GameObject>());
            //    if (pool[name].Count > 0 )
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}
            //else
            //{
            if (pool[name].Count > 0)
                {
                    //Debug.Log("执行了吗data333333");

                    return true;
                }
                else
                {
                    return false;
                }
            //}
            
        }
        else
        {
            //Debug.Log("执行了吗data2222");

            return false;

        }
    }
    public void CleanObjectPool()
    {
        
        pool.Clear();
    }
    
}
