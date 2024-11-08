using UnityEngine;

public class PenguinNPCMovenment : BaseMovenment
{
    private bool isFacingRight = true;
    public float stayTime;
    public float walkTime;
    public bool stop = false;
    private BaseAnimation penguinBaseAnimation;

    protected override void Start()
    {
        base.Start();
        penguinBaseAnimation = GetComponent<BaseAnimation>();
        stayTime = Random.Range(10, 20);
        walkTime = Random.Range(10, 20);
    }

    protected void FixedUpdate()
    {
        if (!stop)
            Walk();
    }

    public void StopMovement()
    {
        stop = true;
        penguinBaseAnimation.isWalking = false;
    }

    public void ResumeMovement()
    {
        stop = false;
    }

    private void Walk()
    {
        if (walkTime > 0)
        {
            penguinBaseAnimation.isWalking = true;
            if (isFacingRight)
            {
                transform.localScale = new Vector3(1, 1, 1);
                transform.Translate(speed * Time.deltaTime, 0, 0);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
                transform.Translate(-speed * Time.deltaTime, 0, 0);
            }
        }
        Stay();
    }

    private void Stay()
    {
        if (walkTime > 0)
            walkTime -= Time.deltaTime;
        else if (stayTime > 0)
        {
            penguinBaseAnimation.isWalking = false;
            stayTime -= Time.deltaTime;
        }
        else
            ResetTimers();
    }

    private void ResetTimers()
    {
        walkTime = Random.Range(10, 20);
        stayTime = Random.Range(10, 20);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isFacingRight = !isFacingRight;
    }
}
