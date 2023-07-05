using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    public Transform interactionPoint;
    public float interactionPointRadius = 1f;
    public LayerMask interactionLayer;
    public bool isInteraction { get; private set; }

    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(interactionPoint.position, interactionPointRadius, interactionLayer);

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                var interactable = colliders[i].GetComponent<IInteractable>();

                if (interactable != null)
                {
                    StartInteraction(interactable);
                }
            }
        }
    }
    
    void StartInteraction(IInteractable interactable)
    {
        interactable.Interact(this, out bool interactSuccessfull);
        isInteraction = true;
    }

    void EndInteraction()
    {
        isInteraction = false;
    }
}
