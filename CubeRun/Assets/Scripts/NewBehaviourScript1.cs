using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR
        Debug.Log(" --- Unity Editor");
# endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
