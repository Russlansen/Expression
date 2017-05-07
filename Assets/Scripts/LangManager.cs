using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;

public class LangManager : MonoBehaviour
{
	[Header("Main menu translation block")]
	[Space(10)]
	[SerializeField] private GameObject playButton;

	[Header("Level translation block")]
	[Space(10)]
	[SerializeField] private GameObject _targetAnswer;
	[SerializeField] private GameObject actions;

	[Header("On Pause translation block")]
	[Space(10)]
	[SerializeField] private GameObject toMenu;
	[SerializeField] private GameObject restart, toLevelList;

	[Header("Win translation block")]
	[Space(10)]
	[SerializeField] private GameObject congatulation;
	[SerializeField] private GameObject levelСomplete, solved, time, actionsWin,
	toMenuWin, restartWin, toLevelListWin, nextLevel;

	[Header("Setting translation block")]
	[Space(10)]
	[SerializeField] private GameObject settings;
	[SerializeField] private GameObject language, hardness, music;


	[Header("\"How To\" translator")]
	[Space(10)]
	[SerializeField] private GameObject htTimerText;
	[SerializeField] private GameObject htActionsText, htMainText, htTargetAnswer, htTargetAnswerDesc;

	[HideInInspector]
	public static int calcAnswer;

	void Start()
    {
		// Main menu translation block
		if(playButton != null) playButton.GetComponent<TextMesh>().text  =  Text(Application.loadedLevelName, playButton.name);

		// Level translation block
		if (_targetAnswer != null) _targetAnswer.GetComponent<TextMesh>().text = Text("Stage", _targetAnswer.name) + calcAnswer;

		// On Pause translation block
		if(toMenu != null) toMenu.GetComponent<TextMesh>().text  = Text ("Pause", toMenu.name);
		if(restart != null) restart.GetComponent<TextMesh>().text  =  Text("Pause", restart.name);
		if(toLevelList != null) toLevelList.GetComponent<TextMesh>().text  =  Text("Pause", toLevelList.name);

		// Win translation block
		if(congatulation != null) congatulation.GetComponent<TextMesh>().text  =  Text("Win", congatulation.name);
		if(levelСomplete != null) levelСomplete.GetComponent<TextMesh>().text  =  Text("Win", levelСomplete.name);
		if(toMenuWin != null) toMenuWin.GetComponent<TextMesh>().text  = Text ("Pause", toMenu.name);
		if(restartWin != null) restartWin.GetComponent<TextMesh>().text  =  Text("Pause", restart.name);
		if(toLevelListWin != null) toLevelListWin.GetComponent<TextMesh>().text  =  Text("Pause", toLevelList.name);
		if(solved != null) solved.GetComponent<TextMesh>().text  =  Text("Win", solved.name);
		if(time != null) time.GetComponent<TextMesh>().text  =  Text("Win", time.name);
		if(actionsWin != null) actionsWin.GetComponent<TextMesh>().text  =  Text("Win", actionsWin.name);
		if(nextLevel != null) nextLevel.GetComponent<TextMesh>().text  =  Text("Win", nextLevel.name);

		// "How To translation block"
		if (htTimerText != null) htTimerText.GetComponent<TextMesh>().text = Text("HowTo", htTimerText.name);
		if (htActionsText != null) htActionsText.GetComponent<TextMesh>().text = Text("HowTo", htActionsText.name);
		if (htMainText != null) htMainText.GetComponent<TextMesh>().text = Text("HowTo", htMainText.name);
		if (htTargetAnswer != null) htTargetAnswer.GetComponent<TextMesh>().text = Text("HowTo", htTargetAnswer.name);
		if (htTargetAnswerDesc != null) htTargetAnswerDesc.GetComponent<TextMesh>().text = Text("HowTo", htTargetAnswerDesc.name);
	}

	void Update()
    {
		// Level translation block
		if(actions != null) actions.GetComponent<TextMesh>().text  =  Text("Stage", actions.name)  +  StageController.actions;

		// Setting translation block
		if(settings != null) settings.GetComponent<TextMesh>().text  =  Text("Settings", settings.name);
		if(language != null) language.GetComponent<TextMesh>().text  =  Text("Settings", language.name);
		if(hardness != null) hardness.GetComponent<TextMesh>().text  =  Text("Settings", hardness.name);
		if(music != null) music.GetComponent<TextMesh>().text  =  Text("Settings", music.name);
	}

    public static string Text(string levelName, string nodeName, string partName) // Translation method
    {
        string _curLanguage = PlayerPrefs.GetString("Language");
        if (_curLanguage == "")
        {
            PlayerPrefs.SetString("Language", "en");
            _curLanguage = PlayerPrefs.GetString("Language");
        }
        TextAsset textAsset = (TextAsset)Resources.Load("Language", typeof(TextAsset));
        XmlDocument root = new XmlDocument();
        root.LoadXml(textAsset.text);
        string xPath = "Settings/group[@id='" + levelName + "']/group[@id='" + nodeName + "']/string[@lang='" + _curLanguage + "']/text[@id='" + partName + "']";
        XmlNode xValue = root.SelectSingleNode(xPath);
        if (xValue != null)
            return xValue.InnerText;
        else
            return "error";
    }

    public static string Text(string levelName, string id) // Translation method
	{
		string _curLanguage = PlayerPrefs.GetString ("Language");
		if (_curLanguage == "")
        {
			PlayerPrefs.SetString ("Language", "en");
			_curLanguage = PlayerPrefs.GetString ("Language");
		}
        TextAsset textAsset = (TextAsset)Resources.Load("Language", typeof(TextAsset));
        XmlDocument root = new XmlDocument();
        root.LoadXml(textAsset.text);

        string xPath = "Settings/group[@id='" + levelName + "']/string[@id='" + id + "']/text[@lang='" + _curLanguage + "']";
		XmlNode xValue = root.SelectSingleNode(xPath);
		if (xValue != null)
			return xValue.InnerText;
		else
			return "error";
	}
}
