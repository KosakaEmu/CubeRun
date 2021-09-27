using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopItemDragArea : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        RotateModel.Instance.InitTransform_model(transform.parent.GetComponent<ShopItemController>().PrefabName);
        RotateModel.Instance.OnBeginDrag();
    }

    public void OnDrag(PointerEventData eventData)
    {
        RotateModel.Instance.InitTransform_model(transform.parent.GetComponent<ShopItemController>().PrefabName);

        RotateModel.Instance.OnDrag();
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        RotateModel.Instance.OnEndDrag();

    }

}
