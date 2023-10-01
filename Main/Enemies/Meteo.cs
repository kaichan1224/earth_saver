using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VContainer;

public class Meteo : MonoBehaviour,IEnemy
{
    private Vector3 targetPos;
    [SerializeField] private float enemySpeed;
    [SerializeField] private Rigidbody2D rb;
    private SoundManager soundManager;

    public void Init(SoundManager soundManager)
    {
        this.soundManager = soundManager;
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
            soundManager.EnemyDestroySE();
            Destroy(this.gameObject);
            return;
        }

        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().GetDamage();
            soundManager.EnemyDestroySE();
            Destroy(this.gameObject);
            return;
        }
        if (collision.gameObject.tag == "Bullet")
        {
            soundManager.EnemyDestroySE();
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
            return;
        }
    }
}
