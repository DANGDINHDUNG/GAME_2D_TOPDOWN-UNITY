using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CraftingBench : MonoBehaviour, IInteractable
{
    [SerializeField] private List<CraftingRecipe> _knownRecipe;

    public List<CraftingRecipe> KnownRecipe => _knownRecipe;

    public static UnityAction<CraftingBench> OnCraftingDisplayRequested;

    #region Interaction Interface

    public UnityAction<IInteractable> OnInteractionComplete { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void EndInteraction()
    {
        
    }

    public void Interact(Interactor interactor, out bool interactSuccessful)
    {
        OnCraftingDisplayRequested?.Invoke(this);

        interactSuccessful = true;
    }



    #endregion
}
