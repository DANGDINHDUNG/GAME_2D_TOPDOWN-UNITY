using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Skills : SkillCooldown
{

    private Collider2D myCollider;
    private Rigidbody2D myRigidbody;

    protected override void Awake()
    {
        myCollider = GetComponent<Collider2D>();
        myCollider.isTrigger = true;

        myRigidbody = GetComponent<Rigidbody2D>();
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        
    }

    private void Update()
    {
        if (skillToCast.skillType == SkillType.Range)
        {
            if (skillToCast.Speed >= 0)
            {
                myRigidbody.velocity = new Vector2(direction.x, direction.y).normalized * skillToCast.Speed;
                float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, rot + 180);
            }
        }
        else if (skillToCast.skillType == SkillType.Melee)
        {
            Transform target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            transform.position = Vector2.MoveTowards(transform.position, target.position, 100 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Apply skill effects to whatever we hit
        // Apply hit particle effects
        // Apply sound effects

        if (collision.gameObject.CompareTag("Enemy"))
        {
            bool isCritical = IsCriticalHit((float)PlayerStatusController.GetInstance().playerCurrentCritRate);
            var totalAttack = 0f;

            if (isCritical)
            {
                totalAttack = (PlayerStatusController.GetInstance().playerCurrentSpAttack * (1 + 20 / 100 + skillToCast.DamageAmount / 5)) * (1 + PlayerStatusController.GetInstance().playerCurrentCritDamage);
            }
            else
            {
                totalAttack = PlayerStatusController.GetInstance().playerCurrentSpAttack * (1 + 20 / 100 + skillToCast.DamageAmount / 5);
            }

            collision.GetComponent<HealthBase>().TakeDame(totalAttack, "magic", isCritical);
            if (!skillToCast.DamageOfTime)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public bool IsCriticalHit(float criticalRate)
    {
        // Generate a random number between 0 and 1
        float randomValue = Random.Range(0f, 1.0f);

        // Check if the random number is less than the critical rate to determine a critical hit
        return randomValue < criticalRate;
    }
}
