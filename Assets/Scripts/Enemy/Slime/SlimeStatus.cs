using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeStatus : HealthBase
{


    [SerializeField] private float boundForce = 1000;

    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private SlimeMovement slimeMovement;

    [SerializeField] private int direction;
    float currentFollowDistance;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        slimeMovement = GetComponent<SlimeMovement>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        currentFollowDistance = slimeMovement.activeFollowDistance;
    }

    private void Update()
    {
        if (playerMovement.facingDown)
        {
            direction = 1;
        }
        else if (playerMovement.facingUp)
        {
            direction = 2;
        }
        else if (playerMovement.facingLeft)
        {
            direction = 3;
        }
        else if (playerMovement.facingRight)
        {
            direction = 4;
        }
    }

    public override void TakeDame(float damage, string type, bool status)
    {
        base.TakeDame(damage, type, status);
        StartCoroutine(KnockBackTime());
        KnockBackController();

        // Play hurt animation

        animator.SetTrigger("Hitting");
        animator.SetBool("IsDead", false);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected override void Die()
    {
        // Die animation
        animator.SetBool("IsDead", true);

        // Disable the enemy

        GetComponent<Collider2D>().enabled = false;
        GetComponent<SlimeMovement>().enabled = false;
        theRB2D.isKinematic = true;
        this.enabled = false;
        theRB2D.constraints = RigidbodyConstraints2D.FreezeAll;

        DropItem();
        Destroy(transform.gameObject, 1.5f);

    }

    public void KnockBackController()
    {
        if (direction == 3)
        {
            theRB2D.AddForce(new Vector2(-boundForce, 100));
        }
        else if (direction == 4)
        {
            theRB2D.AddForce(new Vector2(boundForce, 100));
        }
        else if (direction == 1)
        {
            theRB2D.AddForce(new Vector2(100, -boundForce));
        }
        else if (direction == 2)
        {
            theRB2D.AddForce(new Vector2(100, boundForce));

        }

    }

    IEnumerator KnockBackTime()
    {
        slimeMovement.activeFollowDistance = 4f;
        yield return new WaitForSeconds(0.2f);
        slimeMovement.activeFollowDistance = currentFollowDistance;
    }

    protected override void DropItem()
    {
        GameObject itemDrop = (GameObject)Instantiate(itemDropRef);

        // Map the newly loaded destructable object to the x and y position of the previously destroyed box
        itemDrop.transform.position = transform.position;
    }
}
