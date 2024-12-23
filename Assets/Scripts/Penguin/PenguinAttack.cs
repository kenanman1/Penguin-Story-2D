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
        if (snowman != null)
            AttackSnowman(snowman.gameObject);
    }

    private void OnStopAttack()
    {
        attack = false;
    }

    private void AttackSnowman(GameObject snowman)
    {
        snowman.GetComponent<SnowmanMovement>().Move(transform.position);
    }
}