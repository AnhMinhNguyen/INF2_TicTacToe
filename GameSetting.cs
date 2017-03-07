using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : MonoBehaviour {

	public static GameSetting gameSetting;

	public GameMode mode;

	public Symbol.Value Player1Symbol;

	public int boardSize;
	public int winCon;

	public enum GameMode{
		multiOne,
		single
	}

	void Start(){
		if (gameSetting == null) {
			gameSetting = this;
		} else {
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);
	}

	public void SetMode(GameSetting.GameMode mode){
		this.mode = mode;
	}

	public void SetSymbol(Symbol.Value value){
		Player1Symbol = value;
	}
}
