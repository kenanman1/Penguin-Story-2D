using UnityEngine;

public class PenguinNPCAttack : BaseAttack
{
    private float attackChance = 0.2f;
    private float attackCooldown = 10f;
    private PenguinNPCController penguinNPCController;
    private Vector2 currentScale;

    protected override void Start()
    {
        base.Start();
        BaseAnimation.onStopAttack += AttackCompleted;
        penguinNPCController = GetComponent<PenguinNPCController>();
    }

    private void Update()
    {
        if (!attack)
        {
            Rest();
            CheckForSnowman();
            penguinNPCController.NotAttack();
            return;
        }
        penguinNPCController.Attack();
    }

    private void CheckForSnowman()
    {
        Collider2D snowman = Physics2D.OverlapCircle(transform.position, checkRadius, snowmanLayer);

        if (snowman != null && Random.value < attackChance && attackCooldown <= 0)
        {
            currentScale = transform.localScale;
            attack = true;
            FaceTarget(snowman.transform.position);
        }
    }

    private void FaceTarget(Vector2 targetPosition)
    {
        if (targetPosition.x > transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }

    private void AttackCompleted()
    {
        attack = false;
        attackCooldown = 10f;
        transform.localScale = currentScale;
    }

    private void Rest()
    {
        attackCooldown -= Time.deltaTime;
    }
}
