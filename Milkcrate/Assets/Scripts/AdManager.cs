using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    public static AdManager instance;

    string _iOsGameId = "4287110";
    string _androidGameId = "4287111";

    string adType = "";

    public bool usedAd = false;

    private void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        string _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOsGameId
            : _androidGameId;

        adType = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? "Rewarded_iOS"
            : "Rewarded_Android";

        Advertisement.AddListener(this);
        Advertisement.Initialize(_gameId, true);
    }

    public void PlayAd()
    {
        Advertisement.Show(adType);
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log("Ad ready");
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log("AD ERROR: " + message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            PlayerController.instance.Reviwe();
            usedAd = true;
        }
    }
}
