using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBoxManager : MonoBehaviour
{

    public static DialogBoxManager instance;

    public GameObject textContainer;
    private DialogConvo currentDialog;
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
        if (dialogIndex >= 0 && dialogIndex <= conversationToHave.Count - 1)
        {
            populateConvoByIndex(dialogIndex);
            dialogIndex++;
        }
        else
        {
            dialogIndex = -1;
        }
    }

    void SetDialogSequence(List<DialogConvo> sequenceToAdd)
    {
        conversationToHave = sequenceToAdd;
    }

    void setIconAndSpeaker(Image icn, string speaker)
    {
        currentDialog.icon = icn;
        currentDialog.speaker = speaker;
    }

    void populateTextBox(DialogConvo convs)
    {
        currentDialog.textToShow = convs.textToShow;
    }

    void populateConvoByIndex(int index)
    {
        setIconAndSpeaker(conversationToHave[index].icon, conversationToHave[index].speaker);
        populateTextBox(conversationToHave[index]);
    }


    void closeDialogBox()
    {
        conversationToHave.Clear();
        currentDialog.textToShow = "";
    }

    void disableTextBox()
    {
        textContainer.SetActive(false);
    }

    void enabletextBox()
    {
        textContainer.SetActive(true);
    }
}
