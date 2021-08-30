using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    public static AdManager instance;

    string _iOsGameId = "4287110";
    string _androidGameId = "4287111";

    private void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        string _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOsGameId
            : _androidGameId;

        Advertisement.AddListener(this);
        Advertisement.Initialize(_gameId, true);
    }

    public void PlayAd()
    {
        Advertisement.Show("rewardedVideo");
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
        if (placementId == "rewardedVideo" && showResult == ShowResult.Finished)
        {
            PlayerController.instance.Reviwe();
        }
    }
}
