using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Agent : MonoBehaviour
{
    private AgentMover agentMover;

    //private WeaponParent weaponParent;

    [SerializeField] private Animator animator;

    [SerializeField]
    private Transform attackPoint;

    [SerializeField]
    private float attackDamage;

    [SerializeField]
    private LayerMask playerLayer;

    public float lastXPos, lastYPos = 0;

    public float attackDistance = 2f;

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
        animator.SetTrigger("IsAttack");
        animator.SetFloat("Horizontal", lastXPos);
        animator.SetFloat("Vertical", lastYPos);
        Attack();
    }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        //weaponParent = GetComponentInChildren<WeaponParent>();
        agentMover = GetComponent<AgentMover>();
    }

    private void AnimateEnemy()
    {
        if (movementInput.x == 0 && movementInput.y == 0)
        {
            animator.SetInteger("State", 0);
            animator.SetFloat("Horizontal", lastXPos);
            animator.SetFloat("Vertical", lastYPos);
        }
        else
        {
            lastXPos = movementInput.x;
            lastYPos = movementInput.y;
            animator.SetInteger("State", 1);
            animator.SetFloat("Horizontal", movementInput.x);
            animator.SetFloat("Vertical", movementInput.y);
        }
    }

    private void Attack()
    {
        Collider2D[] hitPlayer;
        hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackDistance, playerLayer);

        foreach (Collider2D player in hitPlayer)
        {
            if (player.gameObject.tag == "Player")
            {
                player.GetComponent<PlayerStatus>().TakeDamage(attackDamage);
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackDistance);
    }

}