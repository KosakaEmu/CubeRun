using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Test");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator Test()
    {
        yield return new WaitForSeconds(10);
        Time.timeScale = 0;
    }
}
