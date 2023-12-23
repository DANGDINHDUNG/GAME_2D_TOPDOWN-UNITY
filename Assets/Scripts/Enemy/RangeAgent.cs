using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAgent : MonoBehaviour
{
    private AgentMover agentMover;
    public GameObject bulletPref;
    //private WeaponParent weaponParent;

    [SerializeField] private Animator animator;

    [SerializeField]
    private Transform attackPoint;
    public float lastXPos, lastYPos = 0;

    [SerializeField] private float chargeTime = 0.5f;
    [SerializeField] private float currentChargeTime = 0.5f;
    [SerializeField] private float timeLockTarget = 1.5f;

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
        animator.SetTrigger("IsCharge");
        if (currentChargeTime < chargeTime / timeLockTarget)
        {
            Transform target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            Vector3 rotation = transform.position - target.position;
            rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        }
        if (currentChargeTime >= chargeTime)
        {
            animator.SetTrigger("IsAttack");
            Instantiate(bulletPref, attackPoint.position, Quaternion.Euler(0, 0, rot + 180));
            //animator.SetFloat("Horizontal", lastXPos);
            //animator.SetFloat("Vertical", lastYPos);
            currentChargeTime = 0;
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
        }
        else
        {
            lastXPos = movementInput.x;
            lastYPos = movementInput.y;
            animator.SetInteger("State", 1);
        }
    }
}
