using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackItem : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {
        if (gameObject != null)
        {
            gameObject.transform.Rotate(Vector3.down * 0.3f, Space.World);

        }
        //item.transform.rotation = Quaternion.Euler(item.transform.rotation.x, item.transform.rotation.y + Time.deltaTime, item.transform.rotation.z);
    }
}
