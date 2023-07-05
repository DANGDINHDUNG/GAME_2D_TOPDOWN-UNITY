using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class SpriteTransparency : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Transform player;
    private Color originalColor;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();    
        originalColor = spriteRenderer.color;
        player = GameObject.FindGameObjectWithTag("Collider").GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform == player)
        {
            Color color = spriteRenderer.color;
            color.a = 0.2f;
            spriteRenderer.color = color;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform == player)
        {
            spriteRenderer.color = originalColor;
        }
    }
}
