using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SettingsButton : MonoBehaviour
{
	public GameObject SettingsController;

	private Color orange = new Color(0.937f, 0.498f, 0.101f);
	private Color red = new Color(0.894f, 0.172f, 0.141f);

	void OnMouseDown()
    {
		GetComponent<SpriteRenderer>().color = red;
	}

	void OnMouseUp()
    {
		GetComponent<SpriteRenderer>().color = orange;
	}

	void OnMouseUpAsButton()
    {
		AudioController.Play("clickButton");
		string buttonName = this.name;
		switch (buttonName)
        {
			case "ButtonLanguage":
				if(PlayerPrefs.GetString("Language") == "en")
                {
					PlayerPrefs.SetString("Language", "ru");
					SettingsController.GetComponent<SettingsController>().ruFlag.SetActive(true);
					SettingsController.GetComponent<SettingsController>().enFlag.SetActive(false);
				}
                else
                {
					PlayerPrefs.SetString("Language", "en");
					SettingsController.GetComponent<SettingsController>().ruFlag.SetActive(false);
					SettingsController.GetComponent<SettingsController>().enFlag.SetActive(true);
				}
			break;

			case "ButtonHardness":
				switch(PlayerPrefs.GetString("Hardness"))
                {
					case ("Easy"):
						PlayerPrefs.SetString("Hardness", "Medium");
						SettingsController.GetComponent<SettingsController>().hardnessText.GetComponent<TextMesh>().text = "M";
					break;
					case ("Medium"):
						PlayerPrefs.SetString("Hardness", "Hard");
						SettingsController.GetComponent<SettingsController>().hardnessText.GetComponent<TextMesh>().text = "L";
					break;
					case ("Hard"):
						PlayerPrefs.SetString("Hardness", "ExtraHard");
						SettingsController.GetComponent<SettingsController>().hardnessText.GetComponent<TextMesh>().text = "XL";
					break;
					case ("ExtraHard"):
						PlayerPrefs.SetString("Hardness", "Easy");
						SettingsController.GetComponent<SettingsController>().hardnessText.GetComponent<TextMesh>().text = "S";
					break;
				}
			break;

			case "ButtonMusic":
				if(PlayerPrefs.GetString("Music") == "on")
                {
					PlayerPrefs.SetString("Music", "off");
					SettingsController.GetComponent<SettingsController>().noMusic.SetActive(true);
					SettingsController.GetComponent<SettingsController>().music.SetActive(false);
				}
                else
                {
					PlayerPrefs.SetString("Music", "on");
					SettingsController.GetComponent<SettingsController>().noMusic.SetActive(false);
					SettingsController.GetComponent<SettingsController>().music.SetActive(true);
				}
			break;
		}

	}

	void Update ()
    {
        if (Input.GetKey(KeyCode.Escape))
            SceneManager.LoadScene("MainScene");
	}
}
