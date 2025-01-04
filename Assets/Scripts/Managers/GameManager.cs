using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject portal;
    [SerializeField] private List<GameObject> objectsToDestroy;

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
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
            SceneManager.LoadScene(0);
    }

    public void ButtonNextLevel()
    {
        StartCoroutine(LoadNextLevel());
    }

    public void DestroyObject()
    {
        if (objectsToDestroy.Count > 0)
        {
            Destroy(objectsToDestroy[0]);
            objectsToDestroy.RemoveAt(0);
        }
    }
}
