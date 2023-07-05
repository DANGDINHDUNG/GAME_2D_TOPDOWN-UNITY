using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    public Animator animator;
    public Transform player;

    public SlimeCombat slimeCombat;

    public float moveSpeed = 50f;                           // Tốc độ di chuyển của quái
    public Rigidbody2D rb2d;

    [SerializeField]
    bool facingLeft, facingRight, facingUp = false;         // Hướng của quái 
    [SerializeField]
    private bool facingDown = true;

    [SerializeField]
    public float lastXPos, lastYPos = 0f;                   // Tọa độ hướng của quái 

    public float idleTime = 20f;                            // Thời gian nghỉ ngơi mặc định
    public float currentIdleTime;                           // Thời gian nghỉ ngơi hiện tại so với thời gian nghỉ ngơi mặc định

    public float currentMovementTime = 0f;                  // Thời gian di chuyển hiện tại so với thời gian di chuyển mặc định
    public float movementTime = 4f;                         // Thời gian di chuyển mặc định

    public bool isIdle = true;                              // Trạng thái nghỉ ngơi của quái
    public bool isFollowing = false;                        // Trạng thái quái đang đi theo người chơi

    public float activeFollowDistance;	// Khoảng cách mặc định giữa quái và người để kích hoạt follow
    public Vector3 distance;            // Khoảng cách hiện tại giữa người và quái

    public Vector3 spawnPoint;
    public Vector3 returnDistance;      // Khoảng cách tối đa quái có thể follow người, nếu lớn hơn khoảng cách này, quái sẽ quay trở lại
   


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        slimeCombat = GetComponent<SlimeCombat>();
        spawnPoint = transform.position;
    }

    private void FixedUpdate()
    {
        distance = player.transform.position - transform.position;
        returnDistance = this.transform.position - spawnPoint;

        // Nếu khoảng cách giữa quái và người chơi lớn hơn khoảng cách follow mặc định
        // Chuyển sang di chuyển tự do
        if (distance.magnitude > activeFollowDistance )
        {
            if (isFollowing)
            {
                ReturnToRespawnPoint();
            }
            // Kiểm tra nếu trạng thái enemy
            if (isIdle)
            {
                StartCoroutine(IdleTime(idleTime));                        // Cố định quái không di chuyển trong thời gian nghỉ ngơi
                animator.SetInteger("State", 0);                           // Animation nghỉ ngơi của quái
                animator.SetFloat("IdleHorizontal", lastXPos);             // Xác định hướng nghỉ ngơi của quái
                animator.SetFloat("IdleVertical", lastYPos);

                // Nếu thời gian nghỉ ngơi hiện tại lớn hơn thời gian nghỉ ngơi mặc định
                if (currentIdleTime >= idleTime)
                {
                    DirectionPosController();                                  // Xác định hướng của quái hiện tại để lấy tọa độ
                                                                               // Đổi hướng di chuyển của quái
                    isIdle = false;
                }
            }
            else
            {
                // Kiểm tra thời gian di chuyển của quái
                // Nếu thời gian di chuyển lớn hơn thời gian di chuyển mặc định
                MovementTime();
                MovementController();

                animator.SetInteger("State", 1);                            // Animation di chuyển của quái
                animator.SetFloat("Horizontal", lastXPos);                  // Xác định hướng di chuyển của quái
                animator.SetFloat("Vertical", lastYPos);
            }

            currentIdleTime += Time.deltaTime;
        }
        else
        {
            FollowPlayer();
        }
    }

    // Điều khiển lệnh di chuyển của enemy
    public void MovementController()
    {
        Vector2 nextDirection = new Vector2(lastXPos, lastYPos);
        //if (facingDown)
        //{
        //    nextDirection = new Vector2(-1, 0);
        //}
        //else if (facingLeft)
        //{
        //    nextDirection = new Vector2(0, 1);
        //}
        //else if (facingUp)
        //{
        //    nextDirection = new Vector2(1, 0);
        //}
        //else if (facingRight)
        //{
        //    nextDirection = new Vector2(0, -1);
        //}
        rb2d.velocity = nextDirection * moveSpeed * Time.deltaTime;
    }

    //Kiểm tra hướng đi của enemy
    public void DirectionPosController()
    {
        if (facingLeft)
        {
            lastXPos = -1;
            lastYPos = 0;
        }
        else if (facingRight)
        {
            lastXPos = 1;
            lastYPos = 0;
        }
        else if (facingUp)
        {
            lastXPos = 0;
            lastYPos = 1;
        }
        else if (facingDown)
        {
            lastXPos = 0;
            lastYPos = -1;
        }
    }

    public void DirectionChange()
    {
        // Nếu quái đang hướng xuống dưới, khi hết thời gian nghỉ ngơi sẽ chuyển trạng thái thành hướng bên trái
        if (facingDown)
        {
            facingLeft = true;
            facingRight = false;
            facingUp = false;
            facingDown = false;
        }
        else if (facingLeft)
        {
            facingLeft = false;
            facingRight = false;
            facingUp = true;
            facingDown = false;
        }
        else if (facingUp)
        {
            facingLeft = false;
            facingRight = true;
            facingUp = false;
            facingDown = false;
        }
        else if (facingRight)
        {
            facingLeft = false;
            facingRight = false;
            facingUp = false;
            facingDown = true;
        }
    }

    public void MovementTime()
    {
        // Nếu thời gian di chuyển vượt quá thời gian cho phép, quái sẽ vào trạng thái nghỉ ngơi
            currentIdleTime = 0;
        if (currentMovementTime >= movementTime)
        {
            DirectionChange();

            isIdle = true;
            currentMovementTime = 0;
        }
        currentMovementTime += Time.deltaTime;
    }

    IEnumerator IdleTime(float second)
    {
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(second);
        rb2d.constraints = RigidbodyConstraints2D.None;
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void FollowPlayer()
    {
        isIdle = false;
        isFollowing = true;

        // Last x point of enemy
        //lastXpoint = transform.position.x;

        if (distance.magnitude > 1.1 && distance.magnitude <= activeFollowDistance)
        {

            // Minimum distance between people and enemy
            Vector3 targetPoint = player.transform.position - distance.normalized ;

            // Follow player
            gameObject.transform.position =
                Vector3.MoveTowards(gameObject.transform.position,
                new Vector3(targetPoint.x, targetPoint.y, transform.position.z), 3 * 1 * Time.deltaTime);

            // Animation run
            animator.SetInteger("State", 1);
            if (player.transform.position.x > this.transform.position.x)
            {
                animator.SetFloat("Horizontal", 1);                  // Xác định hướng di chuyển của quái
                animator.SetFloat("Vertical", 0);
            }
            else
            {
                animator.SetFloat("Horizontal", -1);                  // Xác định hướng di chuyển của quái
                animator.SetFloat("Vertical", 0);
            }

            if (returnDistance.magnitude > 20f)
            {
                ReturnToRespawnPoint();

            }
        }
        else
        {
            animator.SetInteger("State", 0);
            if (player.transform.position.x > this.transform.position.x)
            {
                animator.SetFloat("IdleHorizontal", 1);                  // Xác định hướng di chuyển của quái
                animator.SetFloat("IdleVertical", 0);
            }
            else
            {
                animator.SetFloat("IdleHorizontal", -1);                  // Xác định hướng di chuyển của quái
                animator.SetFloat("IdleVertical", 0);
            }

            // Play attack animation
            //slimeCombat.Attack();
        }
    }

    public void ReturnToRespawnPoint()
    {
        // Cho quái trở về địa điểm hồi sinh khi quái không còn follow player
        gameObject.transform.position =
        Vector3.MoveTowards(gameObject.transform.position,
        new Vector3(spawnPoint.x, spawnPoint.y, transform.position.z), 3 * 1 * Time.deltaTime);
        animator.SetInteger("State", 1);
        if (this.transform.position.x < spawnPoint.x)
        {
            animator.SetFloat("Horizontal", 1);                  // Xác định hướng di chuyển của quái
            animator.SetFloat("Vertical", 0);
        }
        else
        {
            animator.SetFloat("Horizontal", -1);                  // Xác định hướng di chuyển của quái
            animator.SetFloat("Vertical", 0);
        }

        // Nếu quái trở về đúng địa điểm hồi sinh thì sẽ trở về trạng thái nghỉ ngơi và di chuyển như thường
        if (transform.position == spawnPoint)
        {
            isIdle = true;
            isFollowing = false;
            facingDown = true;
            facingUp = false;
            facingRight = false;
            facingLeft = false;
        }
    }
}
