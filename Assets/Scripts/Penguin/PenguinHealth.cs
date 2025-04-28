using Cinemachine;
using UnityEngine;

public class PenguinHealth : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int health = 3;
    [SerializeField] private float immunityDuration = 5f;
    private float maxImmunity;
    private int maxHealth;
    private CinemachineImpulseSource impulse;
    private bool isDead = false;

    private void Start()
    {
        impulse = GetComponent<CinemachineImpulseSource>();
        maxImmunity = immunityDuration;
        maxHealth = health;
    }

    private void Update()
    {
        if (isDead)
            return;
        if (immunityDuration > 0)
            immunityDuration -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy") && immunityDuration <= 0 && !isDead)
        {
            TakeDamage();
            if (maxHealth <= 0)
            {
                Die();
                return;
            }
            else
            {
                LeanTween.color(gameObject, Color.red, 0.3f)
                .setLoopPingPong(3)
                .setEase(LeanTweenType.easeInOutSine);
                immunityDuration = maxImmunity;
            }
        }
    }

    private void TakeDamage()
    {
        maxHealth--;
        UIManager.Instance.UpdateHealthText(health);
        impulse.GenerateImpulse();
    }

    private void Die()
    {
        LeanTween.rotateZ(gameObject, 270f, 2f).setEase(LeanTweenType.easeOutBack);
        LeanTween.move(gameObject, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), 1f).setEase(LeanTweenType.easeInCubic);
        isDead = true;
        Destroy(GetComponent<PenguinMovement>());
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<BaseAnimation>());
        Destroy(GetComponent<Animator>());
    }
}
