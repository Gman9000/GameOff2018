using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cutSceneChange : MonoBehaviour {
    private LevelManager theLevelManager;
	// Use this for initialization
	void Start () {
        theLevelManager = FindObjectOfType<LevelManager>();
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            theLevelManager.NextLevel("CutScene");
        }
    }


}
