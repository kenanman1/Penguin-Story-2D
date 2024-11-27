using UnityEngine;

public class NPCMovenment : BaseMovenment
{
    public float stayTime;
    public float walkTime;
    public bool stop = false;
    private BaseAnimation penguinBaseAnimation;

    protected override void Start()
    {
        base.Start();
        penguinBaseAnimation = GetComponent<BaseAnimation>();
        ResetTimers();
    }

    protected void FixedUpdate()
    {
        if (!stop)
            Walk();
    }

    public void StopMovement()
    {
        stop = true;
    }

    public void ResumeMovement()
    {
        stop = false;
    }

    private void Walk()
    {
        if (walkTime > 0)
        {
            walkTime -= Time.deltaTime;
            penguinBaseAnimation.isWalking = true;
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
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
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        speed *= -1;
    }
}
