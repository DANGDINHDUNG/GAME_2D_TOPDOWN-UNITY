using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePlayerPos : MonoBehaviour
{
    public GameObject player;
    public HealthBar healthBar;
    public float pX, pY;

    public bool inDoor = false;

    private void Awake()
    {
        healthBar = GameObject.Find("Health Bar").GetComponent<HealthBar>();
    }

    public void PlayerPosSave()
    {
        PlayerPrefs.SetFloat("p_x", player.transform.position.x);
        PlayerPrefs.SetFloat("p_y", player.transform.position.y);
        //PlayerPrefs.SetInt("Saved", 1);
        PlayerPrefs.Save();
    }

    public void PlayerPosLoad()
    {

        pX = PlayerPrefs.GetFloat("p_x");
        pY = PlayerPrefs.GetFloat("p_y");
        //PlayerPrefs.SetInt("TimeToLoad", 1);
        PlayerPrefs.Save();
        player.transform.position = new Vector2(pX, pY - 2);
    }

    public void PlayerReset()
    {
        player.transform.position = new Vector2(0, (float)-8.5);
    }

    public virtual void GameOver()
    {
        GUIController.instance.btnGameOver.SetActive(false);
        SceneManager.LoadScene("SampleScene");
        PlayerPosLoad();
        Time.timeScale = 1;
        PlayerStatusController.GetInstance().currentHealth = PlayerStatusController.GetInstance().playerCurrentHealth;
        healthBar.SetHealth(PlayerStatusController.GetInstance().playerCurrentHealth);
    }
}
