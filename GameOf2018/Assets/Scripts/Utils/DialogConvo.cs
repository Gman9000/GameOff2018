using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogConvo : MonoBehaviour
{

    public Image icon;
    public string textToShow;

    //constructor here for the icon and text to Show <-option 1
    public string speaker;
    // Use this for initialization
    void Start()
    {
        icon = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
