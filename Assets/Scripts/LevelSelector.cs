using UnityEngine;
using System.Collections;

public class LevelSelector : MonoBehaviour
{
	[SerializeField] private GameObject levelPrefab, notActiveLevel, attentionPrefab;
	[SerializeField] private GameObject mainCamera;
	private GameObject level;
	private GameObject attention;
	private float lastPosition = 0;
	private const float starsCoef = 2f;
	public const float offsetX = 1.45f;
	public const float offsetY = -2.5f;
	public const float divider = -4f;

	public int numberOfLevels;

	void Awake()
    {
		int row = 0;
		int levelDivider = 0;
		bool levelsOpen = true;
		int stars = PlayerPrefs.GetInt("Stars");
		Vector3 startPosition = new Vector3(-2.2f, 4.2f ,0);

		for(int i = 0; i < numberOfLevels; i++)
        {
			if(levelDivider >= 10)
            {
				attention = Instantiate(attentionPrefab) as GameObject;

                attention.GetComponentInChildren<TextMesh>().text =
                    LangManager.Text(Application.loadedLevelName, attention.GetComponentInChildren<TextMesh>().name, "1") + " " + i * starsCoef + " " + 
                    LangManager.Text(Application.loadedLevelName, attention.GetComponentInChildren<TextMesh>().name, "2") + " " + stars;

				attention.transform.position = new Vector3 (0, startPosition.y + (offsetY / 1.2f), 0);
				startPosition = new Vector3(startPosition.x, startPosition.y + divider, 0);
				float coef = (float)stars/(float)i;
				if(coef < starsCoef)
					levelsOpen = false;
                else
					levelsOpen = true;

				levelDivider = 0;
				row = 0;
				i--;
			}
            else
            {
				if(row >= 4)
                {
					startPosition = new Vector3(startPosition.x, startPosition.y + offsetY, 0);
					row = 0;
					levelDivider--;
					i--;
				}
                else if(levelsOpen)
                {
					level = Instantiate(levelPrefab) as GameObject;
					level.GetComponentInChildren<TextMesh>().text = "" + (i + 1);
					float posX = (offsetX * row) + startPosition.x; 
					level.transform.position = new Vector3 (posX, startPosition.y, 0);
					row++;
				}
                else
                {
					level = Instantiate(notActiveLevel) as GameObject;
					float posX = (offsetX * row) + startPosition.x; 
					level.transform.position = new Vector3 (posX, startPosition.y, 0);
					row++;
				}
			if(i == numberOfLevels - 1)
				lastPosition = level.transform.position.y;

			levelDivider++;
			}
		}
		mainCamera.GetComponent<CameraMove>().EndPosition = lastPosition;
		mainCamera.GetComponent<CameraMove>().PrefHeight  = (level.GetComponent<Renderer>().bounds.size.y) * 2.0f;
	}
}