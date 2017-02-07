using UnityEngine;
using System.Collections;
using ChartboostSDK;

public class InterstitialAdChartboost : MonoBehaviour {

	public bool chartboostUse;
	public int videoReward;

	public GameObject addManager;
	Advertisement_Manager _myAdvertisementManager;

	public delegate void RewardForWatchingChartboost(int value);
	public static event RewardForWatchingChartboost ChartboostInterstitialWatched;

	void Start()
	{
		_myAdvertisementManager = addManager.GetComponent<Advertisement_Manager> ();

		if (chartboostUse) 
		{
			CacheInterstitial ();
			CacheRewardedVideo (CBLocation.Default);
			CacheMoreApps();
		}
	}

	#region Enable/Disable

	void OnEnable() {

		Chartboost.didFailToLoadInterstitial += didFailToLoadInterstitial;
		Chartboost.didDismissInterstitial += didDismissInterstitial;
		Chartboost.didCloseInterstitial += didCloseInterstitial;
		Chartboost.didClickInterstitial += didClickInterstitial;
		Chartboost.didCacheInterstitial += didCacheInterstitial;

		//VIDEO LISTENERS
		Chartboost.didFailToLoadRewardedVideo += didFailToLoadRewardedVideo;
		Chartboost.didDismissRewardedVideo += didDismissRewardedVideo;
		Chartboost.didCloseRewardedVideo += didCloseRewardedVideo;
		Chartboost.didClickRewardedVideo += didClickRewardedVideo;
		Chartboost.didCacheRewardedVideo += didCacheRewardedVideo;
		Chartboost.shouldDisplayRewardedVideo += shouldDisplayRewardedVideo;
		Chartboost.didCompleteRewardedVideo += didCompleteRewardedVideo;
		Chartboost.didDisplayRewardedVideo += didDisplayRewardedVideo;

		//MORE APPS LISTENERS
		Chartboost.didFailToLoadMoreApps += didFailToLoadMoreApps;
		Chartboost.didDismissMoreApps += didDismissMoreApps;
		Chartboost.didCloseMoreApps += didCloseMoreApps;
		Chartboost.didClickMoreApps += didClickMoreApps;
		Chartboost.didCacheMoreApps += didCacheMoreApps;
		Chartboost.shouldDisplayMoreApps += shouldDisplayMoreApps;
		Chartboost.didDisplayMoreApps += didDisplayMoreApps;
	}
	
	void OnDisable() {

		Chartboost.didFailToLoadInterstitial -= didFailToLoadInterstitial;
		Chartboost.didDismissInterstitial -= didDismissInterstitial;
		Chartboost.didCloseInterstitial -= didCloseInterstitial;
		Chartboost.didClickInterstitial -= didClickInterstitial;
		Chartboost.didCacheInterstitial -= didCacheInterstitial;

		//VIDEO LISTENERS
		Chartboost.didFailToLoadRewardedVideo -= didFailToLoadRewardedVideo;
		Chartboost.didDismissRewardedVideo -= didDismissRewardedVideo;
		Chartboost.didCloseRewardedVideo -= didCloseRewardedVideo;
		Chartboost.didClickRewardedVideo -= didClickRewardedVideo;
		Chartboost.didCacheRewardedVideo -= didCacheRewardedVideo;
		Chartboost.shouldDisplayRewardedVideo -= shouldDisplayRewardedVideo;
		Chartboost.didCompleteRewardedVideo -= didCompleteRewardedVideo;
		Chartboost.didDisplayRewardedVideo -= didDisplayRewardedVideo;

		//MORE APPS LISTENERS
		Chartboost.didFailToLoadMoreApps -= didFailToLoadMoreApps;
		Chartboost.didDismissMoreApps -= didDismissMoreApps;
		Chartboost.didCloseMoreApps -= didCloseMoreApps;
		Chartboost.didClickMoreApps -= didClickMoreApps;
		Chartboost.didCacheMoreApps -= didCacheMoreApps;
		Chartboost.shouldDisplayMoreApps -= shouldDisplayMoreApps;
		Chartboost.didDisplayMoreApps -= didDisplayMoreApps;
	}

	#endregion

	#region INTERSTITIAL SECTION

	public void CacheInterstitial()
	{
		if(chartboostUse)
		{
			Chartboost.cacheInterstitial(CBLocation.Default);
		}
	}

