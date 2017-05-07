using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
	[SerializeField] private Sprite circle;
	[SerializeField] private GameObject star1, star2, star3;
	private string numberLevel;

	void Start ()
    {
		numberLevel = gameObject.GetComponentInChildren<TextMesh>().text;
		switch (PlayerPrefs.GetInt("Level" + numberLevel)){
			case 1:
				star1.GetComponent<SpriteRenderer>().color = Color.yellow;
			break;
			case 2:
				star1.GetComponent<SpriteRenderer>().color = Color.yellow;
				star2.GetComponent<SpriteRenderer>().color = Color.yellow;
			break;
			case 3:
				star1.GetComponent<SpriteRenderer>().color = Color.yellow;
				star2.GetComponent<SpriteRenderer>().color = Color.yellow;
				star3.GetComponent<SpriteRenderer>().color = Color.yellow;
			break;
		}
	}
	
	void OnMouseDown()
    {
		GetComponent<SpriteRenderer>().color = Color.yellow;
	}

	void OnMouseUp()
    {
		GetComponent<SpriteRenderer>().color = Color.white;
	}

	void OnMouseUpAsButton()
    {
		AudioController.Play("clickButton");
		SceneManager.LoadScene("Level" + numberLevel);
	}
}
