using UnityEngine;
using System.Collections;

public class Advertisement_Manager : MonoBehaviour {

	public enum AppLovin 		{High, Medium, Low};
	public enum Chartboost 		{High, Medium, Low};
	public enum UnityAds 		{High, Medium, Low};

	public AppLovin appLovinPriority;
	public Chartboost chartboostPriority;
	public UnityAds unityAdsPriority;

	public GameObject appLovinManager;
	public GameObject chartboostManager;
	public GameObject unityAdsManager;

	AppLovin_Ad _myAppLovin;
	InterstitialAdChartboost _myChartboost;
	InterstitialAdUnity	_myUnity;

	int tryNO = 0;
	int tryVideoNO = 0;

	void OnEnable() 
	{
		Slot_Machine_System._ShowInterstitial += ShowInterstitial;
	}

	void OnDisable() 
	{
		Slot_Machine_System._ShowInterstitial -= ShowInterstitial;
	}

	void Start()
	{
		_myAppLovin = appLovinManager.GetComponent<AppLovin_Ad> ();
		_myChartboost = chartboostManager.GetComponent<InterstitialAdChartboost> ();
		_myUnity = unityAdsManager.GetComponent<InterstitialAdUnity> ();
	}

	#region INTERSTITIAL SECTION

	public void ShowInterstitial()
	{
		tryNO++;

		Debug.Log ("show add with priority number " + tryNO.ToString());

		if (tryNO <= 0) 
		{
			Debug.Log ("TRY NUMBER IS LESS OR EQUAL TO NULL");
		}
		else if (tryNO > 0 && tryNO < 4) 
		{
			switch (tryNO) 
			{
			case 1:
				DisplayHighPriorityAdd ();
				Debug.Log ("DISPLAY HIGH PRIORITY ADD");
				break;
			case 2:
				DisplayMediumPriorityAdd ();
				Debug.Log ("DISPLAY MEDIUM PRIORITY ADD");
				break;
			case 3:
				DisplayLowPriorityAdd ();
				Debug.Log ("DISPLAY MEDIUM PRIORITY ADD");
				break;
			}
		}
		else if (tryNO >= 4) 
		{
			Debug.Log ("TRY NUMBER IS GREATER OR EQUAL TO 4 AND WE WAS NOT ABLE TO DISPLAY ADD");
			NotAbleToDisplayAdd ();
		}
	}

	void DisplayHighPriorityAdd()
	{
		if (appLovinPriority == AppLovin.High) 
		{
			_myAppLovin.ShowInterstitial ();
			Debug.Log ("HIGH PRIORITY ADD BELONGS TO APPLOVIN");
		}
		else if (chartboostPriority == Chartboost.High)
		{
			_myChartboost.ShowInterstitial ();
			Debug.Log ("HIGH PRIORITY ADD BELONGS TO CHARTBOOST");
		}
		else if (unityAdsPriority == UnityAds.High)
		{
			_myUnity.ShowInterstitial ();
			Debug.Log ("HIGH PRIORITY ADD BELONGS TO UNITYADD");
		}
	}

	void DisplayMediumPriorityAdd()
	{
		if (appLovinPriority == AppLovin.Medium) 
		{
			_myAppLovin.ShowInterstitial ();
			Debug.Log ("MEDIUM PRIORITY ADD BELONGS TO APPLOVIN");
		}
		else if (chartboostPriority == Chartboost.Medium)
		{
			_myChartboost.ShowInterstitial ();
			Debug.Log ("MEDIUM PRIORITY ADD BELONGS TO CHARTBOOST");
		}
		else if (unityAdsPriority == UnityAds.Medium)
		{
			_myUnity.ShowInterstitial ();
			Debug.Log ("MEDIUM PRIORITY ADD BELONGS TO UNITYADS");
		}
	}

	void DisplayLowPriorityAdd()
	{
		if (appLovinPriority == AppLovin.Low) 
		{
			_myAppLovin.ShowInterstitial ();
			Debug.Log ("LOW PRIORITY ADD BELONGS TO APPLOVIN");
		}
		else if (chartboostPriority == Chartboost.Low)
		{
			_myChartboost.ShowInterstitial ();
			Debug.Log ("LOW PRIORITY ADD BELONGS TO CHARTBOOST");
		}
		else if (unityAdsPriority == UnityAds.Low)
		{
			_myUnity.ShowInterstitial ();
			Debug.Log ("LOW PRIORITY ADD BELONGS TO UNITYADS");
		}
	}

	public void AddSuccesfullyDisplayed()
	{
		tryNO = 0;
		Debug.Log ("ADD WAS SUCCESFULLY DISPLAYED AND TRYNO SHOULD BE 0 AND THE REAL VALUE IS " + tryNO.ToString());
	}

