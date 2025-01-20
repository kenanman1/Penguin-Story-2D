using TMPro;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public float health = 100;
    [SerializeField] TextMeshPro damageText;
    private Vector3 lastDirection;
    private Vector3 lastPosition;
    private float maxHealth;

    void Start()
    {
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CheckPosition()
    {
        Vector3 currentDirection = transform.position - lastPosition;
        if (currentDirection != Vector3.zero)
        {
            lastDirection = currentDirection.normalized;
        }
        lastPosition = transform.position;
    }

    public void TakeDamage()
    {
        float damage = Random.Range(5, 20);
        health -= damage;
        damageText.text = damage.ToString();

        Vector3 textScale = damageText.transform.localScale;
        textScale.x = transform.localScale.x > 0 ? Mathf.Abs(textScale.x) : -Mathf.Abs(textScale.x);
        damageText.transform.localScale = textScale;

        FindFirstObjectByType<EffectManager>().DamageTextEffect(damageText);

        if (health <= 0)
        {
            health = maxHealth;
            ObjectPoolManager.Instance.ReturnObject(gameObject);
        }
    }
}
