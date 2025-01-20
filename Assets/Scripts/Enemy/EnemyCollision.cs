using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("cup"))
        {
            AudioManager.instance.PlayCollectSound();
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("fish"))
        {
            GetComponent<EnemyMovement>().targetPosition = null;
            Destroy(collision.gameObject);
            Collectable collectable = FindFirstObjectByType<Collectable>();
            UIManager.Instance.UpdateCollectableText(collectable.collectedAmount--, collectable.amountToCollect);
            LeanTween.alpha(gameObject, 0f, 1f).setOnComplete(() =>
            {
                ObjectPoolManager.Instance.ReturnObject(gameObject);
            });
        }
    }
}
