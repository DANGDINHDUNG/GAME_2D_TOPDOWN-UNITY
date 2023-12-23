using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherEnemyStatus : HealthBase
{
    [SerializeField] private ArcherAgent agent;
    [SerializeField] private SpriteRenderer sprite;


    protected override void Start()
    {
        base.Start();
        sprite = GetComponentInChildren<SpriteRenderer>();

        agent = GetComponent<ArcherAgent>();

    }

    public override void TakeDame(float damage, string type, bool status)
    {
        base.TakeDame(damage, type, status);
        //theRB2D.AddForce(new Vector2(-agent.lastXPos, -agent.lastYPos).normalized * knockbackVel);
        //theRB2D.velocity = -direction.normalized * 1000;

        // Play hurt animation

        //animator.SetTrigger("IsHit");
        //animator.SetBool("IsDead", false);

    }

    protected override void Die()
    {
        base.Die();
        GetComponent<ArcherAgent>().enabled = false;

        // Die animation
        //animator.SetBool("IsDead", true);


    }

    protected override void DropItem()
    {
        base.DropItem();
    }

    private IEnumerator FadeToWhite()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.white;

    }
}
