using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultState : IGameState {

    private readonly GameManager gm;

    public DefaultState(GameManager gm)
    {
        this.gm = gm;
    }

	public void Run()
    {

    }
}
