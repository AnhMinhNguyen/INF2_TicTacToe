using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttonlv1 : MonoBehaviour {

    public GameObject window2;

    public GameObject mainMenu;

	public void MultiOne()
    {
        GameSetting.gameSetting.SetMode(GameSetting.GameMode.multiOne);
        GoToWindow2();
        window2.transform.FindChild("Background").FindChild("Title").GetComponent<Text>().text = "Player1 choose";
    }

    public void Single()
    {
        GameSetting.gameSetting.SetMode(GameSetting.GameMode.single);
        GoToWindow2();
        window2.transform.FindChild("Background").FindChild("Title").GetComponent<Text>().text = "Choose X or O";
        
    }

    public void GoToWindow2()
    {
        mainMenu.SetActive(false);
        window2.SetActive(true);
    }
}
