using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;  
using TMPro;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.EventSystems;

public class Dialoguemanger : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    private Story currentStory;

    private static Dialoguemanger instance;

    //portrait variables
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";
    [SerializeField] Animator portraitAnimator;

    public bool dialogueIsPlaying { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;
    }

    public static Dialoguemanger GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        //get all of the choices text
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
 
    }

    private void Update()
    { 
    
        if (!dialogueIsPlaying)
         {
            return;     
         }

        if (currentStory.currentChoices.Count == 0 && InputManager1.GetInstance().GetSubmitPressed())
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Ink.Runtime.Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        ContinueStory();

        //reset portrait / layout

        portraitAnimator.Play("default");

        //if (currentStory.canContinue)
        //{
        //    dialogueText.text = currentStory.Continue();
        //}
        //else
        //{
        //    ExitDialogueMode();
        //}
    }

    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            // set text for the current dialogue line
            dialogueText.text = currentStory.Continue();
            // display choices, if any, for this dialogue line
            DisplayChoices();

            HandleTags(currentStory.currentTags);
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    private void HandleTags(List<string> currentTags)
    {
        //loops through tags

        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');

            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be parsed appropriately " + tag);
            }

            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            //handle tag

            switch(tagKey)
            {
                case PORTRAIT_TAG:
                    portraitAnimator.Play(tagValue);
                    break;
                case LAYOUT_TAG:
                    break;
            }
        }



    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        //defensive check to make sure UI can handle choices coming in
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: " + currentChoices.Count);
        }

        int index = 0;
        // enable and initialize the choices up to the amount of choices for this line of dialogue

        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        //go through the remaining choices the UI supports and make sure theyre hidden
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        //Event System requires we clear it first, then wait
        // for at least one frame before we set the current selected object.
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        InputManager1.GetInstance().RegisterSubmitPressed();
        ContinueStory();
    }
}


