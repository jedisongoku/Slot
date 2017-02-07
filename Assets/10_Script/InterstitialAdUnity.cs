using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class InterstitialAdUnity : MonoBehaviour {

	public bool unityAdsInUse;
	public GameObject addManager;
	Advertisement_Manager _myAdvertisementManager;

	// Serialize private fields
	//  instead of making them public.
	[SerializeField] string iosGameId = "1050675";
	[SerializeField] string androidGameId = "1050674";
	[SerializeField] bool enableTestMode;

	public string zoneId;
	public int rewardQty = 250;

	void Start ()
	{
		_myAdvertisementManager = addManager.GetComponent<Advertisement_Manager> ();

		if (unityAdsInUse) {
			string gameId = null;

			#if UNITY_IOS // If build platform is set to iOS...
		gameId = iosGameId;
			#elif UNITY_ANDROID // Else if build platform is set to Android...
			gameId = androidGameId;
			#endif

			if (string.IsNullOrEmpty (gameId)) { // Make sure the Game ID is set.
				Debug.LogError ("Failed to initialize Unity Ads. Game ID is null or empty.");
			} else if (!Advertisement.isSupported) {
				Debug.LogWarning ("Unable to initialize Unity Ads. Platform not supported.");
			} else if (Advertisement.isInitialized) {
				Debug.Log ("Unity Ads is already initialized.");
			} else {
				Debug.Log (string.Format ("Initialize Unity Ads using Game ID {0} with Test Mode {1}.",
					gameId, enableTestMode ? "enabled" : "disabled"));
				Advertisement.Initialize (gameId, enableTestMode);
			}
		}
	}

	public void ShowInterstitial()
	{
		Debug.Log ("Showing UNITY add interstitial");

		if (unityAdsInUse) 
		{
			if (Advertisement.IsReady())
			{
				// An ad is currently available, so show the interstitial.
				Advertisement.Show();
				Debug.Log ("UNITY ADD IS READY SO WE DISPLAY IT AND CALL MANAGER THAT IT IS SUCCESFULLY DISPLAYED");

				_myAdvertisementManager.AddSuccesfullyDisplayed ();
			} else {
				Debug.LogWarning ("UNITY ADD HAS NOT BEEN LOADED SO WE CALL MANAGER TO SHOW INTERSTITIAL");

				_myAdvertisementManager.ShowInterstitial ();
			}
		}
		else 
		{
			Debug.Log ("UNITYADS INTERSTITIAL IS NOT ALLOWED IN INSPECTOR TO BE DISPLAYED SO WE CALL MANAGER TO SHOW INTERSTITIAL");
			_myAdvertisementManager.ShowInterstitial ();
		}
	}

	public void ShowRewardInterstitial()
	{
		if (unityAdsInUse) 
		{
			if (Advertisement.IsReady("rewardedVideoZone"))
			{
				ShowOptions options = new ShowOptions();
				options.resultCallback = HandleShowResult;
				zoneId = null;

				Advertisement.Show (zoneId, options);
				Debug.Log ("UNITY REWARD VIDEO IS READY SO WE PLAY IT AND CALL MANAGER THAT IT IS SUCCESFULLY PLAYED");

				_myAdvertisementManager.RewardVideoSuccesfullyDisplayed ();
			} 
			else 
			{
				_myAdvertisementManager.ShowRewardVideo ();
				Debug.Log ("UNITY REWARD VIDEO IS NOT READY SO WE CALL MANAGER TO SHOW REWARD VIDEO");
			}
		}
		else 
		{
			Debug.Log ("UNITYADS INTERSTITIAL IS NOT ALLOWED IN INSPECTOR TO BE DISPLAYED SO WE CALL MANAGER TO SHOW REWARD VIDEO");
			_myAdvertisementManager.ShowRewardVideo ();
		}
	}

	private void HandleShowResult (ShowResult result)
	{
		switch (result)
		{
		case ShowResult.Finished:
			Debug.Log ("Video completed. User rewarded " + rewardQty + " credits.");
			Debug.Log ("UNITY ADS HANDLE RESULT EVENT CALL REWARD VIDEO SUCCESFULLY DISPLAYED");
			_myAdvertisementManager.RewardVideoSuccesfullyDisplayed ();
			break;
		case ShowResult.Skipped:
			Debug.LogWarning ("Video was skipped.");
			Debug.Log ("UNITY ADS HANDLE RESULT EVENT CALL REWARD VIDEO SUCCESFULLY DISPLAYED");
			_myAdvertisementManager.RewardVideoSuccesfullyDisplayed ();
			break;
		case ShowResult.Failed:
			Debug.LogError ("Video failed to show.");
			Debug.Log ("UNITY ADS HANDLE RESULT EVENT CALL MANAGER TO SHOW REWARD VIDEO");
			_myAdvertisementManager.ShowRewardVideo ();
			break;
		}
	}

	public void ShowOnlyUnityAdd()
	{
		Debug.Log ("SHOWING UNITY ADD ONLY");

		if (Advertisement.IsReady())
		{
			Advertisement.Show();
		}
	}
}