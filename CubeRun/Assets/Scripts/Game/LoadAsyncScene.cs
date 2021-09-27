using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;
public class LoadAsyncScene : MonoBehaviour
{

    //��ʾ���ȵ��ı�
    private Text progress;
    //����������ֵ
    private float progressValue;
    //������
    private Slider slider;
    [Tooltip("�¸�����������")]
    public string nextSceneName;

    private AsyncOperation async = null;

    private Button button_BG;
    private Button button_Tip;

    private void Start()
    {
        progress = transform.Find("Tips").GetComponent<Text>();
        slider = transform.GetComponent<Slider>();
        button_BG = GameObject.Find("Canvas/BackGound").GetComponent<Button>();
        button_Tip = GameObject.Find("Canvas/LoadingBar/Tips").GetComponent<Button>();
        StartCoroutine("LoadScene");

    }
    private static GameM A()
    {
        return new GameM();
    }
    IEnumerator LoadScene()
    {
        async = SceneManager.LoadSceneAsync(nextSceneName);
        async.allowSceneActivation = false;
        while (!async.isDone)
        {
            if (async.progress < 0.9f)
                progressValue = async.progress;
            else
                progressValue = 1.0f;

            slider.value = progressValue;
            progress.text = (int)(slider.value * 100) + " %";

            if (progressValue >= 0.9)
            {
                progress.text = "�����Ļ����";
                button_BG.onClick.AddListener(OnClickBackGound);
                button_Tip.onClick.AddListener(OnClickBackGound);
                //if (Input.anyKeyDown)
                //{
                //    async.allowSceneActivation = true;
                //}
            }

            yield return null;
        }

    }
    private void OnClickBackGound()
    {
        async.allowSceneActivation = true;
    }

}
