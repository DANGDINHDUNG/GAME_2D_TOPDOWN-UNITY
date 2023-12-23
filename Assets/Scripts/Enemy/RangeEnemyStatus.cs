using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemyStatus : HealthBase
{

    [SerializeField] private RangeAgent agent;


    protected override void Start()
    {
        base.Start();

        agent = GetComponent<RangeAgent>();

    }

    public override void TakeDame(float damage, string type, bool status)
    {
        base.TakeDame(damage, type, status);
        theRB2D.AddForce(new Vector2(-agent.lastXPos, -agent.lastYPos).normalized * knockbackVel);
        //theRB2D.velocity = -direction.normalized * 1000;

        // Play hurt animation

        animator.SetTrigger("IsHit");
        animator.SetBool("IsDead", false);

    }

    protected override void Die()
    {
        base.Die();
        GetComponent<RangeAgent>().enabled = false;

        // Die animation
        animator.SetBool("IsDead", true);

        
    }

    protected override void DropItem()
    {
        base.DropItem();
    }

}
