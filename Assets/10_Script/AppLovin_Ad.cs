using UnityEngine;
using System.Collections;

public class AppLovin_Ad : MonoBehaviour {

	public bool appLovinUse;
	public int videoReward = 100;
	public string appLovinSDKkey = "BeZCuZXf_OWleaU1fw1NJ537mnwmqla0N8DFimwh9QRp_xqfrr18w5FgtAQhv_hH8wXrgRXqtzctuQWiFTsAe5";

	public delegate void RewardForWatchingInterstitial(int value);
	public static event RewardForWatchingInterstitial InterstitialWatched;

	public GameObject addManager;
	Advertisement_Manager _myAdvertisementManager;

	void Start()
	{
		_myAdvertisementManager = addManager.GetComponent<Advertisement_Manager> ();

		AppLovin.SetUnityAdListener("Ad_Manager");

		if (appLovinUse) 
		{
			AppLovin.SetSdkKey(appLovinSDKkey);
			AppLovin.InitializeSdk();
			PreloadInterstitial();
			PreloadRewardedInterstitial();
		}
	}

	void PreloadInterstitial()
	{
		if (appLovinUse) 
		{
			AppLovin.PreloadInterstitial ();
		}
	}

	void PreloadRewardedInterstitial()
	{
		if (appLovinUse) 
		{
			AppLovin.LoadRewardedInterstitial ();
		}
	}

	public void ShowInterstitial()
	{
		Debug.Log ("Showing Applovin add interstitial");

		if (appLovinUse) {
			if (AppLovin.HasPreloadedInterstitial ()) {
				// An ad is currently available, so show the interstitial.
				AppLovin.ShowInterstitial ();
				Debug.Log ("APPLOVIN ADD WAS READY TO DISPLAY SO WE DISPLAYED IT");
				PreloadInterstitial ();

				_myAdvertisementManager.AddSuccesfullyDisplayed ();
			} else {
				Debug.LogWarning ("APP LOVIN ADD WAS NOT READY SO WE CALL MANAGER TO DISPLAY ANOTHER ADD");

				_myAdvertisementManager.ShowInterstitial ();

				PreloadInterstitial ();
			}
		}
		else 
		{
			Debug.Log ("APP LOVIN INTERSTITIAL IS NOT ALLOWED IN INSPECTOR TO BE DISPLAYED SO WE CALL MANAGER TO DISPLAY OTHER ADD");
			_myAdvertisementManager.ShowInterstitial ();
		}
	}

	public void ShowRewardInterstitial()
	{
		Debug.Log ("Showing Applovin reward video");

		if (appLovinUse) 
		{
			if (AppLovin.IsIncentInterstitialReady ()) 
			{
				AppLovin.ShowRewardedInterstitial ();
				Debug.Log ("APPLOVIN REWARD VIDEO WAS READY TO DISPLAY SO WE PLAY IT");
				_myAdvertisementManager.RewardVideoSuccesfullyDisplayed ();
			} 
			else 
			{
				Debug.Log ("APPLOVIN REWARD VIDEO WAS NOT READY TO DISPLAY SO WE CALL MANAGER TO DISPLAY OTHER REWARD VIDEO");
				_myAdvertisementManager.ShowRewardVideo ();

				PreloadRewardedInterstitial();
			}
		}
		else 
		{
			Debug.Log ("APP LOVIN REWARD VIDEO IS NOT ALLOWED IN INSPECTOR TO BE DISPLAYED SO WE CALL MANAGER TO DISPLAY OTHER VIDEO");
			_myAdvertisementManager.ShowRewardVideo ();
		}
	}

	void onAppLovinEventReceived(string ev)
	{
		// The format would be "REWARDAPPROVEDINFO|AMOUNT|CURRENCY"
		if(ev.Contains("REWARDAPPROVEDINFO"))
		{
			Debug.LogWarning ("ABOUT TO CALL 1st InterstitialWatched DELEGATE");

			if(InterstitialWatched != null)
			{
				InterstitialWatched(videoReward);
				Debug.LogWarning ("InterstitialWatched DELEGATE WAS CALLED 1st time");
			}
			else Debug.LogWarning ("NOTHING IS ASSIGN TO InterstitialWatched DELEGATE");
			PreloadRewardedInterstitial();

//			// For this example, assume the event is "REWARDAPPROVEDINFO|10|Coins"
//			char[] delimeter = "|".ToCharArray();
//			// Split the string based on the delimeter
//			string split = ev.Split(delimeter).ToString();;
//			
//			// Pull out the currency amount
//			double amount = double.Parse(split);
//			int value = System.Convert.ToInt32(amount);
//			
//			// Pull out the currency name
//			//string currencyName = split[2];
//			
//			// Do something with the values from above.  For example, grant the coins to the user.
//			//updateBalance(amount, currencyName);
//
//			Debug.LogWarning ("ABOUT TO CALL InterstitialWatched DELEGATE");
//
//			if(InterstitialWatched != null)
//			{
//				InterstitialWatched(value);
//				Debug.LogWarning ("InterstitialWatched DELEGATE WAS CALLED");
//			}
//			else Debug.LogWarning ("NOTHING IS ASSIGN TO InterstitialWatched DELEGATE");
//
//			PreloadRewardedInterstitial();
		}
		else if(ev.Contains("LOADEDREWARDED"))
		{
			// A rewarded video was successfully loaded.
		}
		else if(ev.Contains("LOADREWARDEDFAILED"))
		{
			// A rewarded video failed to load.
			PreloadRewardedInterstitial();
		}
		else if(ev.Contains("HIDDENREWARDED"))
		{
			// A rewarded video has been closed.  Preload the next rewarded video.
			PreloadRewardedInterstitial();
		}
	}

	public void ShowOnlyAppLovinADD()
	{
		Debug.Log ("SHOW ONLY APPLOVIN INTERSTITIAL ADD");
		if (AppLovin.HasPreloadedInterstitial ()) 
		{
			// An ad is currently available, so show the interstitial.
			AppLovin.ShowInterstitial ();
			PreloadInterstitial ();
		}
	}
}
