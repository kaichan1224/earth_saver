using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour,IEnemy
{
    [SerializeField] private int life;
    [SerializeField] private GameObject ufoPrefabs;
    private Vector3 targetPos = new Vector3(0, 0, 0);
    [SerializeField] private float enemySpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SoundManager soundManager;
    public void Init(SoundManager soundManager)
    {
        this.soundManager = soundManager;  
    }
    void Start()
    {
        StartCoroutine(Generate());
        StartCoroutine(Action());
    }

    IEnumerator Generate()
    {
        while (true)
        {
            var v = Instantiate(ufoPrefabs,transform);
            v.GetComponent<Ufo>().Init(soundManager);
            yield return new WaitForSeconds(2f);
        }
    }

    IEnumerator Action()
    {
        while ((transform.position - targetPos).magnitude > 7f)
        {
            yield return new WaitForFixedUpdate();
            Vector2 moveDirection = targetPos - transform.position;
            moveDirection.Normalize();
            rb.velocity = moveDirection * enemySpeed;
        }
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
