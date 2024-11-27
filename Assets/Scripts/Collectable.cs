using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private GameObject objectToCollect;
    [SerializeField] private GameObject buttonCollect;
    [SerializeField] public string tagToCollect;
    [SerializeField] private int amountToCollect;
    public bool isStarted = false;
    private int collectedAmount = 0;
    private Collider2D objColliderToCollect;
    private float distanceToCollect = 2f;

    private void Update()
    {
        if (objColliderToCollect == null)
            print("null");
        if (isStarted)
        {
            ShowButton();
            UIManager.Instance.ChangeCollectableEnabled(true);
        }
        else
        {
            UIManager.Instance.ChangeCollectableEnabled(false);
        }
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
        Destroy(objColliderToCollect.gameObject);
        collectedAmount++;
        UIManager.Instance.UpdateCollectableText(collectedAmount, amountToCollect);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("finishQuest") && amountToCollect == collectedAmount && isStarted)
        {
            isStarted = false;
            for (int i = 0; i < amountToCollect; i++)
            {
                //randomize the position of the object to collect

                Instantiate(objectToCollect, transform.position, Quaternion.identity);
            }

        }
    }
}
