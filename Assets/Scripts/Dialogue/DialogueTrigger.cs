using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    // Check if have cutscene after finish dialogue
    [SerializeField] private bool triggerCutScene = false;
    [SerializeField] private PlayableDirector playableDirection;

    private bool playerInRange;

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                DialogueManager.GetInstance().triggerCutScene = triggerCutScene;
                if (!triggerCutScene)
                {
                    DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                }
                else
                {
                    DialogueManager.GetInstance().EnterDialogueModeWithCutScene(inkJSON, playableDirection);
                }
            }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    public void CutSceneDialogue()
    {
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            playerInRange = false;
        }
    }
}
