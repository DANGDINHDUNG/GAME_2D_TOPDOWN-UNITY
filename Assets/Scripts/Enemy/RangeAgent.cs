using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAgent : MonoBehaviour
{
    private AgentMover agentMover;

    //private WeaponParent weaponParent;

    [SerializeField] private Animator animator;

    [SerializeField]
    private Transform attackPoint, hitPoint;

    [SerializeField]
    private float attackDamage;

    [SerializeField]
    private LayerMask playerLayer;

    public float lastXPos, lastYPos = 0;

    public float chargeTime = 0.5f;
    public float currentChargeTime = 0.5f;

    private Vector2 pointerInput, movementInput;

    public Vector2 PointerInput { get => pointerInput; set => pointerInput = value; }
    public Vector2 MovementInput { get => movementInput; set => movementInput = value; }

    private void Update()
    {
        agentMover.MovementInput = movementInput;
        //weaponParent.PointerPosition = pointerInput;
        AnimateEnemy();
    }

    public void PerformAttack()
    {
        animator.SetTrigger("IsCharge");
        if (currentChargeTime >= chargeTime)
        {
            animator.SetTrigger("IsAttack");
            //animator.SetFloat("Horizontal", lastXPos);
            //animator.SetFloat("Vertical", lastYPos);
            Attack();
        }
        else
        {
            currentChargeTime += Time.fixedDeltaTime;
        }

    }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        //weaponParent = GetComponentInChildren<WeaponParent>();
        agentMover = GetComponent<AgentMover>();
    }

    private void AnimateEnemy()
    {
        Vector2 lookDirection = pointerInput - (Vector2)transform.position;
        Vector3 scale = transform.localScale;
        if (lookDirection.x > 0)
        {
            scale.x = 1;
        }
        else if (lookDirection.x < 0)
        {
            scale.x = -1;
        }
        transform.localScale = scale;
        if (movementInput.x == 0 && movementInput.y == 0)
        {
            animator.SetInteger("State", 0);
            //animator.SetFloat("Horizontal", lastXPos);
            //animator.SetFloat("Vertical", lastYPos);
        }
        else
        {
            //lastXPos = movementInput.x;
            //lastYPos = movementInput.y;
            animator.SetInteger("State", 1);
            //animator.SetFloat("Horizontal", movementInput.x);
            //animator.SetFloat("Vertical", movementInput.y);
        }
    }

    private void Attack()
    {
        Collider2D[] hitPlayer;
        hitPlayer = Physics2D.OverlapAreaAll(attackPoint.position, hitPoint.position, playerLayer);

        foreach (Collider2D player in hitPlayer)
        {
            if (player.gameObject.tag == "Player")
            {
                player.GetComponent<PlayerStatus>().TakeDamage(attackDamage);
            }
        }

        currentChargeTime = 0;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(attackPoint.position, hitPoint.position);
    }
}
