using UnityEngine;

public class EnemyMovement : BaseMovenment
{
    public GameObject targetPosition;

    protected override void Start()
    {
        base.Start();
        targetPosition = GameObject.FindGameObjectWithTag("target");
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (targetPosition != null)
        {
            float step = speed * Time.deltaTime;
            Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition.transform.position, step);

            if (newPosition.x > transform.position.x)
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            
            else if (newPosition.x < transform.position.x)
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            transform.position = newPosition;
        }
        else
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }
}
