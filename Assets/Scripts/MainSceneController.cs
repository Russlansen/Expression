using UnityEngine;
using System.Collections;

public class MainSceneController : MonoBehaviour
{
	void Awake()
    {
		if(PlayerPrefs.GetString("Language") == "") PlayerPrefs.SetString("Language", "ru");
		if(PlayerPrefs.GetString("Hardness") == "") PlayerPrefs.SetString("Hardness", "Easy");
		if(PlayerPrefs.GetString("Music") == "") PlayerPrefs.SetString("Music", "on");
		if (PlayerPrefs.GetInt("ads") == 0) PlayerPrefs.SetInt("ads", 1);
	}

	void Start ()
    {
		GameObject text = GameObject.Find("PLAY");
        text.GetComponent<TextMesh>().text = LangManager.Text (Application.loadedLevelName, text.name);
	}

	void Update ()
    {
		if(Input.GetKey(KeyCode.Escape))
        {
			Application.Quit();
		}
	}
}
