using UnityEngine;
using UnityEngine.InputSystem;

public class PenguinMovement : BaseMovenment
{
    private Vector2 run;

    protected void Update()
    {
        Run();
    }

    public void OnMove(InputValue value)
    {
        run = NormalizeInput(value.Get<Vector2>());
    }

    private Vector2 NormalizeInput(Vector2 input)
    {
        float normalizedX = input.x > 0.5f ? 1 : (input.x < -0.5f ? -1 : 0);
        float normalizedY = input.y > 0.5f ? 1 : (input.y < -0.5f ? -1 : 0);

        return new Vector2(normalizedX, normalizedY);
    }

    private void Run()
    {
        FlipPlayer();
        Vector2 movement = new Vector2(run.x * speed, rigidbody.velocity.y);
        rigidbody.velocity = movement;
        GetComponent<BaseAnimation>().isWalking = rigidbody.velocity.x != 0;
    }

    private void FlipPlayer()
    {
        if (run.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (run.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
    }

    private void OnJump()
    {
        if (IsGrounded())
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpSpeed);
    }
}
