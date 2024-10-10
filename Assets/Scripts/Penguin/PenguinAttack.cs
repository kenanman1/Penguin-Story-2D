using UnityEngine;

public class PenguinAttack : BaseAttack
{
    private PenguinController penguinController;

    protected override void Start()
    {
        base.Start();
        penguinController = GetComponent<PenguinController>();
        PenguinAnimation.onStopAttack += StopAttack;
    }

    private void OnAttack()
    {
        attack = true;
        penguinController.Attack();
        AttackSnowman();
    }

    private void StopAttack()
    {
        attack = false;
    }

    public void AttackSnowman()
    {
        Collider2D snowman = Physics2D.OverlapCircle(transform.position, 2f, snowmanLayer);

        if (snowman != null)
            snowman.GetComponent<SnowmanMovement>().Move(gameObject.transform.position);
    }
}