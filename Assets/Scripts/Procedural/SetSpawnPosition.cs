using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSpawnPosition : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        player.transform.position = this.gameObject.transform.position;
    }
}
