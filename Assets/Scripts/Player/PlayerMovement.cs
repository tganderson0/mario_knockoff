using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rbd;
    public float maxSpeed = 1.0f;
    public float jumpHeight = 1.0f;
    public float groundCheckDistance = 0.5f;
    public float knockbackStunDuration;
    public float launchSpeed;
    public LayerMask ground;

    private float timerDuration;

    // Start is called before the first frame update
    void Start()
    {
        rbd = gameObject.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timerDuration > 0)
        {
            timerDuration -= Time.fixedDeltaTime;
        }
        else
        {
            if (Input.GetButton("Jump"))
            {
                Jump();
            }

            if (Input.GetAxis("Horizontal") != 0)
            {
                SideMove();
            }
        }
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + 0.5f), groundCheckDistance * Vector2.down, Color.red);
    }

    private void Jump()
    {
        // hmax = Vy² / (2 * g)
        // Vy² = hmax * 2 * g
        // Vy = sqrt(hmax * 2 * g)
       
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.5f), Vector2.down, groundCheckDistance, ground); // Ground is layer 3

        if (hit.collider != null)
        {
            float yVelocity = Mathf.Sqrt(jumpHeight * 2 * (-Physics.gravity.y * rbd.gravityScale));
            rbd.velocity = new Vector2(rbd.velocity.x, yVelocity);
        }
        
    }

    private void SideMove()
    {
        rbd.velocity = new Vector2(maxSpeed * Input.GetAxis("Horizontal"), rbd.velocity.y);
    }

    public void LaunchPlayer((Vector2, bool) args)
    {
        rbd.velocity = (args.Item1 * launchSpeed);
        if (args.Item2) timerDuration = knockbackStunDuration;
    }
}
