using UnityEngine;
using System.Collections;

public class TextScript : MonoBehaviour
{

	public string SortingLayerName = "Default";
	public int SortingOrder = 150;

	void Awake ()
	{
		gameObject.GetComponent<MeshRenderer> ().sortingLayerName = SortingLayerName;
		gameObject.GetComponent<MeshRenderer> ().sortingOrder = SortingOrder;
	}
}
