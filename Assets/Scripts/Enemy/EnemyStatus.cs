using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : HealthBase
{
    [SerializeField] private SpriteRenderer sprite;

    [SerializeField] private Agent agent;

    protected override void Start()
    {
        base.Start();
        sprite = GetComponentInChildren<SpriteRenderer>();

        agent = GetComponent<Agent>();

    }

    public override void TakeDame(float damage, string type, bool status)
    {
        base.TakeDame(damage, type, status);
        StartCoroutine(FadeToWhite());
        theRB2D.AddForce(new Vector2(-agent.lastXPos, -agent.lastYPos).normalized * knockbackVel);

        // Play hurt animation

        //animator.SetTrigger("Hitting");
        //animator.SetBool("IsDead", false);

    }

    protected override void Die()
    {
        base.Die();
        GetComponent<Agent>().enabled = false;

        // Die animation
        animator.SetTrigger("IsDead");
        animator.SetFloat("Horizontal", agent.MovementInput.x);
        animator.SetFloat("Vertical", agent.MovementInput.y);
       
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
