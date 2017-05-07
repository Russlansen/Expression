using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class WinController : MonoBehaviour
{
	[SerializeField] private GameObject stageController;
	[SerializeField] private GameObject winScreen;
	[SerializeField] private GameObject starSolved, starTime, starAction;
	private Color idle, error;
	private StageController stageControllerScript;
	private string answer;
	private Color orange = new Color(0.93f, 0.49f, 0.1f, 0.5f);
	private Color gray = new Color(0.63f, 0.63f, 0.63f);
	private Color darkGray = new Color(0.152f, 0.152f, 0.152f, 0.576f);
	private const float fadeSpeed = 0.02f;
	private bool time = false;
	private bool actions = false;
	private bool win = false;
	private bool winSound = true;

	void Awake()
    {
		stageControllerScript = stageController.GetComponent<StageController>();
		Advertisement.Initialize(StageController.game_id, false);
	}

	void Update()
    {
		if (win)
        {
			if (winSound)
            {
				AudioController.Play("win");
				winSound = false;
			}
			winScreen.SetActive(true);
			Messenger.Broadcast ("Paused");
			IEnumerator coroutine = Stars(time, actions);
			StartCoroutine (coroutine);
			stageControllerScript.Timer.GetComponent<TextMesh>().gameObject.SetActive(false);
			GameObject.FindWithTag("Pause").GetComponent<CircleCollider2D>().enabled = false;
		}
	}

	void LateUpdate ()
    {
		bool fillCircle = true;
		answer = stageControllerScript.AnswerText.GetComponent<TextMesh>().text;
		if(stageControllerScript.TargetAnswerInt ==  Int32.Parse(answer)){
			for(int i = 0; i < stageControllerScript.ExpressionSlots.Length; i++){
				GameObject slotItem = stageControllerScript.ExpressionSlots[i].GetComponent<SlotScript>().Item;
				if(!slotItem)
                {
					stageControllerScript.ExpressionSlots[i].GetComponent<Image>().color = orange;
					fillCircle = false;
				}
			}
			if(fillCircle)
            {
				int stars = 1;
				if(!stageControllerScript.TimeExpire)
                {
					stars++;
					time = true;
				}
				if(StageController.actions < stageControllerScript.maxActions)
                {
					stars++;
					actions = true;
				}
				if(PlayerPrefs.GetInt(Application.loadedLevelName) < stars)
                {
					int tempStars = PlayerPrefs.GetInt("Stars");
					tempStars -= PlayerPrefs.GetInt(Application.loadedLevelName);
					tempStars += stars;
					PlayerPrefs.SetInt("Stars", tempStars);
					PlayerPrefs.SetInt(Application.loadedLevelName, stars);
				}
				win = true;
			}
		}
        else
        {
			for(int i = 0; i < stageControllerScript.ExpressionSlots.Length; i++)
				stageControllerScript.ExpressionSlots[i].GetComponent<Image>().color = darkGray;
		}
	}

	IEnumerator  Stars(bool time, bool actions)
    {
		starSolved.GetComponent<SpriteRenderer>().color = orange;
		for(float alpha = 0; alpha < 1.0f; alpha += fadeSpeed)
        {
			orange.a = alpha;
			starSolved.GetComponent<SpriteRenderer>().color = orange;
			yield return null;
		}

		if(time)
        {
			for(float alpha = 0; alpha < 1.0f; alpha += fadeSpeed)
            {
				orange.a = alpha;
				starTime.GetComponent<SpriteRenderer>().color = orange;
				yield return null;
			}
		}
        else
        {
			for(float alpha = 0; alpha < 1.0f; alpha += fadeSpeed)
            {
				gray.a = alpha;
				starTime.GetComponent<SpriteRenderer>().color = gray;
				yield return null;
			}
		}

		if(actions)
        {
			for(float alpha = 0; alpha < 1.0f; alpha += fadeSpeed)
            {
				orange.a = alpha;
				starAction.GetComponent<SpriteRenderer>().color = orange;
				yield return null;
			}
		}
        else
        {
			for(float alpha = 0; alpha < 1.0f; alpha += fadeSpeed)
            {
				gray.a = alpha;
				starAction.GetComponent<SpriteRenderer>().color = gray;
				yield return null;
			}
		}
	}
}
