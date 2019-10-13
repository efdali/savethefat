using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{

    private BannerView reklamObjesi;
    private string bannerId = "ca-app-pub-1387518596299983/5099877734";

    void Start()
    {
        MobileAds.Initialize("ca-app-pub-1387518596299983~7726041072");

        reklamObjesi = new BannerView(bannerId, AdSize.SmartBanner, AdPosition.Bottom);
        AdRequest reklamIstegi = new AdRequest.Builder().Build();
        reklamObjesi.LoadAd(reklamIstegi);
        reklamObjesi.Show();
    }


    void OnDestroy()
    {
        if (reklamObjesi != null)
            reklamObjesi.Destroy();
    }
}
