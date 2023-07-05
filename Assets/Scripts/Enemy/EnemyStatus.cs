using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float currentHealth;
    [SerializeField] private Rigidbody2D theRB2D;
    [SerializeField] private GameObject itemDropRef;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sprite;

    [SerializeField] private Agent agent;

    [SerializeField] private float knockbackVel = 1000f;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        agent = GetComponent<Agent>();
        currentHealth = maxHealth;
        theRB2D = GetComponent<Rigidbody2D>();

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        SceneShakeController.Instance.ShakeCamera(3f, .1f);
        StartCoroutine(FadeToWhite());
        theRB2D.AddForce(-agent.MovementInput.normalized * knockbackVel);
        //theRB2D.velocity = -direction.normalized * 1000;

        // Play hurt animation

        //animator.SetTrigger("Hitting");
        //animator.SetBool("IsDead", false);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GetComponent<EnemyAI>().enabled = false;
        GetComponent<Agent>().enabled = false;

        // Die animation
        animator.SetTrigger("IsDead");
        animator.SetFloat("Horizontal", agent.MovementInput.x);
        animator.SetFloat("Vertical", agent.MovementInput.y);
        // Disable the enemy

        GetComponent<Collider2D>().enabled = false;
        theRB2D.isKinematic = true;
        this.enabled = false;
        theRB2D.constraints = RigidbodyConstraints2D.FreezeAll;

        //DropItem();
        DropItem();
        Destroy(transform.gameObject, 1f);
    }

    void DropItem()
    {
        GameObject itemDrop = (GameObject)Instantiate(itemDropRef);

        // Map the newly loaded destructable object to the x and y position of the previously destroyed box
        itemDrop.transform.position = transform.position;
    }

    private IEnumerator FadeToWhite()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.white;

    }
}
