using UnityEngine;

public class PenguinController : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private PenguinAnimation penguinanimation;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        penguinanimation = GetComponent<PenguinAnimation>();
    }

    public void Attack()
    {
        penguinanimation.Attack(0.5f);
    }
}
