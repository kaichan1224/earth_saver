using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Vector3 targetPos = new Vector3(0,0,0);
    [SerializeField] private float enemySpeed;
    [SerializeField] private Rigidbody2D rb;
    private void Start()
    {
        transform.LookAt(targetPos);
        transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
    }
    void FixedUpdate()
    {
        Vector2 moveDirection = targetPos - transform.position;
        moveDirection.Normalize();
        rb.velocity = moveDirection * enemySpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Earth")
        {
            collision.gameObject.GetComponent<EarthManager>().GetDamage();
            Destroy(this.gameObject);
            return;
        }

        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().GetDamage();
            Destroy(this.gameObject);
            return;
        }

        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
            return;
        }
    }
}
