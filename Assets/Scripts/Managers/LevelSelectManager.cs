using UnityEngine;
using UnityEngine.UI;

public class LevelSelectManager : MonoBehaviour
{
    [SerializeField] private Button[] levelButtons;

    private void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int index = i;
            levelButtons[index].onClick.AddListener(() => GameManager.instance.OnLevelButtonClicked(index + 1));
            if (i + 1 <= levelReached)
            {
                levelButtons[i].interactable = true;
                levelButtons[i].GetComponent<Image>().color = Color.white;
            }
            else
            {
                levelButtons[i].interactable = false;
                levelButtons[i].GetComponent<Image>().color = Color.gray;
            }
        }
    }
}
