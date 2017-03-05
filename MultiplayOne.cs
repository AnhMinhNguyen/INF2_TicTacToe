using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayOne : IGameState
{

    public GameObject X;
    public GameObject O;
    bool vsMode;
    GameObject symbol;


    public int winCon;

    private readonly GameManager gm;

    List<Field> allField;

    public MultiplayOne(GameManager gm)
    {
        this.gm = gm;
    }


    public void Run()
    {
        if (gm.Play())
        {
            if (gm.CheckWin(Symbol.Value.X))
            {
                gm.WinMulti(Symbol.Value.X);
                ReturnToDefault();
            }
            else
            {
                if (gm.CheckTie())
                {
                    gm.Tie();
                    ReturnToDefault();
                }
            }
            if (gm.CheckWin(Symbol.Value.O))
            {
                gm.WinMulti(Symbol.Value.O);
                ReturnToDefault();
            }
            else
            {
                if (gm.CheckTie())
                {
                    gm.Tie();
                    ReturnToDefault();
                }
            }
            gm.Turn();
        }
    }
    public void ReturnToDefault()
    {
        gm.gameState = gm.def;
    }
}
