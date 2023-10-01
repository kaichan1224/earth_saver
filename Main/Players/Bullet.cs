using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private MasterParam masterParam;
    [SerializeField] private GameObject player;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float deathTime;

    public void Setup(GameObject player)
    {
        this.player = player;
        transform.rotation = this.player.transform.rotation;
    }

    void Start()
    {
        rb.AddForce(player.transform.up * masterParam.bulletSpeed.Value,ForceMode2D.Impulse);
        Destroy(this.gameObject, deathTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Baria")
        {
            Destroy(this.gameObject);
            return;
        }
    }
}
