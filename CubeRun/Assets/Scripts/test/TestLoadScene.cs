using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestLoadScene : MonoBehaviour
{
    void Start()
    {
        Button btn = transform.GetComponent<Button>();
        btn.onClick.AddListener(delegate () { SceneManager.LoadScene("LoadScene"); });
    }
  
}
