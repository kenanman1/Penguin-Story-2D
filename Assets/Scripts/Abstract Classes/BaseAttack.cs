using UnityEngine;

public class BaseAttack : MonoBehaviour
{
    protected float checkRadius = 2f;
    public bool attack = false;
    protected LayerMask snowmanLayer;
    protected LayerMask enemyLayer;

    protected virtual void Start()
    {
        snowmanLayer = LayerMask.GetMask("Snowman");
        enemyLayer = LayerMask.GetMask("Enemy");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}

