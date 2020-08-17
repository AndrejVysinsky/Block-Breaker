using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] Player player;

    [SerializeField] GameObject overlayButtons;
    [SerializeField] GameObject overlayMenu;

    private string regularAd = "video";
    private string rewardAd = "rewardedVideo";

    void Start()
    {
        Advertisement.Initialize("3772019", true);
    }

    public void ShowRewardAd()
    {
        StartCoroutine(ShowAd(rewardAd));
    }

    public void ShowRegularAd()
    {
        StartCoroutine(ShowAd(regularAd));
    }

    private IEnumerator ShowAd(string placement)
    {
        while (!Advertisement.IsReady())
            yield return null;

        //Advertisement.Show(placement);

            overlayButtons.SetActive(false);
            overlayMenu.SetActive(false);
            Time.timeScale = 1.0f;

            player.DefaultBallSetup();
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId == rewardAd && showResult == ShowResult.Finished)
        {
            overlayButtons.SetActive(false);
            overlayMenu.SetActive(false);
            Time.timeScale = 1.0f;

            player.DefaultBallSetup();
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
    }

    public void OnUnityAdsDidError(string message)
    {
    }

    public void OnUnityAdsDidStart(string placementId)
    {
    }

    
}
