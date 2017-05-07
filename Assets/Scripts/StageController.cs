using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class StageController : MonoBehaviour
{
    [SerializeField] private GameObject timer, answerText;
    [SerializeField] private GameObject[] expressionSlots, maths;
    [SerializeField] private GameObject objectPrefab;
	[SerializeField] private GameObject[] slots;
	[SerializeField] private GameObject actionText;
	[HideInInspector]public static int actions = 0;

	public const string game_id = "1252606";
	public static int adsStep = 2;

    private int objectValue;
    public int ObjectValue { get { return objectValue; } }

    public int maxTime;
	public int maxActions;
	private bool timeExpire = false;
    public int objectNumber;

    private GameObject _object;
	private Color orange = new Color(0.93f, 0.49f, 0.1f);
    [HideInInspector] public List<int> values;

	private int targetAnswerInt;

	public GameObject Timer {get {return timer;} set {timer = value;}}
	public GameObject AnswerText {get {return answerText;}}
	public GameObject[] ExpressionSlots {get {return expressionSlots;}}
	public bool TimeExpire { get{return timeExpire;}}
	public int TargetAnswerInt {get{return targetAnswerInt;}}

    void Awake()
    {
		PlayerPrefs.SetString("CurrentLevel",  Application.loadedLevelName);
		string hardness = PlayerPrefs.GetString("Hardness");
		timer.GetComponent<TextMesh>().text = "" + maxTime;

		/*
		 *  Object positioning, assigning values
		 */
        for (int i = 0; i < objectNumber; i++)
        {
			_object = Instantiate(objectPrefab) as GameObject;
			for (int j = 0; j < slots.Length; j++)
            {
				if (!slots[j].GetComponent<SlotScript>().Item)
                {
					DragHandler.item = _object;
					DragHandler.item.transform.SetParent(slots[j].transform);
					DragHandler.item.transform.localPosition = Vector3.zero;
					DragHandler.item.transform.localScale = new Vector3(1, 1, 1);
					break;
				}
			}

			switch (hardness)
            {
			case ("Easy"):
				objectValue = UnityEngine.Random.Range(1, 11);
				_object.GetComponent<ObjectController>().ObjectValue = objectValue;
				break;
			case ("Medium"):
				objectValue = UnityEngine.Random.Range(0, 26);
				_object.GetComponent<ObjectController>().ObjectValue = objectValue;
				break;
			case ("Hard"):
				objectValue = UnityEngine.Random.Range(0, 46);
				_object.GetComponent<ObjectController>().ObjectValue = objectValue;
				break;
			case ("ExtraHard"):
				objectValue = UnityEngine.Random.Range(0, 100);
				_object.GetComponent<ObjectController>().ObjectValue = objectValue;
				break;
			}
			values.Add(objectValue);           
        }



		/*
		 * Composite the string expression
		 */
		string targetAnswer = "";

		for (int i = 0; i < expressionSlots.Length; i++)
        {
			int index = UnityEngine.Random.Range(0, values.Count);
			targetAnswer = targetAnswer + values[index];
			if (i < maths.Length)
				targetAnswer = targetAnswer + maths[i].GetComponent<TextMesh>().text;
			values.RemoveAt(index);
		}

		MathParser.Parser p = new MathParser.Parser();

		if (p.Evaluate(targetAnswer)) // String parsing to expression 
        { 
			LangManager.calcAnswer = (int)p.Result;
			targetAnswerInt = (int)p.Result;
		}
    }

	void OnDestroy()
    {
		actions = 0;
	}

	void Update ()
    {
		/*
		 * Timer handler
		 */
		if(maxTime > Time.timeSinceLevelLoad || timeExpire)
        {
			if(!timeExpire)
				timer.GetComponent<TextMesh>().text = "" + ( maxTime - (int)Time.timeSinceLevelLoad);
            else
				timer.GetComponent<TextMesh>().text = "" + (int)Time.timeSinceLevelLoad;
		}
        else
        {
			timeExpire = true;
			timer.GetComponent<TextMesh>().color = orange;
		}

		/*
		 * Action handler
		 */
		if (maxActions < actions)
			actionText.GetComponent<TextMesh>().color = orange;
     
		/*
		 * Calculate answer
		 */
		string answer = Calculate(expressionSlots, maths);
		MathParser.Parser p = new MathParser.Parser();
		if (p.Evaluate(answer))
			answerText.GetComponent<TextMesh>().text = "" + p.Result;

		if (Input.GetKey (KeyCode.Escape))
			SceneManager.LoadScene ("LevelList");
    }

    private string Calculate(GameObject[] allSlots, GameObject[] maths)
    { 
        string answer = "";
		bool multiply = false;

		for (int i = 0; i < allSlots.Length; i++) // Cycle composite the string expression with slot values 
        { 
			GameObject slotItem = allSlots[i].GetComponent<SlotScript>().Item;
			if (slotItem)
            {
				answer = answer + slotItem.GetComponent<ObjectController>().ObjectValue;
			}
            else
            {
				if(multiply)
                {
					answer = answer + "1"; 
					multiply = false;
				}
                else answer = answer + "0";
			}
			if (i < maths.Length)
            {
				if (maths[i].GetComponent<TextMesh>().text == "*")
                    multiply = true;
				else
                    multiply = false;
				answer = answer + maths[i].GetComponent<TextMesh>().text;
			}
        }
        return answer; 
    }
}
