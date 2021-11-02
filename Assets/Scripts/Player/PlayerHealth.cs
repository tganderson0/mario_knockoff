using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float damageDealt;

    public Vector2 damageArea;
    public float immunityCooldown = 2.0f;
    private float immunityTimer = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (immunityTimer > 0)
        {
            immunityTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.BoxCast(new Vector2(transform.position.x, transform.position.y - 0.75f), damageArea, 0.0f, Vector2.down);
        Debug.DrawRay(new Vector2(transform.position.x - damageArea.x/2, transform.position.y - 0.75f), damageArea.y/2 * Vector2.down, Color.red);
        Debug.DrawRay(new Vector2(transform.position.x + damageArea.x/2, transform.position.y - 0.75f), damageArea.y/2 * Vector2.down, Color.red);

        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            hit.collider.gameObject.SendMessage("TakeDamage", damageDealt);
            Debug.Log("Enemy Damaged");
            Vector2 launchDir = (transform.position - hit.collider.transform.position).normalized;
            gameObject.SendMessage("LaunchPlayer", (launchDir, false));
        }
    }

    public void TakeDamage((float, Vector2) args)
    {
        if (immunityTimer <= 0)
        {
            gameObject.SendMessage("LaunchPlayer", (args.Item2, true));
            health -= args.Item1;
            immunityTimer = immunityCooldown;
        }
        if (health <= 0)
        {
            Debug.Log("Player has died");
        }
    }
}
