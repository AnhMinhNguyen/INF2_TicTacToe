using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttonlv2 : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject window2;

	public void ChooseX()
    {
        GameSetting.gameSetting.SetSymbol(Symbol.Value.X);
        SceneManager.LoadScene(1);
    }

    public void ChooseO()
    {
        GameSetting.gameSetting.SetSymbol(Symbol.Value.O);
        SceneManager.LoadScene(1);
    }

    public void GoBack()
    {
        window2.SetActive(false);
        mainMenu.SetActive(true);
    }
}
