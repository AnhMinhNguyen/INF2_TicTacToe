using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleplay : IGameState {

	public bool firstTurn = true;

	private readonly GameManager gm;

	public Singleplay(GameManager gm){
		this.gm = gm;
	}

	public void Run(){
		if (firstTurn) {
			if (gm.Play ()) {
				gm.playerTurn = false;
				gm.ComPlay(gm.FirstMove(gm.playAllField[0]));
				firstTurn = false;
				gm.playerTurn = true;
			}
		}

		if (gm.playerTurn) {
			if (gm.Play()) {
				gm.playerTurn = false;
				if (gm.CheckWin (gm.playerValue)) {
					gm.WinSingle ();
					ReturntoDefault ();
					return;
				} else {
					if (gm.CheckTie ()) {
						gm.Tie ();
						ReturntoDefault ();
						return;
					}
				}
				gm.ComPlay (gm.AI ());
				if (gm.CheckWin (gm.comValue)) {
					gm.Lose ();
					ReturntoDefault ();
					return;
				} else {
					if (gm.CheckTie ()) {
						gm.Tie ();
						ReturntoDefault ();
						return;
					}
				}
				gm.playerTurn = true;
			}
		}
	}

	public void ReturntoDefault(){
		gm.gameState = gm.def;
	}
}
