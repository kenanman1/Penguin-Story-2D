using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI collectableText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void UpdateCollectableText(int collectableCount, int toCollect)
    {
        collectableText.text = $"Collected: {collectableCount}/{toCollect}";
    }

    public void ChangeCollectableEnabled(bool isEnabled)
    {
        collectableText.gameObject.SetActive(isEnabled);
    }
}
