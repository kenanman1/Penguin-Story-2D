using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private string[] dialogueLines;
    [SerializeField] private string[] failedQuestDialogueLines;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Button continueButton;
    [SerializeField] private bool isQuest;
    [SerializeField] private string questName;
    [SerializeField] public bool hasToFinishQuest;
    [SerializeField] private PlayableDirector playableDirector;
    [SerializeField] private bool hasToDestroyObject;

    private int currentLineIndex = 0;
    private bool isPlayerInRange;
    private bool isDialogueFinished = false;
    private bool isFailedDialogueFinished = false;

    private void Start()
    {
        Collectable.finishedQuest += () => hasToFinishQuest = false;
        continueButton.onClick.AddListener(HandleContinueButton);
    }

    private void Update()
    {
        if (!isPlayerInRange || dialoguePanel.activeInHierarchy || ShouldSkipDialogue())
            return;

        dialoguePanel.SetActive(true);
        StartCoroutine(TypeLine());
    }

    private bool ShouldSkipDialogue()
    {
        return (isDialogueFinished && failedQuestDialogueLines.Length == 0) ||
               (hasToFinishQuest && isFailedDialogueFinished) ||
               (isDialogueFinished && isFailedDialogueFinished) ||
               (!hasToFinishQuest && isDialogueFinished);
    }

    private IEnumerator TypeLine()
    {
        continueButton.gameObject.SetActive(false);
        dialogueText.text = "";

        string[] currentLines = hasToFinishQuest ? failedQuestDialogueLines : dialogueLines;
        foreach (char letter in currentLines[currentLineIndex].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.06f);
        }

        continueButton.gameObject.SetActive(true);
    }

    private void HandleContinueButton()
    {
        if (failedQuestDialogueLines.Length > 0 && hasToFinishQuest && dialogueText.text == failedQuestDialogueLines[currentLineIndex])
            AdvanceLine();
        else if (dialogueText.text == dialogueLines[currentLineIndex])
            AdvanceLine();
    }

    private void AdvanceLine()
    {
        string[] currentLines = hasToFinishQuest ? failedQuestDialogueLines : dialogueLines;

        if (currentLineIndex < currentLines.Length - 1)
        {
            currentLineIndex++;
            StopAllCoroutines();
            StartCoroutine(TypeLine());
        }
        else
            EndDialogue();
    }

    private void EndDialogue()
    {
        ResetDialogueState();
        if (hasToDestroyObject)
            GameManager.instance.DestroyObject();
        if (hasToFinishQuest)
            isFailedDialogueFinished = true;
        else
        {
            isDialogueFinished = true;
            StartQuestOrTimeline();
        }
    }

    private void ResetDialogueState()
    {
        dialogueText.text = "";
        currentLineIndex = 0;
        dialoguePanel.SetActive(false);
        continueButton.gameObject.SetActive(false);
    }

    private void StartQuestOrTimeline()
    {
        if (isQuest)
        {
            var questCollectable = FindObjectsByType<Collectable>(FindObjectsSortMode.None)
                .FirstOrDefault(p => p.tagToCollect == questName);
            if (questCollectable != null)
                questCollectable.isStarted = true;
        }

        if (playableDirector != null)
            playableDirector.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isPlayerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            StopAllCoroutines();
            ResetDialogueState();
        }
    }
}
