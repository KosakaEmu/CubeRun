using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class NewBehaviourScript : MonoBehaviour
{
    private Button button;
    private Transform m_Tranform;

    private Ray ray;
    private RaycastHit hit;
    void Start()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(OnClick); 
  

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    }

    void Update()
    {
        //ChangeUIScale();
        //Debug.Log(Input.mousePosition);
        //if (Physics.Raycast(ray, out hit))
        //{
        //    Debug.Log(hit.collider.name);
        //}
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("点击了吗？1");
            //if (EventSystem.current.IsPointerOverGameObject())
            //{
            //    Debug.Log("点击了吗？2");
            //    var clickObj = EventSystem.current.currentSelectedGameObject;
            //    if (clickObj == null) return;
            //    Debug.Log("点击了吗？3");
            //    if (clickObj.name== "Button")
            //    {
            //        Debug.Log("点击了吗？4");
            //        m_Tranform.DOScale(new Vector3(0.95f, 0.95f, 1), 0.3f);
            //    }
            //    Debug.Log(clickObj.name);
            //}
            Debug.Log("点击了吗？1");
            Tweener tweener= m_Tranform.DOScale(new Vector3(0.5f, 0.5f, 1f), 0.3f);
            tweener.SetUpdate(true);
            Debug.Log("点击了吗？2");
        }
        //if (Input.GetMouseButtonUp(0))
        //{
        //    m_Tranform.DOScale(new Vector3(1, 1, 1), 0.3f);
        //}
    }
    private void OnClick()
    {
        
    }
    
   

}
