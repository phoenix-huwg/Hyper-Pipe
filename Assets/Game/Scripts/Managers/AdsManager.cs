using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
// using GoogleMobileAdsMediationTestSuite.Api;
// using Facebook.Unity;

[DefaultExecutionOrder(-92)]
public class AdsManager : Singleton<AdsManager>
{
    private string m_APP_ID = "ca-app-pub-8721698442392956~5641814462";

    private BannerView m_BannerView;
    private string m_BannerId = "ca-app-pub-8721698442392956/1702569451";
    public bool m_BannerLoaded;

    private InterstitialAd interstitial;
    private string m_InterId = "ca-app-pub-8721698442392956/3416952287";
    public bool m_WatchInter;

    private RewardedAd rewardedAd;
    private string m_RewardId = "ca-app-pub-8721698442392956/4408623845";


    public bool openRwdAds;
    private RewardType m_RewardType;


    private void Awake()
    {
        // FB.Init();

        m_BannerLoaded = false;
        m_WatchInter = true;

        // MobileAds.Initialize(initStatus => { });
        // MobileAds.Initialize(m_APP_ID);

        MobileAds.SetiOSAppPauseOnBackground(true);
        // #if UNITY_EDITOR
        //         // Initialize the Google Mobile Ads SDK.
        //         MobileAds.Initialize(HandleInitCompleteAction);
        //         AppLovin.Initialize();
        // #else
        MobileAds.Initialize((initStatus) =>
        {
            Dictionary<string, AdapterStatus> map = initStatus.getAdapterStatusMap();
            foreach (KeyValuePair<string, AdapterStatus> keyValuePair in map)
            {
                string className = keyValuePair.Key;
                AdapterStatus status = keyValuePair.Value;
                switch (status.InitializationState)
                {
                    case AdapterState.NotReady:
                        // The adapter initialization did not complete.
                        MonoBehaviour.print("Adapter: " + className + " not ready.");
                        break;
                    case AdapterState.Ready:
                        // The adapter was successfully initialized.
                        MonoBehaviour.print("Adapter: " + className + " is initialized.");
                        break;
                }
            }
            //MediationTestSuite.OnMediationTestSuiteDismissed += this.HandleMediationTestSuiteDismissed;
            // this.RequestBanner();
            this.RequestInter();
            this.RequestRewardVideo();
        });

        // this.RequestBanner();
        // this.RequestInter();
        // this.RequestRewardVideo();
        // #endif

        // this.RequestBanner();
        // this.RequestInter();
        // this.RequestRewardVideo();
    }

    // private void Start()
    // {

    // }

    // public void _InitilizationOfSDF()
    // {
    //     MobileAds.Initialize(initStatus => { });

    //     this.RequestBanner();
    // }

    // public void Start()
    // {

    // }

    // private void OnEnable()
    // {
    //     StartListenToEvent();
    // }

    // private void OnDisable()
    // {
    //     StopListenToEvent();
    // }

    // private void OnDestroy()
    // {
    //     StopListenToEvent();
    // }

    public void StartListenToEvent()
    {
        // EventManager.AddListener(GameEvent.END_GAME, LoadBanner);
        // EventManager.AddListener(GameEvent.END_GAME, LoadInter);
    }

    public void StopListenToEvent()
    {
        // EventManager.RemoveListener(GameEvent.END_GAME, LoadBanner);
        // EventManager.RemoveListener(GameEvent.END_GAME, LoadInter);
    }

    public void RequestBanner()
    {
        // #if UNITY_ANDROID
        // #elif UNITY_IPHONE
        // #endif
        // if (!Helper.NoAds())
        // {
        AdSize adSize = new AdSize(320, 35);
        this.m_BannerView = new BannerView(m_BannerId, adSize, AdPosition.Bottom);

        // Called when an ad request has successfully loaded.
        this.m_BannerView.OnAdLoaded += this.HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.m_BannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        this.m_BannerView.OnAdOpening += this.HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        this.m_BannerView.OnAdClosed += this.HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.m_BannerView.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;

        AdRequest request = new AdRequest.Builder().Build();

        this.m_BannerView.LoadAd(request);
        // }
    }

    public void RequestInter()
    {
        // #if UNITY_ANDROID
        // #elif UNITY_IPHONE
        // #endif
        // if (!Helper.NoAds())
        // {

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(m_InterId);

        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);

