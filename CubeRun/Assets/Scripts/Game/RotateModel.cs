using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class RotateModel : MonoBehaviour
{
    private static RotateModel instance;
    private Transform transform_model;
    private bool isRotate;
    private Vector3 startPos;
    private Vector3 startRot;
    private float rotateScale = 0.5f;

    public static RotateModel Instance { get => instance; }
    

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isRotate)
        {
            
        }
        if (Input.GetMouseButtonUp(0))
        {
           
        }

    }
    public void InitTransform_model(string name)
    {
        transform_model=transform.Find(name);
    }
    public void OnBeginDrag()
    {
        isRotate = true;
        startPos = Input.mousePosition;
        startRot = transform_model.eulerAngles;
    }

    public void OnDrag()
    {
        if (isRotate)
        {
            var currentPosx = Input.mousePosition;
            var currentPosy = Input.mousePosition;
            var x = startPos.x - currentPosx.x;
            var y = startPos.y - currentPosy.y;
            transform_model.eulerAngles = startRot + new Vector3(y * rotateScale, x * rotateScale, 0);
            //transform_model.eulerAngles = startRot + new Vector3(0, x * rotateScale, 0);
        }
    }

    public void OnEndDrag()
    {
        isRotate = false;
    }
}
