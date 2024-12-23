using UnityEngine;

public class BaseAnimation : MonoBehaviour
{
    private Collider2D baseCollider;
    protected BoxCollider2D collider;
    public Animator animator;
    public bool isWalking = false;
    private LayerMask mapLayerMask;
    private LayerMask wallLayerMask;

    protected virtual void Start()
    {
        baseCollider = GetComponent<Collider2D>();
        collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        mapLayerMask = LayerMask.GetMask("map");
        wallLayerMask = LayerMask.GetMask("wall");
    }

    protected virtual void Update()
    {
        SetRunningAnimation(isWalking);
        HandleJumpAnimation();
        if (baseCollider != null && baseCollider.IsTouchingLayers(LayerMask.GetMask("Water")))
        {
            GetComponent<SpriteRenderer>().color = Color.cyan;
        }
        else
            GetComponent<SpriteRenderer>().color = Color.white;
    }

    protected void HandleJumpAnimation()
    {
        if (collider == null)
            return;

        bool isGrounded = collider.IsTouchingLayers(mapLayerMask) || collider.IsTouchingLayers(wallLayerMask);

        animator.SetBool("jump", !isGrounded);
        if (!isGrounded)
        {
            animator.SetBool("walk", false);
        }
    }

    public virtual void Attack(float stopAttackTimer)
    {
        animator.SetBool("attack", true);
        Invoke("StopAttack", stopAttackTimer);
    }

    public virtual void StopAttack()
    {
        animator.SetBool("attack", false);
    }

    public void SetRunningAnimation(bool isRunning)
    {
        animator.SetBool("walk", isRunning);
    }
}
