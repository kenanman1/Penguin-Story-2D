using UnityEngine;

public class NPCMovement : BaseMovement
{
    [Header("Timers")]
    [SerializeField] private float minWalkTime = 5f;
    [SerializeField] private float maxWalkTime = 20f;
    [SerializeField] private float minStayTime = 5f;
    [SerializeField] private float maxStayTime = 20f;

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
            walkTime -= Time.fixedDeltaTime;
            penguinBaseAnimation.isWalking = true;
            transform.Translate(speed * Time.fixedDeltaTime, 0, 0);
        }
        else if (stayTime > 0)
        {
            penguinBaseAnimation.isWalking = false;
            stayTime -= Time.fixedDeltaTime;
        }
        else
            ResetTimers();
    }

    private void ResetTimers()
    {
        walkTime = Random.Range(minWalkTime, maxWalkTime);
        stayTime = Random.Range(minStayTime, maxStayTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        speed *= -1;
    }
}
