using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class StartExitPanelController : MonoBehaviour
{
    private Button button_Yes;
    private Button button_No;
    private AudioClip audioClip_OnClick;

    void Start()
    {
        button_Yes = transform.Find("BackGound/Yes").GetComponent<Button>();
        button_No = transform.Find("BackGound/No").GetComponent<Button>();
        audioClip_OnClick = Resources.Load<AudioClip>("GameSound/ButtonClickSound");

        button_Yes.onClick.AddListener(OnClickYes);
        button_No.onClick.AddListener(OnClickNo);

        gameObject.SetActive(false);
    }
    private void OnClickYes()
    {
        //AudioSource.PlayClipAtPoint(audioClip_OnClick, GameObject.Find("Main Camera").transform.position);
        AudioManager.Instance.CreateAudioClip(audioClip_OnClick, GameObject.Find("Main Camera").transform.position);


#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        
        Application.Quit();
    }
    private void OnClickNo()
    {
        //AudioSource.PlayClipAtPoint(audioClip_OnClick, GameObject.Find("Main Camera").transform.position);
        AudioManager.Instance.CreateAudioClip(audioClip_OnClick, GameObject.Find("Main Camera").transform.position);

        gameObject.SetActive(false);
    }

}
