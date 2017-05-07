using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ObjectController : MonoBehaviour
{
    private int objectValue;
    public int ObjectValue { get { return objectValue; } set { objectValue = value;  } }
	public ObjectArrayScript objectArrayScript;
	private int index;

	void Awake()
    {
		index = Random.Range(0, objectArrayScript.objectSprites.Capacity);
		GetComponent<Image>().sprite = objectArrayScript.objectSprites[index];
		Messenger.AddListener ("Paused", Paused);
		Messenger.AddListener ("Continue", Continue);
	}

	void OnDestroy()
    {
		Messenger.RemoveListener ("Paused", Paused);
		Messenger.RemoveListener ("Continue", Continue);
	}

	private void Paused()
    {
		this.GetComponent<DragHandler> ().enabled = false;
	}

	private void Continue()
    {
		this.GetComponent<DragHandler> ().enabled = true;
	}
}
