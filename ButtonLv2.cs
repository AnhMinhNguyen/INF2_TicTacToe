using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonLv2 : MonoBehaviour {

	public GameObject mainMenu;
	public GameObject window2;

	public GameObject buttonX;
	public GameObject buttonO;

	public void ChooseX(){
		GameSetting.gameSetting.SetSymbol (Symbol.Value.X);
		buttonX.GetComponent<Image>().color = new Color32(90,255,160,255);
		buttonO.GetComponent<Image>().color = new Color32(255,255,255,255);
	}

	public void ChooseO(){
		GameSetting.gameSetting.SetSymbol (Symbol.Value.O);
		buttonO.GetComponent<Image>().color = new Color32(90,255,160,255);
		buttonX.GetComponent<Image>().color = new Color32(255,255,255,255);

	}

	public void GoBack(){
		window2.SetActive (false);
		mainMenu.SetActive (true);
	}

	public void StartGame(){
		SceneManager.LoadScene (1);
	}
}
