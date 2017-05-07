using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class SlotScript : MonoBehaviour, IDropHandler
{
	public GameObject Item
    {
		get
        {
			if(transform.childCount > 0)
            {
				return transform.GetChild(0).gameObject;
			}
			return null;
		}
	}

	public void OnDrop(PointerEventData eventData)
	{
		AudioSource[] audio;
		if(!Item)
        {
			DragHandler.item.transform.SetParent(transform);
			if (tag == "Expression")
            {
				if (PlayerPrefs.GetString("Music") == "on")
					AudioController.dropOnExpression.Play();
				StageController.actions++;
			}
            else
            {
				if (PlayerPrefs.GetString("Music") == "on")
					AudioController.clickOnTouch.Play();
			}
		}else if(tag != "Static")
        {
			if (tag == "Expression")
            {
				if (PlayerPrefs.GetString("Music") == "on")
					AudioController.dropOnExpression.Play();
				StageController.actions++;
			}
            else
            {
				if (PlayerPrefs.GetString("Music") == "on")
                {
					AudioController.clickOnTouch.Play();
				}
			}
			DragHandler.item.transform.SetParent(transform);
			Item.transform.SetParent(DragHandler.startParent);
			DragHandler.startParent.GetChild(0).transform.localPosition = Vector3.zero;
		}

	}
}