	public void NotAbleToDisplayAdd()
	{
		tryNO = 0;
		Debug.Log ("ADVERT MANAGER IS NOT ABLE TO DISPLAY ANY ADD AND TRYNO SHOULD BE NOW 0 AND THE REAL VALUE IS " + tryNO.ToString());
	}
		
	#endregion

	#region REWARD VIDEO SECTION

	public void ShowRewardVideo()
	{
		tryVideoNO++;

		Debug.Log ("show add with priority number " + tryVideoNO.ToString());

		if (tryVideoNO <= 0) 
		{
			Debug.Log ("TRY VIDEO NUMBER IS LESS OR EQUAL TO NULL");
		}
		else if (tryVideoNO > 0 && tryVideoNO < 4) 
		{
			switch (tryVideoNO) 
			{
			case 1:
				DisplayHighPriorityRewardVideo ();
				Debug.Log ("DISPLAY HIGH PRIORITY VIDEO");
				break;
			case 2:
				DisplayMediumPriorityRewardVideo ();
				Debug.Log ("DISPLAY MEDIUM PRIORITY VIDEO");
				break;
			case 3:
				DisplayLowPriorityRewardVideo ();
				Debug.Log ("DISPLAY LOW PRIORITY VIDEO");
				break;
			}
		}
		else if (tryVideoNO >= 4) 
		{
			Debug.Log ("TRY VIDEO NUMBER IS GREATER OR EQUAL TO 4 AND WE WAS NOT ABLE TO DISPLAY VIDEO");
			NotAbleToDisplayRewardVideo ();
		}
	}

	void DisplayHighPriorityRewardVideo()
	{
		if (appLovinPriority == AppLovin.High) 
		{
			_myAppLovin.ShowRewardInterstitial ();
			Debug.Log ("HIGH PRIORITY VIDEO BELONGS TO APPLOVIN");
		}
		else if (chartboostPriority == Chartboost.High)
		{
			_myChartboost.PlayRewardedVideo ();
			Debug.Log ("HIGH PRIORITY VIDEO BELONGS TO CHARTBOOST");
		}
		else if (unityAdsPriority == UnityAds.High)
		{
			_myUnity.ShowRewardInterstitial ();
			Debug.Log ("HIGH PRIORITY VIDEO BELONGS TO UNITYADS");
		}
	}

	void DisplayMediumPriorityRewardVideo()
	{
		if (appLovinPriority == AppLovin.Medium) 
		{
			_myAppLovin.ShowRewardInterstitial ();
			Debug.Log ("MEDIUM PRIORITY VIDEO BELONGS TO APPLOVIN");
		}
		else if (chartboostPriority == Chartboost.Medium)
		{
			_myChartboost.PlayRewardedVideo ();
			Debug.Log ("MEDIUM PRIORITY VIDEO BELONGS TO CHARTBOOST");
		}
		else if (unityAdsPriority == UnityAds.Medium)
		{
			_myUnity.ShowRewardInterstitial ();
			Debug.Log ("MEDIUM PRIORITY VIDEO BELONGS TO UNITY ADS");
		}
	}

	void DisplayLowPriorityRewardVideo()
	{
		if (appLovinPriority == AppLovin.Low) 
		{
			_myAppLovin.ShowRewardInterstitial ();
			Debug.Log ("LOW PRIORITY VIDEO BELONGS TO APPLOVIN");
		}
		else if (chartboostPriority == Chartboost.Low)
		{
			_myChartboost.PlayRewardedVideo ();
			Debug.Log ("LOW PRIORITY VIDEO BELONGS TO CHARTBOOST");
		}
		else if (unityAdsPriority == UnityAds.Low)
		{
			_myUnity.ShowRewardInterstitial ();
			Debug.Log ("LOW PRIORITY VIDEO BELONGS TO UNITY ADS");
		}
	}

	public void RewardVideoSuccesfullyDisplayed()
	{
		tryVideoNO = 0;
		Debug.Log ("REWARD VIDEO WAS SUCCESFULLY DISPLAYED AND TRYVIDEONO SHOULD BE 0 AND THE REAL VALUE IS " + tryVideoNO.ToString());
	}

	public void NotAbleToDisplayRewardVideo()
	{
		tryVideoNO = 0;
		Debug.Log ("ADVERT MANAGER IS NOT ABLE TO DISPLAY ANY REWARD VIDEO AND TRYVIDEONO SHOULD BE NOW 0 AND THE REAL VALUE IS " + tryVideoNO.ToString());
	}

	#endregion
}
