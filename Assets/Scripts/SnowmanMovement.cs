using UnityEngine;

public class SnowmanMovement : BaseMovenment
{
    private Vector3 targetPosition;
    private Vector2 direction;
    private float movementDistance = 1f;

    public void Move(Vector2 attackerPosition)
    {
        LeanTween.cancel(gameObject);
        if (transform.position.x > attackerPosition.x)
        {
            direction = Vector2.right;
            targetPosition = transform.position + new Vector3(1, 0, 0);
        }
        else
        {
            direction = Vector2.left;
            targetPosition = transform.position + new Vector3(-1, 0, 0);
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, movementDistance, wallLayerMask);

        if (hit.collider == null)
            LeanTween.move(gameObject, targetPosition, speed);
    }
}