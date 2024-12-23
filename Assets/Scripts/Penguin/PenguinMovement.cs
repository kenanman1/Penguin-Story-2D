using UnityEngine;

public class PenguinMovement : BaseMovenment
{
    private bool hasPlayedSplashSound = false;
    private Vector2 run;

    protected void FixedUpdate()
    {
        Run();
    }

    private void Run()
    {
        FlipPlayer();
        Vector2 movement = new Vector2(run.x, rigidbody.linearVelocity.y);
        rigidbody.linearVelocity = movement;
        GetComponent<BaseAnimation>().isWalking = rigidbody.linearVelocity.x != 0;
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
            rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, jumpSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collider.IsTouchingLayers(LayerMask.GetMask("Water")) && !hasPlayedSplashSound)
        {
            AudioManager.instance.PlayWaterSplashSound();
            hasPlayedSplashSound = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collider.IsTouchingLayers(LayerMask.GetMask("Water")))
            hasPlayedSplashSound = false;
    }
}
