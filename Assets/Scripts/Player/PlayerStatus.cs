using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public Animator animator;

    private Rigidbody2D theRB2D;

    [SerializeField] private float rechargeEnergy = 5f;
    public float boundForce = 1000f;
    public PlayerMovement playerMovement;
    [SerializeField] private SpriteRenderer sprite;

    // Player status system 
    public HealthBar healthBar;
    public EnergyBar energyBar;

    //public ScreenShakeController shakeController;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        theRB2D = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBar>();
        energyBar = GameObject.FindGameObjectWithTag("EnergyBar").GetComponent<EnergyBar>();
        //shakeController = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<ScreenShakeController>();

        healthBar.SetMaxHealth(PlayerStatusController.GetInstance().playerCurrentHealth);          // Đặt giá trị tối đa cho thanh máu
        energyBar.SetMaxEnergy(PlayerStatusController.GetInstance().playerCurrentEnergy);          // Đặt giá trị tối đa cho thanh năng lượng
    }

    private void Update()
    {

        if (PlayerStatusController.GetInstance().currentEnergy < PlayerStatusController.GetInstance().playerCurrentEnergy)
        {
            PlayerStatusController.GetInstance().currentEnergy += rechargeEnergy * Time.deltaTime;

            if (PlayerStatusController.GetInstance().currentEnergy > PlayerStatusController.GetInstance().playerCurrentEnergy) PlayerStatusController.GetInstance().currentEnergy = PlayerStatusController.GetInstance().playerCurrentEnergy;
        }
        energyBar.SetEnergy(PlayerStatusController.GetInstance().currentEnergy);

    }

    public void TakeDamage(float damage)
    {
        if (damage - PlayerStatusController.GetInstance().playerCurrentDefend <= 0)
        {
            PlayerStatusController.GetInstance().currentHealth -= 0;
        }
        else
        {
            PlayerStatusController.GetInstance().currentHealth -= (damage - PlayerStatusController.GetInstance().playerCurrentDefend);

        }
        StartCoroutine(Immortal());
        StartCoroutine(FadeToWhite());
        healthBar.SetHealth(PlayerStatusController.GetInstance().currentHealth);

        if (PlayerStatusController.GetInstance().currentHealth <= 0)
        {
            Die();
        }
    }

    public void EnergyDecrease()
    {

        PlayerStatusController.GetInstance().currentEnergy -= 10;
        energyBar.SetEnergy(PlayerStatusController.GetInstance().currentEnergy);

    }

    void Die()
    {
        GUIController.instance.btnGameOver.SetActive(true);
        Time.timeScale = 0;

        // Die animation
        //animator.SetBool("IsDead", true);
    }

    IEnumerator Immortal()
    {
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(2f);
        GetComponent<Collider2D>().enabled = true;

    }

    private IEnumerator FadeToWhite()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.white;

    }
}