	public void ShowInterstitial()
	{
		Debug.Log ("Showing Chartboost add interstitial");
		if (chartboostUse) 
		{
			if (Chartboost.hasInterstitial(CBLocation.Default)) 
			{
				Chartboost.showInterstitial (CBLocation.Default);
				Debug.Log ("CHARTBOOST ADD SHOULD BE DISPLAYED AND WE CALL MANAGER ADD SUCCESFULLY DISPLAYED");
				_myAdvertisementManager.AddSuccesfullyDisplayed ();
			} 
			else 
			{
				Debug.LogWarning ("CHARTBOOST ADD WAS NOT READY SO WE CALL MANAGER TO DISPLAY ANOTHER ADD");
				_myAdvertisementManager.ShowInterstitial ();
			}

		}
		else 
		{
			Debug.Log ("CHARTBOOST INTERSTITIAL IS NOT ALLOWED IN INSPECTOR TO BE DISPLAYED SO WE CALL MANAGER TO SHOW INTERSTITIAL");
			_myAdvertisementManager.ShowInterstitial ();
		}
	}

	void didFailToLoadInterstitial(CBLocation location, CBImpressionError error) 
	{
		Debug.Log(string.Format("didFailToLoadInterstitial: {0} at location {1}", error, location));
		CacheInterstitial();
	}
	
	void didDismissInterstitial(CBLocation location) 
	{
		Debug.Log("didDismissInterstitial: " + location);
		CacheInterstitial();
	}
	
	void didCloseInterstitial(CBLocation location) 
	{
		Debug.Log("didCloseInterstitial: " + location);
		CacheInterstitial();
	}
	
	void didClickInterstitial(CBLocation location) 
	{
		Debug.Log("didClickInterstitial: " + location);
		CacheInterstitial();
	}
	
	void didCacheInterstitial(CBLocation location) 
	{
		Debug.Log("didCacheInterstitial: " + location);
	}
	
	bool shouldDisplayInterstitial(CBLocation location) 
	{
		Debug.Log("shouldDisplayInterstitial: " + location);
		return true;
	}

	#endregion

	#region MORE APPS SECTION

	public void CacheMoreApps()
	{
		if(chartboostUse)
		{
			Chartboost.cacheMoreApps(CBLocation.Default);
		}
	}

	public void ShowMoreApps()
	{
		if (chartboostUse) 
		{
			if (Chartboost.hasMoreApps (CBLocation.Default))
			{
				Chartboost.showMoreApps (CBLocation.Default);
				CacheMoreApps ();
			}
		}
	}

	void didFailToLoadMoreApps(CBLocation location, CBImpressionError error) {
		Debug.Log(string.Format("didFailToLoadMoreApps: {0} at location: {1}", error, location));
		CacheMoreApps ();
	}
	
	void didDismissMoreApps(CBLocation location) {
		Debug.Log(string.Format("didDismissMoreApps at location: {0}", location));
		CacheMoreApps ();
	}
	
	void didCloseMoreApps(CBLocation location) {
		Debug.Log(string.Format("didCloseMoreApps at location: {0}", location));
		CacheMoreApps ();
	}
	
	void didClickMoreApps(CBLocation location) {
		Debug.Log(string.Format("didClickMoreApps at location: {0}", location));
	}
	
	void didCacheMoreApps(CBLocation location) {
		Debug.Log(string.Format("didCacheMoreApps at location: {0}", location));
	}
	
	bool shouldDisplayMoreApps(CBLocation location) {
		Debug.Log(string.Format("shouldDisplayMoreApps at location: {0}", location));
		return true;
	}
	
	void didDisplayMoreApps(CBLocation location){
		Debug.Log("didDisplayMoreApps: " + location);
	}

	#endregion
	
	#region VIDEO SECTION

	/// 
	/// Cache a rewarded video at the given CBLocation.
	/// This method will first check if there is a locally cached rewarded video
	/// for the given CBLocation and, if found, will do nothing. If no locally cached data exists 
	/// the method will attempt to fetch data from the Chartboost API server.
	/// 
	///The location for the Chartboost impression type.
	public void CacheRewardedVideo(CBLocation location) 
	{
		if (chartboostUse) 
		{
			CBExternal.cacheRewardedVideo (location);
		}
	}

