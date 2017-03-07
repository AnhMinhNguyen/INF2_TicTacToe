using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCondition : MonoBehaviour {

	public GameObject slider;
	public int winCon;

	// Use this for initialization
	void Start () {
		slider = transform.FindChild ("Slider").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		winCon = (int) slider.GetComponent<Slider> ().value;
		GameSetting.gameSetting.winCon = winCon;
		transform.FindChild ("Condition").GetComponent<Text> ().text = winCon.ToString ();
	}
}
