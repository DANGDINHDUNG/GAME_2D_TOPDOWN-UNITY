using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestBoard : MonoBehaviour, IInteractable
{
    [SerializeField] private List<QuestInformation> _currentQuest;

    public List<QuestInformation> CurrentQuest => _currentQuest;

    public static UnityAction<QuestBoard> OnQuestDisplayRequested;

    public UnityAction<IInteractable> OnInteractionComplete { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void EndInteraction()
    {
        throw new System.NotImplementedException();
    }

    public void Interact(Interactor interactor, out bool interactSuccessful)
    {
        OnQuestDisplayRequested?.Invoke(this);

        interactSuccessful = true;
    }
}
