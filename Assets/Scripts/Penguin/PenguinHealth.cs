using Cinemachine;
using UnityEngine;

public class PenguinHealth : MonoBehaviour
{
    private CinemachineImpulseSource impulse;
    [SerializeField] public int health = 3;
    private float immunity = 1;
    private bool isDead = false;

    private void Start()
    {
        impulse = GetComponent<CinemachineImpulseSource>();
    }

    private void Update()
    {
        if (immunity > 0)
            immunity -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy" && immunity <= 0 && !isDead)
        {
            health--;
            UIManager.Instance.UpdateHealthText(health);
            impulse.GenerateImpulse();
            if (health <= 0)
            {
                Die();
                return;
            }
            else
            {
                LeanTween.alpha(gameObject, 0f, 0.4f).setEase(LeanTweenType.easeInOutSine).setLoopPingPong(3);
                immunity = 5;
            }
        }
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
