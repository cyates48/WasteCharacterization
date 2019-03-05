using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour {

	public GameObject[] tutorials;
	public Button nextButton, startButton;
	int tut_index;


	// Use this for initialization
	void Start () {
		tut_index = 0;
	}

	void OnEnable() {
		nextButton.gameObject.SetActive(true);
	}
	
	public void NextTut() {
		tutorials[tut_index].SetActive(false);
		tut_index += 1;
		tutorials[tut_index].SetActive(true);

		nextButton.interactable = false;
		Invoke("enableNext", 0.5f);

		if (tut_index == tutorials.Length - 1) {
			nextButton.gameObject.SetActive(false);
			startButton.gameObject.SetActive(true);
		}
	}

	public void disableTut() {
		gameObject.SetActive(false);
	}

	void enableNext() {
		nextButton.interactable = true;
	}
}
