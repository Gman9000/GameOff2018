using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "DialogConvo", menuName = "DialogConvo")]
public class DialogConvo : ScriptableObject
{

    public string speaker;
    public Sprite sprite;
    [TextArea]
    public string textToShow;

}
