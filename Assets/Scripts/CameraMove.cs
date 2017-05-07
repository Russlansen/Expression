using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CameraMove : MonoBehaviour
{
	private Vector3 startPosition;
	private float endPosition;
	public float EndPosition { get { return endPosition; } set{ endPosition = value; } }
	private float prefHeight;
	public float PrefHeight { get { return prefHeight; } set { prefHeight = value; } }

	void Start ()
    {
		startPosition = transform.position;
	}
	void Update ()
    {
		if(Input.GetKey(KeyCode.Escape))
        {
			SceneManager.LoadScene("MainScene");
		}
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
		{
			transform.position += new Vector3(0, -Input.GetTouch(0).deltaPosition.y * Time.deltaTime * 1.5f, 0);
			transform.position = new Vector3(startPosition.x, Mathf.Clamp(transform.position.y, (endPosition + ((Screen.height / 2 ) /100) - prefHeight), 0 ), startPosition.z);
		}
	}

} 
