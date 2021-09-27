using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverAnimaPanel : MonoBehaviour
{
    private GameObject prefabs_GameOverAnima;
    private Transform tr_GameOverPanel;
    void Awake()
    {
        prefabs_GameOverAnima = Resources.Load<GameObject>("TextEffect/GameOverAnima");
        tr_GameOverPanel = GameObject.Find("Canvas/GameOverPanel").transform;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameOverAnimaPlay()
    {
        Debug.Log("Ö´ÐÐÁËÂð");
        Instantiate<GameObject>(prefabs_GameOverAnima, transform);
        Invoke("ShowGameOverPanel", 3f);
    }
    private void ShowGameOverPanel()
    {
        tr_GameOverPanel.gameObject.SetActive(true);
        GameOverPanelController.Instance.UpdataScore();
        //GameOverPanelController.Instance.UpdataGoldNum(gamingGoldNum);

    }
}
