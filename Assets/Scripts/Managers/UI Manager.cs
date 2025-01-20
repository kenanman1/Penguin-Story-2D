using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI collectableText;
    [SerializeField] private TextMeshProUGUI countDownText;
    [SerializeField] private TextMeshProUGUI timerText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void UpdateCollectableText(int collectableCount, int toCollect)
    {
        if (collectableText != null)
        {
            string[] arr = collectableText.text.Split(":");
            collectableText.text = $"{arr[0]}: {collectableCount}/{toCollect}";
        }
    }

    public void UpdateTimerText(float time)
    {
        if (timerText != null)
        {
            timerText.text = $"Time: {time.ToString("F2")}";
            if (time <= 0)
                timerText.gameObject.SetActive(false);
            else
                timerText.gameObject.SetActive(true);
        }
    }

    public void ChangeCollectableEnabled(bool isEnabled)
    {
        collectableText.gameObject.SetActive(isEnabled);
    }

    public void StartCountdown()
    {
        countDownText.gameObject.SetActive(true);
        AnimateText("Ready?", 0f, 3f);
        AnimateText("3", 3f, 1f);
        AnimateText("2", 4.5f, 1f);
        AnimateText("1", 6f, 1f);
        AnimateText("Go!", 7.5f, 1f);
        LeanTween.delayedCall(gameObject, 9f, () =>
        {
            countDownText.gameObject.SetActive(false);
        });
    }

    private void AnimateText(string message, float delay, float duration)
    {
        LeanTween.delayedCall(gameObject, delay, () =>
        {
            countDownText.text = message;

            countDownText.transform.localScale = Vector3.zero;
            LeanTween.scale(countDownText.gameObject, Vector3.one, duration / 2f)
                .setEaseOutBounce();

            countDownText.alpha = 0f;
            LeanTween.value(countDownText.gameObject, 0f, 1f, duration / 2f)
                .setOnUpdate((float alpha) => countDownText.alpha = alpha);
        });
    }
}
