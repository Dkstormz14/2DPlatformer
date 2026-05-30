using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10;
    public Vector2 moveSpeed = new Vector2(5f, 0);
    public Vector2 knockback = new Vector2(0, 0);

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // if u want the projectile to be affected by gravity by default then make it a dynamic mode rigid body
        rb.linearVelocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            // Calculate knockback based on the direction of the projectile
            Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x * -1, knockback.y);

            // Hit the target
            bool gotHit = damageable.Hit(damage, knockback);

            if (gotHit)
            Debug.Log(collision.name + " hit for " + damage);
            Destroy(gameObject);
        }
    }
}
