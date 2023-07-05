using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb2d;
    private float lastAttackedAt = -9999f;

    public PlayerMovement playerMovement;
    public PlayerStatus playerStatus;

    public Transform attackLeft;
    public Transform attackRight;
    public Transform attackUp;
    public Transform attackDown;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;

    public float attackState = -1f;

    [SerializeField]
    public bool facingLeft, facingRight, facingUp = false;
    [SerializeField]
    public bool facingDown = true;

    Collider2D[] hitEnemies;

    public float frozenTime;            // Khoảng thời gian nhân vật bị đứng yên khi thực hiện tấn công
    //public ScreenShakeController shakeController;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        playerStatus = GetComponent<PlayerStatus>();
        attackLeft = GameObject.FindGameObjectWithTag("AttackLeft").GetComponent<Transform>();
        attackRight = GameObject.FindGameObjectWithTag("AttackRight").GetComponent<Transform>();
        attackUp = GameObject.FindGameObjectWithTag("AttackUp").GetComponent<Transform>();
        attackDown = GameObject.FindGameObjectWithTag("AttackDown").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }

        if (Mouse.current.leftButton.wasPressedThisFrame && !IsPointerOverUIObject())
        {
            if (PlayerStatusController.GetInstance().currentEnergy <= 1)
            {
                return;
            }
            else
            {
                Attack();
                StartCoroutine(AttackTime());
            }
        }

    }

    public void Attack()
    {

        if (Time.time > lastAttackedAt + PlayerStatusController.GetInstance().playerCurrentAttackSpeed)
        {
            playerStatus.EnergyDecrease();
            // Play an attack animation
            attackState++;
            animator.SetFloat("SlashHorizontal", playerMovement.lastXPos);
            animator.SetFloat("SlashVertical", playerMovement.lastYPos);
            animator.SetFloat("SlashState", attackState);
            animator.SetTrigger("IsSlashing");

            if (attackState >= 1)
            {
                attackState = -1;
            }
            lastAttackedAt = Time.time;

            // Detect enemies in range of attack
            if (playerMovement.facingUp)
            {
                hitEnemies = Physics2D.OverlapCircleAll(attackUp.position, attackRange, enemyLayers);
            }
            else if (playerMovement.facingDown)
            {
                hitEnemies = Physics2D.OverlapCircleAll(attackDown.position, attackRange, enemyLayers);
            }
            else if (playerMovement.facingLeft)
            {
                hitEnemies = Physics2D.OverlapCircleAll(attackLeft.position, attackRange, enemyLayers);             
            }
            else if (playerMovement.facingRight)
            {
                hitEnemies = Physics2D.OverlapCircleAll(attackRight.position, attackRange, enemyLayers);
            }

            // Damage them
            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.gameObject.tag == "Skeleton")
                {
                    enemy.GetComponent<EnemyStatus>().TakeDamage(PlayerStatusController.GetInstance().playerCurrentAttack);
                }
                else if (enemy.gameObject.tag == "Slime")
                {
                    enemy.GetComponent<SlimeStatus>().TakeDamage(PlayerStatusController.GetInstance().playerCurrentAttack);
                }
                else if (enemy.gameObject.tag == "ToasterBot")
                {
                    enemy.GetComponent<RangeEnemyStatus>().TakeDamage(PlayerStatusController.GetInstance().playerCurrentAttack);
                }

            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackLeft == null) return;
        if (attackRight == null) return;
        if (attackUp == null) return;
        if (attackDown == null) return;
        if (playerMovement.facingUp)
        {
            Gizmos.DrawWireSphere(attackUp.position, attackRange);
        }
        else if (playerMovement.facingDown)
        {
            Gizmos.DrawWireSphere(attackDown.position, attackRange);
        }
        else if (playerMovement.facingLeft)
        {
            Gizmos.DrawWireSphere(attackLeft.position, attackRange);
        }
        else if (playerMovement.facingRight)
        {
            Gizmos.DrawWireSphere(attackRight.position, attackRange);
        }
    }

    IEnumerator AttackTime()
    {
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(frozenTime);
        rb2d.constraints = RigidbodyConstraints2D.None;
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = Mouse.current.position.ReadValue();
        List<RaycastResult> result = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, result);
        return result.Count > 0;
    }

}
