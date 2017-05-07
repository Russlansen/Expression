using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
	public static AudioSource mainAudio, clickOnTouch, dropOnExpression, win, clickButton;
	private bool switcher = false;

	void Awake()
    {
		mainAudio = GetComponents<AudioSource>()[0];
		clickOnTouch = GetComponents<AudioSource>()[1];
		dropOnExpression = GetComponents<AudioSource>()[2];
		win = GetComponents<AudioSource>()[3];
		clickButton = GetComponents<AudioSource>()[4];
		if (GameObject.FindGameObjectsWithTag("AudioController").Length == 1) DontDestroyOnLoad(gameObject);
		else Destroy(gameObject);
	}

	void Start ()
    {
		if (PlayerPrefs.GetString("Music") == "on")
        {
			mainAudio.Play();
		}
	}

	void Update ()
    {	
		mainAudio = GetComponents<AudioSource>()[0];
		clickOnTouch = GetComponents<AudioSource>()[1];
		dropOnExpression = GetComponents<AudioSource>()[2];
		win = GetComponents<AudioSource>()[3];
		clickButton = GetComponents<AudioSource>()[4];

		if (PlayerPrefs.GetString("Music") == "on" && !switcher)
        {
			mainAudio.Play();
			switcher = true;
		}
		if (PlayerPrefs.GetString("Music") == "off" && switcher)
        {
			mainAudio.Stop();
			switcher = false;
		}
	}

	public static void Play(string clip)
    {
		if (PlayerPrefs.GetString("Music") == "on")
        {
			switch (clip) {
				case "mainAudio":
					mainAudio.Play();
					break;
				case "clickOnTouch":
					clickOnTouch.Play();
					break;
				case "dropOnExpression":
					dropOnExpression.Play();
					break;
				case "win":
					win.Play();
					break;
				case "clickButton":
					clickButton.Play();
					break;	
			}
		}
	}
}
