using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public abstract class HealthBase : MonoBehaviour
{
    [SerializeField] private GameObject floatingTextPrefab;
    [SerializeField] private EnemySO enemy;
    [SerializeField] private FloatingHealthBar healthBar;
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float currentHealth;
    [SerializeField] protected float currentDefend;
    [SerializeField] protected Rigidbody2D theRB2D;
    [SerializeField] protected GameObject itemDropRef;
    [SerializeField] protected Animator animator;
    [SerializeField] protected float knockbackVel = 1000f;
    [SerializeField] private float restTime = 0f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        maxHealth = enemy.Health * (1 + 15 * (PlayerStatusController.GetInstance().playerLevel / 100));
        currentDefend = PlayerStatusController.GetInstance().playerLevel * enemy.Defend + 500;
        animator = GetComponentInChildren<Animator>();
        currentHealth = maxHealth;
        theRB2D = GetComponent<Rigidbody2D>();
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        restTime += Time.deltaTime;
        if (restTime >= 15)
        {
            restTime = 15f;
            healthBar.SetMaxHealth(maxHealth);
            currentHealth = maxHealth;
        }
    }

    public virtual void TakeDame(float damage, string type, bool status)
    {
        SceneShakeController.Instance.ShakeCamera();
        float damageTaken = 0;
        restTime = 0;
        if (type == "physic")
        {
            damageTaken = damage * (1 - enemy.PhysicRes / 100) * (1 - ((currentDefend / 3) / (6 * (damage + 100) + (currentDefend / 3 + 100))));
            currentHealth -= (int)Math.Ceiling(damageTaken);
        }
        else if (type == "magic")
        {
            damageTaken = damage * (1 - enemy.MagicRes / 100) * (1 - ((currentDefend / 3) / (6 * (damage + 100) + (currentDefend / 3 + 100))));
            currentHealth -= (int)Math.Ceiling(damageTaken);
        }
        StartCoroutine(Immobile());
        ShowFloatingDamage((int)Math.Ceiling(damageTaken), type, status);
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void ShowFloatingDamage( float damageTaken, string type, bool status)
    {
        var floatingDamage = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
        floatingDamage.GetComponent<TextMeshPro>().text = damageTaken.ToString();
        if (type == "physic")
        {
            floatingDamage.GetComponent<TextMeshPro>().color = Color.yellow;
            if (status)
            {
                floatingDamage.GetComponent<TextMeshPro>().color = Color.red;
                floatingDamage.GetComponent<TextMeshPro>().fontSize = 10;
            }
        }
        else if (type == "magic")
        {
            floatingDamage.GetComponent<TextMeshPro>().color = Color.cyan;
            if (status)
            {
                floatingDamage.GetComponent<TextMeshPro>().color = Color.blue;
                floatingDamage.GetComponent<TextMeshPro>().fontSize = 10;
            }
        }
    }

    protected virtual void Die()
    {
        GetComponent<EnemyAI>().enabled = false;


        // Disable the enemy

        GetComponent<Collider2D>().enabled = false;
        theRB2D.isKinematic = true;
        this.enabled = false;
        theRB2D.constraints = RigidbodyConstraints2D.FreezeAll;

        //DropItem();
        DropItem();
        Destroy(transform.gameObject, 1f);
    }

    protected virtual void DropItem()
    {
        GameObject itemDrop = (GameObject)Instantiate(itemDropRef);

        // Map the newly loaded destructable object to the x and y position of the previously destroyed box
        itemDrop.transform.position = transform.position;
    }

    private IEnumerator Immobile()
    {
        this.gameObject.GetComponent<EnemyAI>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        this.gameObject.GetComponent<EnemyAI>().enabled = true;
    }
}
