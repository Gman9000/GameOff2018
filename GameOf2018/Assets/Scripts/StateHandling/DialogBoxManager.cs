using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBoxManager : MonoBehaviour
{

    public static DialogBoxManager instance;

    public GameObject textContainer;
    public Image icon;
    public Text textToShow;
    public Text currentSpeaker;

    //private DialogConvo currentDialog;
    private List<DialogConvo> conversationToHave;
    private int dialogIndex;

    void Awake()
    {
        dialogIndex = -1;
        textContainer.SetActive(false);
        conversationToHave = new List<DialogConvo>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Constants.PlayerInput.IsPressingEscape)
        {
            closeDialogBox();
        }
        else if (dialogIndex >= 0 && dialogIndex <= conversationToHave.Count - 1)
        {
            populateConvoByIndex(dialogIndex);
            dialogIndex++;
        }
        else
        {
            dialogIndex = -1;
        }
    }
    


    public void SetDialogSequence(List<DialogConvo> sequenceToAdd)
    {
        conversationToHave = sequenceToAdd;
        dialogIndex = 0;
    }

    void setIconAndSpeaker(Image icn, string speaker)
    {
        icon.sprite = icn.sprite;
        currentSpeaker.text = speaker;
    }

    void populateTextBox(DialogConvo convs)
    {
        textToShow.text = convs.textToShow;
    }

    void populateConvoByIndex(int index)
    {
        setIconAndSpeaker(conversationToHave[index].icon, conversationToHave[index].speaker);
        populateTextBox(conversationToHave[index]);
    }


    void closeDialogBox()
    {
        conversationToHave.Clear();
        //textToShow.text = "";
        dialogIndex = -1;
        deactivateDialogBox();
    }

    void deactivateDialogBox()
    {
        textContainer.SetActive(false);
        icon.gameObject.SetActive(false);
        textToShow.gameObject.SetActive(false);
        currentSpeaker.gameObject.SetActive(false);
    }

    public void ActivateDialogBox()
    {
        textContainer.SetActive(true);
        icon.gameObject.SetActive(true);
        textToShow.gameObject.SetActive(true);
        currentSpeaker.gameObject.SetActive(true);
    }

    void enabletextBox()
    {
        textContainer.SetActive(true);
    }
}