	/// 
	/// Present a rewarded video for the given CBLocation.
	/// This method will first check if there is a locally cached rewarded video
	/// for the given CBLocation and, if found, will present using the locally cached data.
	/// If no locally cached data exists the method will attempt to fetch data from the
	/// Chartboost API server and present it.  If the Chartboost API server is unavailable
	/// or there is no eligible rewarded video to present in the given CBLocation this method
	/// is a no-op.
	/// 
	///The location for the Chartboost impression type.
	public void ShowRewardedVideo(CBLocation location) 
	{
		if (chartboostUse) 
		{
			if (Chartboost.hasRewardedVideo(CBLocation.Default)) 
			{
				CBExternal.showRewardedVideo (location);
				Debug.Log ("CHARTBOOST REWARD VIDEO SHOULD BE DISPLAYED AND WE CALL MANAGER REWARD VIDEO SUCCESFULLY DISPLAYED");
				_myAdvertisementManager.RewardVideoSuccesfullyDisplayed ();
			} 
			else 
			{
				Debug.Log ("CHARTBOOST REWARD VIDEO WAS NOT READY TO DISPLAY SO WE CALL MANAGER TO DISPLAY OTHER REWARD VIDEO");
				_myAdvertisementManager.ShowRewardVideo ();
			}
		}
		else 
		{
			Debug.Log ("CHARTBOOST REWARD VIDEO IS NOT ALLOWED IN INSPECTOR TO BE DISPLAYED SO WE CALL MANAGER TO SHOW REWARD VIDEO");
			_myAdvertisementManager.ShowRewardVideo ();
		}
	}

	public void PlayRewardedVideo()
	{
		Debug.Log ("PLAY'S CHARTBOOST REWARD VIDEO");
		ShowRewardedVideo (CBLocation.Default);
	}

	/// 
	/// Determine if a locally cached rewarded video exists for the given CBLocation.
	/// A return value of true here indicates that the corresponding
	/// showRewardedVideo:(CBLocation)location method will present without making
	/// additional Chartboost API server requests to fetch data to present.
	/// 
	///true if the rewarded video is cached, false if not.
	///The location for the Chartboost impression type.
	public static bool hasRewardedVideo(CBLocation location) {
		return CBExternal.hasRewardedVideo(location);
	}

	/// 
	/// Decide if Chartboost SDK will attempt to fetch videos from the Chartboost API servers.
	/// Set to control if Chartboost SDK control if videos should be prefetched.
	/// Default is YES.
	/// 
	///true if Chartboost should prefetch video content, false otherwise.
	public static void setShouldPrefetchVideoContent(bool shouldPrefetch) {
		CBExternal.setShouldPrefetchVideoContent(shouldPrefetch);
	}

	void didFailToLoadRewardedVideo(CBLocation location, CBImpressionError error) 
	{
		Debug.Log(string.Format("didFailToLoadRewardedVideo: {0} at location {1}", error, location));
		CacheRewardedVideo (CBLocation.Default);
	}
	
	void didDismissRewardedVideo(CBLocation location) 
	{
		Debug.Log("didDismissRewardedVideo: " + location);
		CacheRewardedVideo (CBLocation.Default);
	}
	
	void didCloseRewardedVideo(CBLocation location) 
	{
		Debug.Log("didCloseRewardedVideo: " + location);
		CacheRewardedVideo (CBLocation.Default);
	}
	
	void didClickRewardedVideo(CBLocation location) 
	{
		Debug.Log("didClickRewardedVideo: " + location);
		CacheRewardedVideo (CBLocation.Default);
	}
	
	void didCacheRewardedVideo(CBLocation location) 
	{
		Debug.Log("didCacheRewardedVideo: " + location);
	}
	
	bool shouldDisplayRewardedVideo(CBLocation location) 
	{
		Debug.Log("shouldDisplayRewardedVideo: " + location);
		return true;
	}
	
	void didCompleteRewardedVideo(CBLocation location, int reward) {
		Debug.Log(string.Format("didCompleteRewardedVideo: reward {0} at location {1}", reward, location));

		Debug.LogWarning ("ABOUT TO CALL InterstitialWatched DELEGATE");
		
		if(ChartboostInterstitialWatched != null)
		{
			ChartboostInterstitialWatched(videoReward);
			Debug.LogWarning ("ChartboostInterstitialWatched DELEGATE WAS CALLED");
		}
		else Debug.LogWarning ("NOTHING IS ASSIGN TO InterstitialWatched DELEGATE");

		CacheRewardedVideo (CBLocation.Default);
	}
	
	void didDisplayRewardedVideo(CBLocation location){
		Debug.Log("didDisplayRewardedVideo: " + location);
		CacheRewardedVideo (CBLocation.Default);
	}

	#endregion

	public void ShowOnlyChartboostAd()
	{
		Debug.Log ("SHOW ONLY CHARTBOOST AD");
		Chartboost.showInterstitial (CBLocation.Default);
		CacheInterstitial ();
	}
}
