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

    private int timeBetweenAdsInMinutes = 5;

    void Start()
    {
        Advertisement.Initialize("3772019", testMode: false);
        Advertisement.AddListener(this);
    }

    public void ShowRewardAd()
    {
        StartCoroutine(ShowAd(rewardAd));
    }

    public void ShowRegularAd()
    {
        var appDataManager = ApplicationDataManager.Instance;

        if (appDataManager.HideAds() == false && IsTimeForRegularAd())
        {
            appDataManager.AdsShown++;
            StartCoroutine(ShowAd(regularAd));
        }
    }

    private IEnumerator ShowAd(string placement)
    {
        while (!Advertisement.IsReady())
            yield return null;

        Advertisement.Show(placement);
    }

    private bool IsTimeForRegularAd()
    {
        int sessionTimeInMinutes = (int)(Time.time / 60);
        int numberOfShownAds = ApplicationDataManager.Instance.AdsShown;

        sessionTimeInMinutes -= timeBetweenAdsInMinutes * numberOfShownAds;

        if (sessionTimeInMinutes >= timeBetweenAdsInMinutes)
        {
            return true;
        }

        return false;
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

    private void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }
}
