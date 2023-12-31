using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlimeCombat : MonoBehaviour
{
    public Transform hitPoint;
    public LayerMask playerLayer;
    public Transform player;

    public int attackDamage = 50;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //shakeController = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<ScreenShakeController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerStatus>().TakeDamage(attackDamage);
        }
    }
}
