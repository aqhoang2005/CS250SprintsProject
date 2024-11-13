using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField]
    private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    public bool playerInRange;
    public static DialogueTrigger instance;

    public static DialogueTrigger GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange && !Dialoguemanger.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);

            if (InputManager1.GetInstance().GetInteractPressed())
            {
                Dialoguemanger.GetInstance().EnterDialogueMode(inkJSON);
            }

        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
