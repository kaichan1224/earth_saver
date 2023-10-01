using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Fish : MonoBehaviour
{
    //transformのキャッシュ
    public Transform thisTransform;
    [SerializeField] private FishData fishData;
    [SerializeField] private FishManager fishManager;
    public Vector3 accel;
    public Vector3 Velocity;
    [SerializeField] private float cohesionTurblence;
    [SerializeField] private float alignmentTurblence;
    [SerializeField] private float separationTurblence;
    private SoundManager soundManager;

    public void Init(FishManager fishManager,SoundManager soundManager)
    {
        this.fishManager = fishManager;
        this.soundManager = soundManager;
    }

    private void Start()
    {
        thisTransform = transform;
    }

    private void Update()
    {
        Move();
    }
    void Move()
    {
        float deltaTime = Time.deltaTime;
        float speed;
        Vector3 direction;
        accel = (HeadToLeader() * fishData.LeaderWeight + GetCohesion() * fishData.CohesionWeight + GetAlignment() * fishData.AlignmentWeight + GetSeparation() * fishData.SeparationWeight) / (fishData.LeaderWeight + fishData.CohesionWeight + fishData.AlignmentWeight + fishData.SeparationWeight);
        Velocity += accel * Mathf.PerlinNoise(Time.time, Random.value);
        direction = Velocity.normalized;
        speed = Velocity.magnitude;
        Velocity = direction * Mathf.Clamp(speed, fishData.MinSpeed, fishData.MaxSpeed) * Random.Range(0.9f,1.1f);
        thisTransform.position += Velocity * deltaTime;
        if (speed > 0)
        {
            //thisTransform.rotation = Quaternion.LookRotation(Velocity, Vector3.up);
            thisTransform.rotation = fishManager.fishLeader.transform.rotation;
            //thisTransform.rotation = Quaternion.Euler(thisTransform.rotation.x, thisTransform.rotation.y,0);
        }
    }

    Vector3 HeadToLeader()
    {
        Vector3 headToLeader = Vector3.zero;
        if ((fishManager.fishLeader.transform.position - thisTransform.position).sqrMagnitude > fishData.MaxDistanceFromLeader * fishData.MaxDistanceFromLeader)
        {
            headToLeader = fishManager.fishLeader.transform.position - thisTransform.position;
        }

        return headToLeader;
    }

    Vector3 GetCohesion()
    {
        Vector3 origin;
        Vector3 cohesion;
        if (fishManager.fishLeader != null)
        {
            origin = fishManager.fishLeader.transform.position;
        }
        else
        {
            origin = fishManager.transform.position;
        }
        cohesion = origin - thisTransform.position;
        cohesion = Vector3.Lerp(cohesion, Velocity, cohesionTurblence);

        return cohesion;
    }

    Vector3 GetAlignment()
    {
        Vector3 alignment = fishManager.averageAlignment;
        alignment = Vector3.Lerp(alignment, Velocity, alignmentTurblence);

        return alignment;
    }

    Vector3 GetSeparation()
    {
        Vector3 separation = fishManager.averageSeparation;

        separation = thisTransform.position - separation;
        separation = Vector3.Lerp(separation, thisTransform.position, separationTurblence);

        return separation;
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

    private void Death()
    {
        fishManager.RemoveFish(this);
        soundManager.EnemyDestroySE();
        Destroy(this.gameObject);
    }
}
