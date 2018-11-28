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
    private Platformer[] myPlatformers;
    public Platformer[] MyPlatformers
    {
        get
        {
            return myPlatformers;
        }
    }

    void Awake()
    {
        myPlatformers = FindObjectsOfType<Platformer>();
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
            if (Constants.PlayerInput.IsReleasingSpace)
            {
                dialogIndex++;
            }
        }
        else
        {
            dialogIndex = -1;
        }
    }

    void deactivatePlatformers() {
        foreach(Platformer plat in myPlatformers) {
            plat.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            plat.gameObject.GetComponent<Animator>().SetFloat("velocity", Vector2.zero.x);
            plat.gameObject.GetComponent<Rigidbody2D>().gravityScale = plat.gameObject.GetComponent<PlayerPlatformer>().gravityScale;
            plat.enabled = false;
        }
    }

    void activatePlatformers() {
        foreach(Platformer plat in myPlatformers) {
            plat.enabled = true;
        }
    }

    public void SetDialogSequence(List<DialogConvo> sequenceToAdd)
    {
        conversationToHave = sequenceToAdd;
        dialogIndex = 0;
        deactivatePlatformers();
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
    void closeDialogBox()
    {
        conversationToHave.Clear();
        //textToShow.text = "";
        dialogIndex = -1;
        deactivateDialogBox();
        activatePlatformers();
    }
}
