using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 雑魚敵1の移動アルゴリズム
/// </summary>
public class Zako1 : MonoBehaviour,IEnemy
{
    private Vector3 targetPos = new Vector3(0,0,0);
    [SerializeField] private float enemySpeed;
    [SerializeField] private Rigidbody2D rb;
    private SoundManager soundManager;
    public void Init(SoundManager soundManager)
    {
        this.soundManager = soundManager;
    }
    private void Start()
    {
        StartCoroutine(Action());
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
        float timer = 0;
        while ((transform.position - targetPos).magnitude > 6)
        {
            yield return new WaitForFixedUpdate();
            Vector2 moveDirection = targetPos - transform.position;
            moveDirection.Normalize();
            rb.velocity = moveDirection * enemySpeed;
        }
        yield return new WaitForSeconds(0.5f);
        //移動方向をランダムにする
        int randomValue = Random.Range(0, 2);
        int direction = (randomValue == 0) ? -1 : 1;
        while (true)
        {
            transform.RotateAround(targetPos,Vector3.forward, 360 / 10 * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
    }

    private void Death()
    {
        soundManager.EnemyDestroySE();
        Destroy(this.gameObject);
    }
}
