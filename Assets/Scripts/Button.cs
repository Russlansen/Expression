using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;


    public class Button : MonoBehaviour
    {
	public GameObject pause;
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
			case "Play":
				string level = PlayerPrefs.GetString("CurrentLevel");
				if(level == "")
					SceneManager.LoadScene("Level1");
                else
                {
					Time.timeScale = 1;
					SceneManager.LoadScene(level);
				}
			break;

			case "LevelList":
				SceneManager.LoadScene("LevelList");
			break;
				
			case "Settings":
                SceneManager.LoadScene("Settings");
                break;

			case "HowTo":
				SceneManager.LoadScene("HowTo");
			break;
				
			case "Rate":
				Application.OpenURL("https://play.google.com/store/apps/details?id=com.FinishGames.Expression");
			break;
		case "Exit":
			Application.Quit();
			break;
		case "Pause":
			pause.SetActive (true);
			Messenger.Broadcast ("Paused");
			Time.timeScale = 0;
			GameObject.FindWithTag("Pause").GetComponent<CircleCollider2D>().enabled = false;
            break;
		case "ClosePause":
			GameObject.FindWithTag("Pause").GetComponent<CircleCollider2D>().enabled = true;
			GetComponent<SpriteRenderer>().color = orange;
			Messenger.Broadcast ("Continue");
			Time.timeScale = 1;
			pause.SetActive(false);
			break;
		case "ToMenu":
			Messenger.Broadcast ("Continue");
			SceneManager.LoadScene ("MainScene");
			Time.timeScale = 1;
			break;
		case "Restart":
			AdsController.PlayAds();
			Messenger.Broadcast ("Continue");
			Time.timeScale = 1;
			string name = Application.loadedLevelName;
			SceneManager.LoadScene (name);
			break;
		case "ToLevelList":
			Time.timeScale = 1;
			Messenger.Broadcast ("Continue");
			SceneManager.LoadScene ("LevelList");
			break;
		case "ToNextLevel":
				string currentLevel = Application.loadedLevelName;
				string number = currentLevel.Substring(5);
				int numLevel = Int32.Parse(number);
				if (numLevel % 10 == 0)
					SceneManager.LoadScene("LevelList");
				int numNextLevel = ++numLevel;
				SceneManager.LoadScene("Level" + numNextLevel);
			break;		


        }
	}
}
