using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject portal;
    [SerializeField] private List<GameObject> objectsToDestroy;
    [SerializeField] private GameObject canvasLevels;
    [SerializeField] private GameObject canvasMainMenu;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        if (IsAnyDialoguePending())
            return;

        if (portal != null)
            portal.SetActive(true);
    }

    private bool IsAnyDialoguePending()
    {
        Dialogue[] dialogues = FindObjectsByType<Dialogue>(FindObjectsSortMode.None);
        foreach (Dialogue dialogue in dialogues)
        {
            if (dialogue.hasToFinishQuest)
                return true;
        }
        return false;
    }

    public IEnumerator LoadNextLevel(bool loadSpecificLevel = false, int levelIndex = 1)
    {
        yield return new WaitForSeconds(1);
        int highestLevelReached = PlayerPrefs.GetInt("levelReached", 0);
        int targetLevelIndex = loadSpecificLevel ? levelIndex : SceneManager.GetActiveScene().buildIndex + 1;
        if (targetLevelIndex > highestLevelReached)
            PlayerPrefs.SetInt("levelReached", targetLevelIndex);

        if (targetLevelIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(targetLevelIndex);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public void DestroyObject()
    {
        if (objectsToDestroy.Count > 0)
        {
            Destroy(objectsToDestroy[0]);
            objectsToDestroy.RemoveAt(0);
        }
    }

    public void ButtonNextLevel()
    {
        StartCoroutine(LoadNextLevel());
    }

    public void ButtonSelectLevelCanvas()
    {
        bool isMainMenuActive = canvasMainMenu.activeSelf;
        canvasMainMenu.SetActive(!isMainMenuActive);
        canvasLevels.SetActive(isMainMenuActive);
    }

    public void OnLevelButtonClicked(int levelIndex)
    {
        StartCoroutine(LoadNextLevel(true, levelIndex));
    }

    public void BackToMenuButton()
    {
        StartCoroutine(LoadNextLevel(true, 0));
    }

    public void ButtonExit()
    {
        Application.Quit();
    }
}
