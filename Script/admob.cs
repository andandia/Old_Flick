using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
using System;


/// <summary>
/// admob管理
/// </summary>
public class admob : MonoBehaviour
{
    public string Android_Banner;
    public string Android_Interstitial;
    public string ios_Banner;
    public string ios_Interstitial;

    private InterstitialAd interstitial;
    private AdRequest request;
    private static admob instance;

    public delegate void CallBack();

    private CallBack callback;

    public BannerView bannerView;

    void Start()
    {
        instance = this;

        // RequestInterstitial();
       
           // RequestBanner(call_pos);
        
        
    }

    public void RequestBanner(int position)
    {
#if UNITY_ANDROID
        string adUnitId = Android_Banner;
#elif UNITY_IOS
            string adUnitId = ios_Banner;
#endif
        if (position == 1)//バナーが下
        {
            bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
            AdRequest request = new AdRequest.Builder()
                    //.AddTestDevice("CEE73F65BCE4660D4BA3CE319889C572")//製品では取る
                    .Build();
            bannerView.LoadAd(request);
        }
        else if (position == 2)//バナーが上
        {
            bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
            AdRequest request = new AdRequest.Builder()
                    //.AddTestDevice("CEE73F65BCE4660D4BA3CE319889C572")
                    .Build();
            bannerView.LoadAd(request);
        }

        
    }

    public void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = Android_Interstitial;
#elif UNITY_IOS
            string adUnitId = ios_Interstitial;
#endif

        interstitial = new InterstitialAd(adUnitId);
        interstitial.OnAdClosed += delegate (object sender, EventArgs args)
        {
            interstitial.Destroy();
            RequestInterstitial();

#if UNITY_IOS
                    if (callback != null)
                    {
                            callback();
                    }
#endif
        };

        request = new AdRequest.Builder()
                //.AddTestDevice("CEE73F65BCE4660D4BA3CE319889C572")
                .Build();

        interstitial.LoadAd(request);
    }

    public static void DisplayInterstitial(CallBack cb = null)
    {
        instance.displayInterstitial(cb);
    }

    private void displayInterstitial(CallBack cb = null)
    {
        callback = cb;

        if (interstitial.IsLoaded())
        {
            interstitial.Show();
#if !UNITY_IOS || UNITY_EDITOR
            if (callback != null)
            {
                callback();
            }
#endif
        }
        else
        {
            if (callback != null)
            {
                callback();
            }
        }
    }


    public void destroy()
    {
        bannerView.Destroy();
    }

   
}