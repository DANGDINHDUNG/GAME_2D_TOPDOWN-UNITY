using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Skills : MonoBehaviour
{
    private Vector3 mousePos, direction, rotation;
    private Camera mainCam;
    public SkillSO skillToCast;

    private Collider2D myCollider;
    private Rigidbody2D myRigidbody;

    Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        myCollider = GetComponent<Collider2D>();
        myCollider.isTrigger = true;

        myRigidbody = GetComponent<Rigidbody2D>();
        //myRigidbody.isKinematic = true;

        Destroy(this.gameObject, skillToCast.LifeTime);
     }

    private void Start()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - transform.position;
        rotation = transform.position - mousePos;
        
    }

    private void Update()
    {
        if (skillToCast.Speed >= 0)
        {
            myRigidbody.velocity = new Vector2(direction.x, direction.y).normalized * skillToCast.Speed;
            float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rot + 180);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Apply skill effects to whatever we hit
        // Apply hit particle effects
        // Apply sound effects

        if (collision.gameObject.CompareTag("Skeleton"))
        {
            collision.GetComponent<EnemyStatus>().TakeDamage(skillToCast.DamageAmount + PlayerStatusController.GetInstance().playerMaxSpAttack);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("Slime"))
        {
            collision.GetComponent<SlimeStatus>().TakeDamage(skillToCast.DamageAmount + PlayerStatusController.GetInstance().playerMaxSpAttack);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("ToasterBot"))
        {
            collision.GetComponent<RangeEnemyStatus>().TakeDamage(skillToCast.DamageAmount + PlayerStatusController.GetInstance().playerMaxSpAttack);
            Destroy(this.gameObject);
        }

    }
}
