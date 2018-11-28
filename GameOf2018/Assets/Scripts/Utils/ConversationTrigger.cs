using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationTrigger : MonoBehaviour {

	// Use this for initialization
    public List<DialogConvo> dialogs;
    private DialogBoxManager daBoxManagah;

    private void Awake()
    {
        daBoxManagah = FindObjectOfType<DialogBoxManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            List<DialogConvo> tempDialogs = new List<DialogConvo>(dialogs);
            daBoxManagah.ActivateDialogBox();
            daBoxManagah.SetDialogSequence(tempDialogs);
            //DialogBoxManager.instance.ActivateDialogBox();
            //DialogBoxManager.instance.SetDialogSequence(dialogs);
        }
    }
}
