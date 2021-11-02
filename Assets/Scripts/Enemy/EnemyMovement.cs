using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 1.0f;
    public float groundCheckDistance = 1.0f;
    public float wallCheckDistance = 0.5f;
    public float checkOffset = 0.5f;
    public LayerMask ground;
    private int currDirection = -1;
    private Rigidbody2D rbd;
    // Start is called before the first frame update
    void Start()
    {
        rbd = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit2D hitLeft = Physics2D.Raycast(new Vector2(transform.position.x - checkOffset, transform.position.y + 0.5f), Vector2.down, groundCheckDistance, ground); // Ground is layer 3
        Debug.DrawRay(new Vector2(transform.position.x - checkOffset, transform.position.y + 0.5f), groundCheckDistance * Vector2.down, Color.red);
        RaycastHit2D hitRight = Physics2D.Raycast(new Vector2(transform.position.x + checkOffset, transform.position.y + 0.5f), Vector2.down, groundCheckDistance, ground); // Ground is layer 3
        Debug.DrawRay(new Vector2(transform.position.x + checkOffset, transform.position.y + 0.5f), groundCheckDistance * Vector2.down, Color.green);

        RaycastHit2D wallHitLeft = Physics2D.Raycast(transform.position, Vector2.left, wallCheckDistance, ground); // Ground is layer 3
        Debug.DrawRay(transform.position, wallCheckDistance * Vector2.right, Color.red);
        RaycastHit2D wallHitRight = Physics2D.Raycast(transform.position, Vector2.right, wallCheckDistance, ground); // Ground is layer 3
        Debug.DrawRay(transform.position, wallCheckDistance * Vector2.left, Color.green);

        if (hitLeft.collider == null || wallHitLeft.collider != null)
        {
            currDirection = 1;
        }
        if (hitRight.collider == null || wallHitRight.collider != null)
        {
            currDirection = -1;
        }

        rbd.velocity = new Vector2(speed * currDirection, rbd.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            FlipDirections();
        }
    }

    public void FlipDirections()
    {
        currDirection *= -1;
    }
}
