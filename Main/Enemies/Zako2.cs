using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 雑魚敵2の移動アルゴリズム
/// </summary>
public class Zako2 : MonoBehaviour,IEnemy
{
    private Vector3 targetPos = new Vector3(0,0,0);
    [SerializeField] private float enemySpeed;
    [SerializeField] private Rigidbody2D rb;
    private SoundManager soundManager;
    private int life = 3;
    public void Init(SoundManager soundManager)
    {
        this.soundManager = soundManager;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Earth")
        {
            soundManager.EnemyDestroySE();
            collision.gameObject.GetComponent<EarthManager>().GetDamage();
            Death();
            return;
        }

        if (collision.gameObject.tag == "Player")
        {
            soundManager.EnemyDestroySE();
            collision.gameObject.GetComponent<Player>().GetDamage();
            Death();
            return;
        }

        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            soundManager.EnemyDestroySE();
            Damage();
            return;
        }
    }

    void FixedUpdate()
    {
        Vector2 moveDirection = targetPos - transform.position;
        moveDirection.Normalize();
        rb.velocity = moveDirection * enemySpeed;
    }

    private void Damage()
    {
        life--;
        if (life <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }


}
