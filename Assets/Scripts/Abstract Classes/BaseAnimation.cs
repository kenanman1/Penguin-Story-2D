using System;
using UnityEngine;

public class BaseAnimation : MonoBehaviour
{
    protected BoxCollider2D collider;
    public Animator animator;
    public bool isWalking = false;
    private LayerMask mapLayerMask;
    private LayerMask wallLayerMask;
    public static Action onStopAttack;

    protected virtual void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        mapLayerMask = LayerMask.GetMask("map");
        wallLayerMask = LayerMask.GetMask("wall");
    }

    protected virtual void Update()
    {
        SetRunningAnimation(isWalking);
        HandleJumpAnimation();
    }

    private void HandleJumpAnimation()
    {
        bool isGrounded = collider.IsTouchingLayers(mapLayerMask) || collider.IsTouchingLayers(wallLayerMask);

        animator.SetBool("jump", !isGrounded);
        if (!isGrounded)
        {
            animator.SetBool("walk", false);
        }
    }

    public void Attack(float stopAttackTimer)
    {
        animator.SetTrigger("attack");
        Invoke("StopAttack", stopAttackTimer);
    }

    protected void StopAttack()
    {
        animator.ResetTrigger("attack");
        onStopAttack?.Invoke();
    }

    public void SetRunningAnimation(bool isRunning)
    {
        animator.SetBool("walk", isRunning);
    }
}
