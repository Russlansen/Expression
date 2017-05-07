using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public static  GameObject item;
	private Transform parent;
	public static Transform startParent;

	public void OnBeginDrag(PointerEventData eventData)
	{
		item = gameObject;
		startParent =  transform.parent;
		item.transform.SetParent(transform.root);
		parent = transform.parent;
		GetComponent<CanvasGroup>().blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData eventData)
	{
		transform.position = Camera.main.ScreenToWorldPoint(eventData.position);
		transform.position = new Vector3(transform.position.x, transform.position.y, 0);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		GetComponent<CanvasGroup>().blocksRaycasts = true;
		if(transform.parent == parent)
        {
			item.transform.SetParent(startParent);
			transform.localPosition = Vector3.zero;
		}
        else
        {
			transform.localPosition = Vector3.zero;
		}
		item = null;
	}
}
