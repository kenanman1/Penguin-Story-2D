using UnityEngine;

public class PenguinAttack : BaseAttack
{
    private PenguinController penguinController;

    protected override void Start()
    {
        base.Start();
        penguinController = GetComponent<PenguinController>();
    }

    public void OnAttack()
    {
        AudioManager.instance.PlayAttackSound();
        attack = true;
        penguinController.Attack();

        Collider2D snowman = Physics2D.OverlapCircle(transform.position, checkRadius, snowmanLayer);
        Collider2D enemy = Physics2D.OverlapCircle(transform.position, checkRadius, enemyLayer);

        if (snowman != null)
            AttackSnowman(snowman.gameObject);
        if (enemy != null)
            AttackEnemy(enemy.gameObject);
    }

    private void OnStopAttack()
    {
        attack = false;
    }

    private void AttackSnowman(GameObject snowman)
    {
        snowman.GetComponent<SnowmanMovement>().Move(transform.position);
    }

    private void AttackEnemy(GameObject enemy)
    {
        enemy.GetComponent<EnemyHealth>().TakeDamage();
    }
}