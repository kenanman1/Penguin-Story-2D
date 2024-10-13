using UnityEngine;

public class PenguinMovement : BaseMovenment
{
    private Vector2 run;

    protected void Update()
    {
        Run();
    }

    private void Run()
    {
        FlipPlayer();
        Vector2 movement = new Vector2(run.x, rigidbody.velocity.y);
        rigidbody.velocity = movement;
        GetComponent<BaseAnimation>().isWalking = rigidbody.velocity.x != 0;
    }

    private void FlipPlayer()
    {
        if (run.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (run.x > 0)
            transform.localScale = Vector2.one;
    }

    public void OnMoveToRight()
    {
        run = new Vector2(speed, 0);
    }

    public void OnMoveToLeft()
    {
        run = new Vector2(-speed, 0);
    }

    public void OnStop()
    {
        run = Vector2.zero;
    }

    public void OnJump()
    {
        if (IsGrounded())
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpSpeed);
    }
}
