using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private string[] dialogueLines;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Button continueButton;
    [SerializeField] private bool isQuest;
    [SerializeField] private string questName;
    private int index = 0;
    private bool isPlayerInRange;
    private bool isFinished = false;

    private void Update()
    {
        if (isPlayerInRange && !isFinished)
        {
            if (!dialoguePanel.activeInHierarchy)
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
            else if (dialogueText.text == dialogueLines[index])
            {
                continueButton.gameObject.SetActive(true);
                continueButton.onClick.AddListener(() => OnClick());
            }
        }
    }

    private void ZeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
        continueButton.gameObject.SetActive(false);
    }

    private IEnumerator Typing()
    {
        continueButton.gameObject.SetActive(false);
        dialogueText.text = "";
        foreach (char letter in dialogueLines[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.06f);
        }
    }

    public void NextLine()
    {
        if (index < dialogueLines.Length - 1)
        {
            index++;
            StopAllCoroutines();
            StartCoroutine(Typing());
        }
        else
        {
            ZeroText();
            isFinished = true;
            if (isQuest)
            {
                FindObjectsByType<Collectable>(FindObjectsSortMode.None).FirstOrDefault(p => p.tagToCollect == questName).isStarted = true;
            }
        }
    }

    public void OnClick()
    {
        if (dialogueText.text == dialogueLines[index])
        {
            NextLine();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            StopAllCoroutines();
            ZeroText();
        }
    }
}
