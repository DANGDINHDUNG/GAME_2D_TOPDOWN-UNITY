using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private float currentAttackDamage;
    [SerializeField] private float existTime = 0.5f;
    [SerializeField] private float speed = 30f;
    [SerializeField] private EnemySO enemy;
    [SerializeField] private bool isFlying;
    [SerializeField] private Rigidbody2D rgd;
    private Transform player;
    private Vector2 direction;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Start()
    {
        rgd = GetComponent<Rigidbody2D>();
        currentAttackDamage = enemy.Attack * (1 + (PlayerStatusController.GetInstance().playerLevel * 20 / 100));
        Destroy(this.gameObject, existTime);
        direction = (player.position - transform.position).normalized;
    }

    private void Update()
    {
        if (isFlying)
        {
            rgd.velocity = new Vector2(direction.x, direction.y).normalized * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Apply skill effects to whatever we hit
        // Apply hit particle effects
        // Apply sound effects

        if (collision.gameObject.CompareTag("Player"))
        {
            Transform target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            target.GetComponent<PlayerStatus>().TakeDamage(currentAttackDamage);
            Destroy(this.gameObject);
        }
    }

}
