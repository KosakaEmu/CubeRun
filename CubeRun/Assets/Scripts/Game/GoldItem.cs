using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldItem : MonoBehaviour
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
    }
}
