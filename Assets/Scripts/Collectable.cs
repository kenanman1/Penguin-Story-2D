using System;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public static Action finishedQuest;
    [SerializeField] private GameObject objectToCollect;
    [SerializeField] private GameObject buttonCollect;
    [SerializeField] public string tagToCollect;
    [SerializeField] public int amountToCollect;
    public bool isStarted = false;
    public int collectedAmount = 0;
    private Collider2D objColliderToCollect;
    private float distanceToCollect = 2f;

    private void Update()
    {
        if (isStarted)
        {
            ShowButton();
            UIManager.Instance.ChangeCollectableEnabled(true);
            UIManager.Instance.UpdateCollectableText(collectedAmount, amountToCollect);
        }
        else
            UIManager.Instance.ChangeCollectableEnabled(false);
    }

    private void ShowButton()
    {
        if (IsPlayerNearObjectWithTag(tagToCollect) && amountToCollect > 0)
            buttonCollect.SetActive(true);
        else
            buttonCollect.SetActive(false);
    }

    private bool IsPlayerNearObjectWithTag(string tag)
    {
        objColliderToCollect = Physics2D.OverlapCircle(transform.position, distanceToCollect, LayerMask.GetMask("Collectibles"));

        if (objColliderToCollect != null && objColliderToCollect.CompareTag(tag))
            return true;

        return false;
    }

    public void OnCollect()
    {
        AudioManager.instance.PlayCollectSound();
        Destroy(objColliderToCollect.gameObject);
        collectedAmount++;
        UIManager.Instance.UpdateCollectableText(collectedAmount, amountToCollect);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("finishQuest") && collectedAmount >= amountToCollect && isStarted)
        {
            finishedQuest?.Invoke();
            isStarted = false;

            if (objectToCollect == null)
                return;
            for (int i = 0; i < collectedAmount; i++)
            {
                float randomOffsetX = UnityEngine.Random.Range(-0.5f, 0.5f);
                float randomOffsetY = UnityEngine.Random.Range(-0.5f, 0.5f);

                Vector3 randomizedPosition = collision.transform.position + new Vector3(randomOffsetX, randomOffsetY, 0);
                Instantiate(objectToCollect, randomizedPosition, Quaternion.identity);
            }
        }
    }
}
