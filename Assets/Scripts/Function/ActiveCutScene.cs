using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ActiveCutScene : MonoBehaviour
{
    [SerializeField] private PlayableDirector playableDirection;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playableDirection.Play();
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
