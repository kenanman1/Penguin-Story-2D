using UnityEngine;

public class PenguinController : MonoBehaviour
{
    [SerializeField] private float attackTimer = 0.3f;
    private Rigidbody2D rigidbody;
    private PenguinAnimation penguinanimation;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        penguinanimation = GetComponent<PenguinAnimation>();
    }

    public void Attack()
    {
        penguinanimation.Attack(attackTimer);
    }
}
