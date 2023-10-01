using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishLeader : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject baria;
    private FishManager fishManager;
    [SerializeField] private GameObject enemyBullet;
    private SoundManager soundManager;

    public void Init(FishManager fishManager, SoundManager soundManager)
    {
        this.fishManager = fishManager;
        this.soundManager = soundManager;
    }
    void Update()
    {
        transform.RotateAround(Vector3.zero,
      new Vector3(0, 0, -1),
      360 / (1/moveSpeed) * Time.deltaTime
        );
    }

    public void BariaOff()
    {
        baria.SetActive(false);
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

    IEnumerator Shot()
    {
        while (true)
        {
            yield return new WaitForSeconds(7f);
            Instantiate(enemyBullet, transform.position, Quaternion.identity);
        }
    }


    private void Death()
    {
        soundManager.EnemyDestroySE();
        Destroy(fishManager.gameObject);
        Destroy(this.gameObject);
    }
}
