using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
public class ADManager : MonoBehaviour
{
    [SerializeField] private MoneySO moneyType = null;    // Scriptable Objects eriþir 
    private BannerView bannerView;
    private InterstitialAd interstitialAd;
    [SerializeField] private string bannerAdId = "";  // Reklam id'si
    [SerializeField] private string interstitialAdId = "";  // Reklam id'si
    void Start()
    {
        MobileAds.Initialize(initStatus => { });
        this.RequestInterstitial();
        this.RequestBanner();
    }
    
    void Update()
    {
        if (moneyType.gameOpen == 3)
        {
            // Her 3 oyunda bir reklam göster
            AdShow();   // Geçiş reklamı göster
            moneyType.gameOpen = 0;
        }
    }
    void RequestBanner()
    {
        // Banner reklamı
        this.bannerView = new BannerView(bannerAdId, AdSize.Banner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        this.bannerView.LoadAd(request);    // Banner reklamı göster
    }
    void RequestInterstitial()
    {
        // Geçiş reklamı
        this.interstitialAd = new InterstitialAd(interstitialAdId);
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitialAd.LoadAd(request);
    }
    public void AdShow()
    {
        interstitialAd.Show();
    }
}
