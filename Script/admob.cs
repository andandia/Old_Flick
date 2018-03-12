using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// admob管理
/// </summary>
public class Admob : MonoBehaviour
{

    private string Android_BannerId = "ca-app-pub-2640689103025541/2738794711";
    private string Android_InterstitialId = "ca-app-pub-2640689103025541/5395499121";
    private string ios_BannerId;
    private string ios_InterstitialId;


    BannerView bannerAd;
    InterstitialAd interstitial;

    /// <summary>
    /// 全面広告が表示されたかのフラグ
    /// </summary>
    bool AdDisplayed;


    /// <summary>
    /// バナー広告の表示
    /// </summary>
    /// <param name="AdPos">1…上、2…下</param>
    public void showBannerAd(int AdPos)
    {

#if UNITY_ANDROID
        string adUnitId = Android_BannerId;
#elif UNITY_IOS
        string adUnitId = ios_BannerId;
#else
        string adUnitId = adID;
#endif



        //***For Testing in the Device***
        /*
        AdRequest request = new AdRequest.Builder()
       .AddTestDevice(AdRequest.TestDeviceSimulator)       // Simulator.
       .AddTestDevice("C9946BE5CA92B1E4EA4F93398364018E")  // My test device.
       .Build();
       */

        //***For Production When Submit App***
        AdRequest request = new AdRequest.Builder().Build();

        if (AdPos == 1)//バナーが上
        {
            bannerAd = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Top);
        }else if(AdPos == 2)
        {
            bannerAd = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
        }

        bannerAd.LoadAd(request);
    }





    public void showInterstitialAd()
    {
        //Show Ad
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
            Debug.Log("SHOW AD XXX");
        }

    }

    
    public void RequestInterstitialAds()
    {

#if UNITY_ANDROID
        string adUnitId = Android_InterstitialId;
#elif UNITY_IOS
        string adUnitId = adID;
#else
        string adUnitId = adID;
#endif

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);

        //***テスト用***
        /*
        AdRequest request = new AdRequest.Builder()
       .AddTestDevice(AdRequest.TestDeviceSimulator)       // Simulator.
       .AddTestDevice("C9946BE5CA92B1E4EA4F93398364018E")  // My test device.
       .Build();
       */

        //***Production***
        AdRequest request = new AdRequest.Builder().Build();

        //Register Ad Close Event
        interstitial.OnAdClosed += Interstitial_OnAdClosed;

        // Load the interstitial with the request.
        interstitial.LoadAd(request);

        Debug.Log("AD LOADED XXX");

    }

    //Ad Close Event
    private void Interstitial_OnAdClosed(object sender, System.EventArgs e)
    {
        interstitial.Destroy();
    }





    public void Bannerdestroy()
    {
        bannerAd.Destroy();
    }




}