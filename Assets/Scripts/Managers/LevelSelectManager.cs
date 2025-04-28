using UnityEngine;
using UnityEngine.UI;

public class LevelSelectManager : MonoBehaviour
{
    [SerializeField] private Button[] levelButtons;
    private Color unlockedColor = Color.white;
    private Color lockedColor = Color.gray;
    private Image[] _buttonImages;
    private const string LevelReachedKey = "levelReached";

    private void Start()
    {
        _buttonImages = new Image[levelButtons.Length];
        for (int i = 0; i < levelButtons.Length; i++)
        {
            _buttonImages[i] = levelButtons[i].GetComponent<Image>();
        }

        int levelReached = PlayerPrefs.GetInt(LevelReachedKey, 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i + 1;
            levelButtons[levelIndex].onClick.AddListener(() => GameManager.instance.OnLevelButtonClicked(levelIndex + 1));
            bool isUnlocked = (levelIndex <= levelReached);
            levelButtons[i].interactable = isUnlocked;
            _buttonImages[i].color = isUnlocked ? unlockedColor : lockedColor;
        }
    }
}
