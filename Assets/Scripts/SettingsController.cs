using UnityEngine;
using System.Collections;

public class SettingsController : MonoBehaviour
{
	public GameObject ruFlag, enFlag, hardnessText, music, noMusic;

	void Awake ()
    {
		string language = PlayerPrefs.GetString("Language");
		string hardness =  PlayerPrefs.GetString("Hardness");
		string musicString = PlayerPrefs.GetString("Music");

		if(language == "en")
        {
			ruFlag.SetActive(false);
			enFlag.SetActive(true);
		}
        else
        {
			ruFlag.SetActive(true);
			enFlag.SetActive(false);
		}

		switch (hardness)
        {
			case ("Easy"):
				hardnessText.GetComponent<TextMesh>().text = "S";
			break;
			case ("Medium"):
				hardnessText.GetComponent<TextMesh>().text = "M";
			break;
			case ("Hard"):
				hardnessText.GetComponent<TextMesh>().text = "L";
			break;
			case ("ExtraHard"):
				hardnessText.GetComponent<TextMesh>().text = "XL";
			break;
		}

		if(musicString == "on")
        {
			noMusic.SetActive(false);
			music.SetActive(true);
		}
        else
        {
			noMusic.SetActive(true);
			music.SetActive(false);
		}

	}
}
