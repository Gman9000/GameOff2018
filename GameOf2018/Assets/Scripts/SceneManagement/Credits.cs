using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Rolls the credits in the Credits scene and manages the buttons that come up afterward
 */
public class Credits : MonoBehaviour
{

    public GameObject objToMove; // object to be moved

    //point that object will move towards
    public Transform endPoint; // ending point
    public float moveSpeed; // how fast the object moves
    private Vector3 currentTarget; // the current point it's going to
    public Vector3 initialPosition; // initial pos of the object

    public bool finishedMoving; // is obj at final position

    public Text titleText; // title text shown at end of credits
    public Button QuitGameButton, ReturnToTitleButton;
    Color rtCol, qgCol;

    // Use this for initialization
    void Start()
    {
        currentTarget = endPoint.transform.position;
        initialPosition = transform.position;
        finishedMoving = false;
        titleText.enabled = false;
        ReturnToTitleButton.gameObject.SetActive(false);
        QuitGameButton.gameObject.SetActive(false);

        setAlphaToZero();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (objToMove != null)
        {
            objToMove.transform.position = Vector3.MoveTowards(objToMove.transform.position, currentTarget, moveSpeed * Time.deltaTime);
        }

        if (objToMove.transform.position == currentTarget)
        {
            finishedMoving = true;
        }

        if (isFinishedMoving())
        {
            titleText.enabled = true;
            ReturnToTitleButton.gameObject.SetActive(true);
            QuitGameButton.gameObject.SetActive(true);
            increaseAlphas();
        }

        //update these to be used in increaseAlphas
        rtCol = ReturnToTitleButton.GetComponent<Image>().color;
        qgCol = QuitGameButton.GetComponent<Image>().color;
    }
    ///checks if obj is done moving and returns answer
    public bool isFinishedMoving()
    {
        if (finishedMoving)
        {
            return true;
        }
        return false;
    }
    ///sets title text, and buttons to be transparent
    public void setAlphaToZero()
    {
        titleText.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        ReturnToTitleButton.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        QuitGameButton.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

    }
    ///sets title text, and buttons to be visible
    public void increaseAlphas()
    {
        //Increase all color's alpha by 0.1 until max
        titleText.color = new Color(titleText.color.r, titleText.color.g, titleText.color.b, titleText.color.a + 0.01f);
        ReturnToTitleButton.GetComponent<Image>().color = new Color(rtCol.r, rtCol.g, rtCol.b, rtCol.a + 0.01f);
        QuitGameButton.GetComponent<Image>().color = new Color(qgCol.r, qgCol.g, qgCol.b, qgCol.a + 0.01f);

    }

}
