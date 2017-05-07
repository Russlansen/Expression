using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsController : MonoBehaviour
{

	public static int adsCount;

	public static void PlayAds()
    {
		adsCount = PlayerPrefs.GetInt("ads");
		if (adsCount < StageController.adsStep)
        {
			adsCount++;
			PlayerPrefs.SetInt("ads", adsCount);
		}
        else
        {
			if (Advertisement.isSupported)
            {	
				if (Advertisement.IsReady())
                {
					Advertisement.Show();
					PlayerPrefs.SetInt("ads", 1);
				}
			}
		}
	}
}
