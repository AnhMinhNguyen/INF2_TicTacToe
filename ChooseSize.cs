using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseSize : MonoBehaviour {

	GameObject slider;
	int size;
	public GameObject winCondition;

	// Use this for initialization
	void Start () {
		slider = transform.FindChild ("Slider").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameSetting.gameSetting.mode == GameSetting.GameMode.multiOne) {
			slider.GetComponent<Slider> ().maxValue = 33;
		} else {
			slider.GetComponent<Slider> ().maxValue = 6;
		}
		size = (int) slider.GetComponent<Slider> ().value;
		GameSetting.gameSetting.boardSize = size;
		transform.FindChild("Size").GetComponent<Text>().text = size.ToString() + "x" + size.ToString();
		if (size < 5) {
			winCondition.GetComponent<WinCondition> ().slider.GetComponent<Slider> ().value = 3;
			winCondition.GetComponent<WinCondition> ().slider.GetComponent<Slider> ().interactable = false;
		} else {
			winCondition.GetComponent<WinCondition> ().slider.GetComponent<Slider> ().interactable = true;
		}
	}
		
}
