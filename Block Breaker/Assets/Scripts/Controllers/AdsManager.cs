using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] Player player;

    [SerializeField] GameObject overlayButtons;
    [SerializeField] GameObject overlayMenu;

    [SerializeField] GameObject lastChanceButton;
    [SerializeField] GameObject lastChanceText;
    [SerializeField] GameObject tryAgainText;

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

        //TESTING ONLY
        lastChanceButton.SetActive(false);
        lastChanceText.SetActive(false);
        tryAgainText.SetActive(true);

        overlayButtons.SetActive(false);
        overlayMenu.SetActive(false);
        Time.timeScale = 1.0f;

        Level.Instance.StartInputDelay();
        player.DefaultBallSetup();
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId == rewardAd && showResult == ShowResult.Finished)
        {
            lastChanceButton.SetActive(false);
            lastChanceText.SetActive(false);
            tryAgainText.SetActive(true);

            overlayButtons.SetActive(false);
            overlayMenu.SetActive(false);
            Time.timeScale = 1.0f;

            Level.Instance.StartInputDelay();
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
