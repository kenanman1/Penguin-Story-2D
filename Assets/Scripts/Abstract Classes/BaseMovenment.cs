using UnityEngine;

public abstract class BaseMovenment : MonoBehaviour
{
    [SerializeField] public float speed = 10f;
    [SerializeField] protected float jumpSpeed = 5f;

    protected LayerMask mapLayerMask;
    protected LayerMask wallLayerMask;

    protected Collider2D collider;
    protected Rigidbody2D rigidbody;

    protected virtual void Start()
    {
        collider = GetComponent<Collider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
        mapLayerMask = LayerMask.GetMask("map");
        wallLayerMask = LayerMask.GetMask("wall");
    }

    protected bool IsGrounded()
    {
        return collider.IsTouchingLayers(mapLayerMask) || collider.IsTouchingLayers(wallLayerMask);
    }
}
