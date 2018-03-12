using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class AdScript : MonoBehaviour
{

    bool hasShownAdOneTime;

    // Use this for initialization
    void Start()
    {
        //Request Ad
        RequestInterstitialAds();
    }

    void Update()
    {
        if (GameScript.isGameOver)
        {
            if (!hasShownAdOneTime)
            {
                hasShownAdOneTime = true;
                Invoke("showInterstitialAd", 3.0f);
            }
        }
    }

    public void showInterstitialAd()
    {
        //Show Ad
        if (interstitial.IsLoaded())
        {
            interstitial.Show();

            //Stop Sound
            //

            Debug.Log("SHOW AD XXX");
        }

    }

    InterstitialAd interstitial;
    private void RequestInterstitialAds()
    {
        string adID = "ca-app-pub-2640689103025541/5395499121";

#if UNITY_ANDROID
        string adUnitId = adID;
#elif UNITY_IOS
        string adUnitId = adID;
#else
        string adUnitId = adID;
#endif

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);

        //***Test***
        AdRequest request = new AdRequest.Builder()
       .AddTestDevice(AdRequest.TestDeviceSimulator)       // Simulator.
       .AddTestDevice("C9946BE5CA92B1E4EA4F93398364018E")  // My test device.
       .Build();

        //***Production***
        //AdRequest request = new AdRequest.Builder().Build();

        //Register Ad Close Event
        interstitial.OnAdClosed += Interstitial_OnAdClosed;

        // Load the interstitial with the request.
        interstitial.LoadAd(request);

        Debug.Log("AD LOADED XXX");

    }

    //Ad Close Event
    private void Interstitial_OnAdClosed(object sender, System.EventArgs e)
    {
        //Resume Play Sound

    }
}