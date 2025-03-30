using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsShowListener, IUnityAdsLoadListener
{
    private string gameId = "5815750";
    private string interstitialAdUnit = "Interstitial_Android";
    public static AdManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Advertisement.Initialize(gameId, true, this); // Второй параметр true = тестовый режим
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Invoke("ShowAd", 5);
    }

    public void ShowAd()
    {
        if (Advertisement.isInitialized)
        {
            Advertisement.Load(interstitialAdUnit, this);
            Advertisement.Show(interstitialAdUnit, this);
        }
        else
        {
            Debug.Log("Реклама не готова!");
        }
    }

    public void OnInitializationComplete()
    {
        print("Initialization complete");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        print($"Initialization failed: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId == interstitialAdUnit && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
            Debug.Log("Реклама успешно показана.");
            // Добавь награду или действие после рекламы
        }
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log($"Реклама {placementId} началась.");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log($"Реклама {placementId} кликнута.");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.LogError($"Ошибка показа рекламы: {error} - {message}");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        print("Ad loaded");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        print($"Ad failed to load: {error.ToString()} - {message}");
    }
}