        // }
    }

    public void RequestRewardVideo()
    {
        this.rewardedAd = new RewardedAd(m_RewardId);

        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        // this.rewardedAd.LoadAd(request);
        this.rewardedAd.LoadAd(request);
    }

    public void LoadBanner()
    {
        // if (!Helper.NoAds())
        // {
        // AdSize adSize = new AdSize(320, 35);
        // this.m_BannerView = new BannerView(m_BannerId, adSize, AdPosition.Bottom);
        // this.m_BannerView.Destroy();
        AdRequest request = new AdRequest.Builder().Build();
        this.m_BannerView.LoadAd(request);
        // }
    }

    public void DestroyBanner()
    {
        this.m_BannerView.Hide();
        this.m_BannerView.Destroy();
    }

    public void LoadInter()
    {
        // if (!Helper.NoAds())
        // {
        // this.interstitial.Destroy();
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
        // }
    }

    public void LoadRewardVideo()
    {
        this.rewardedAd = new RewardedAd(m_RewardId);
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        AdRequest request = new AdRequest.Builder().Build();
        this.rewardedAd.LoadAd(request);
    }

    public void WatchInterstitial()
    {
        // if (m_WatchInter && !Helper.NoAds())
        // {
        if (interstitial == null)
        {
            Helper.DebugLog("WatchInterstitialWatchInterstitialWatchInterstitialWatchInterstitialWatchInterstitialWatchInterstitial");
        }
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
            // AnalysticsManager.LogInterAdsShow();
        }
        else
        {
            // RequestInter();
            LoadInter();
        }
        // }
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");

        m_BannerLoaded = true;

        // if (Helper.NoAds())
        // {
        //     DestroyBanner();
        // }
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);

        m_BannerLoaded = false;
        // LoadBanner();

        LoadInter();
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");

        // if (Helper.NoAds())
        // {
        //     DestroyBanner();
        // }
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.Message);

        LoadRewardVideo();
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);

        LoadRewardVideo();
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        // MonoBehaviour.print("HandleRewardedAdClosed event received");
        if (!openRwdAds)
        {
            StartCoroutine(IEProcessRewardVideoClosed());
        }

        LoadRewardVideo();
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);

        if (openRwdAds)
        {
            openRwdAds = false;

            ProcessRewardVideo();
        }
    }

    IEnumerator IEProcessRewardVideoClosed()
    {
        // yield return Yielders.EndOfFrame;
        yield return null;

        switch (m_RewardType)
        {
            case RewardType.GOLD_1:
                EventManager.CallEvent(GameEvent.ADS_GOLD_1_ANIM);
                break;
                // case RewardType.GOLD_2:
                //     EventManager.CallEvent(GameEvent.ADS_GOLD_2_ANIM);
                //     break;
                // case RewardType.CHARACTER_2:
                //     EventManager.CallEvent(GameEvent.ADS_CHARACTER_2_ANIM);
                //     break;
        }
    }

    public void ProcessRewardVideo()
    {
        StartCoroutine(IEProcessRewardVideo());
    }

    IEnumerator IEProcessRewardVideo()
    {
        // yield return Yielders.EndOfFrame;
        yield return null;

        switch (m_RewardType)
        {
            case RewardType.CHARACTER:
                EventManager.CallEvent(GameEvent.ADS_CHARACTER_LOGIC);
                break;
            // case RewardType.CHARACTER_2:
            //     m_WatchInter = false;
            //     EventManager.CallEvent(GameEvent.ADS_CHARACTER_2_LOGIC);
            //     break;
            case RewardType.GOLD_1:
                EventManager.CallEvent(GameEvent.ADS_GOLD_1_LOGIC);
                break;
                // case RewardType.GOLD_2:
                //     m_WatchInter = false;
                //     EventManager.CallEvent(GameEvent.ADS_GOLD_2_LOGIC);
                //     break;
        }
    }

    public void WatchRewardVideo(RewardType _rewardType)
    {
        StartCoroutine(IEWatchRewardVideo(_rewardType));
    }

    IEnumerator IEWatchRewardVideo(RewardType _rewardType)
    {
        // LoadRewardVideo();

        m_RewardType = _rewardType;
        openRwdAds = true;

        // GUIManager.Instance.GetPanelLoadingAds().g_Content.SetActive(true);

        yield return Yielders.Get(1f);
        yield return Yielders.EndOfFrame;

        // GUIManager.Instance.GetPanelLoadingAds().g_Content.SetActive(false);

        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
        else
        {
            LoadRewardVideo();
            // GUIManager.Instance.GetPanelLoadingAds().g_NetworkError.SetActive(true);
            yield return Yielders.Get(0.5f);
            // GUIManager.Instance.GetPanelLoadingAds().g_NetworkError.SetActive(false);
        }
    }

    // public void ShowMediationTestSuite()
    // {
    //     MediationTestSuite.Show();
    // }
}

public enum RewardType
{
    CHARACTER,
    CHARACTER_2,
    GOLD_1,
    GOLD_2,
}