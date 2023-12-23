using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAgent : MonoBehaviour
{
    private AgentMover agentMover;
    public GameObject bulletPref;

    //private WeaponParent weaponParent;

    [SerializeField] private Animator animator;

    [SerializeField]
    private Transform attackPoint;

    [SerializeField]
    private float attackDamage;

    public float lastXPos, lastYPos = 0;

    private Vector2 pointerInput, movementInput;

    public Vector2 PointerInput { get => pointerInput; set => pointerInput = value; }
    public Vector2 MovementInput { get => movementInput; set => movementInput = value; }
    float rot = 0f;

    private void Update()
    {

        agentMover.MovementInput = movementInput;
        //weaponParent.PointerPosition = pointerInput;
        AnimateEnemy();
    }

    public void PerformAttack()
    {
        Transform target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Vector3 rotation = transform.position - target.position;
        rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        animator.SetTrigger("IsAttack");
        Instantiate(bulletPref, attackPoint.position, Quaternion.Euler(0, 0, rot + 180));
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
}
