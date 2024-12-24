using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject portal;

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        Dialogue[] dialogues = FindObjectsByType<Dialogue>(FindObjectsSortMode.None);
        foreach (Dialogue dialogue in dialogues)
        {
            if (dialogue.hasToFinishQuest)
                return;
        }

        if (portal != null)
            portal.SetActive(true);
    }

    public IEnumerator LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
            SceneManager.LoadScene(0);
    }

    public void ButtonNextLevel()
    {
        StartCoroutine(LoadNextLevel());
    }
}
