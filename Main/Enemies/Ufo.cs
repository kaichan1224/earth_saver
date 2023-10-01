using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ufo : MonoBehaviour,IEnemy
{
    private Vector3 targetPos = new Vector3(0, 0, 0);
    [SerializeField] private float enemySpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject enemyBullet;
    private SoundManager soundManager;
    public void Init(SoundManager soundManager)
    {
        this.soundManager = soundManager;
    }
    private void Start()
    {
        StartCoroutine(Action());
        StartCoroutine(Shot());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Earth")
        {
            collision.gameObject.GetComponent<EarthManager>().GetDamage();
            Death();
            return;
        }

        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().GetDamage();
            Death();
            return;
        }

        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            Death();
        }
    }


    IEnumerator Action()
    {
        while ((transform.position - targetPos).magnitude > 6)
        {
            yield return new WaitForFixedUpdate();
            Vector2 moveDirection = targetPos - transform.position;
            moveDirection.Normalize();
            rb.velocity = moveDirection * enemySpeed;
        }
        float delta = 0;
        int p = 1;
        rb.velocity = Vector3.zero;
        while (true)
        {
            transform.RotateAround(targetPos, Vector3.forward, 360 / 10 * Time.deltaTime);
            transform.position *= (1f+delta);
            yield return new WaitForFixedUpdate();
            delta += p *  0.001f;
            if (delta >= 0.015f)
            {
                p = -1;
            }
            if (delta <= -0.015f)
            {
                p = 1;
            }
        }
    }

    IEnumerator Shot()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            Instantiate(enemyBullet,transform.position,Quaternion.identity);
        }
    }

    private void Death()
    {
        soundManager.EnemyDestroySE();
        Destroy(this.gameObject);
    }
}
