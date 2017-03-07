using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{

    public GameObject winSingle;
    public GameObject loseSingle;
    public GameObject winMulti;
    public GameObject tie;

    public GameObject X;
    public GameObject O;

    GameObject symbol;
    GameObject currentSym;
    GameObject comSym;

    public int winCon;

    List<Field> allField = new List<Field>();
    List<Field> xField = new List<Field>();
    List<Field> oField = new List<Field>();

    public List<Field> comAllField;
    public List<Field> playAllField;

    public Symbol.Value comValue;
    public Symbol.Value playerValue;

    public IGameState gameState;
    public MultiplayOne mpOne;
    public Singleplay sp;
    public DefaultState def;

    public bool playerTurn = true;

    // Use this for initialization
    void Start()
    {
        mpOne = new MultiplayOne(this);
        sp = new Singleplay(this);
        def = new DefaultState(this);
        allField = BoardLayout.allField;

        if (GameSetting.gameSetting != null)
        {
            winCon = GameSetting.gameSetting.winCon;

            if (GameSetting.gameSetting.mode == GameSetting.GameMode.multiOne)
            {
                gameState = mpOne;
            }
            else if (GameSetting.gameSetting.mode == GameSetting.GameMode.single)
            {
                gameState = sp;
            }

            if (GameSetting.gameSetting.Player1Symbol == Symbol.Value.O)
            {
                currentSym = O;
                comSym = X;
                comAllField = xField;
                playAllField = oField;
                comValue = Symbol.Value.X;
                playerValue = Symbol.Value.O;

            }
            else if (GameSetting.gameSetting.Player1Symbol == Symbol.Value.X)
            {
                currentSym = X;
                comSym = O;
                comAllField = oField;
                playAllField = xField;
                comValue = Symbol.Value.O;
                playerValue = Symbol.Value.X;
            }
        }
        else
        {
            winCon = 3;
            gameState = sp;
            currentSym = X;
            comSym = O;
            comAllField = oField;
            playAllField = xField;
            comValue = Symbol.Value.O;
            playerValue = Symbol.Value.X;
        }



    }

    // Update is called once per frame
    void Update()
    {
        gameState.Run();
    }

    public void Turn()
    {
        if (currentSym.GetComponent<Symbol>().value.Equals(Symbol.Value.X))
        {
            currentSym = O;
        }
        else
        {
            currentSym = X;
        }
    }

    public bool Play()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject.layer == 8)
                    {
                        if (hit.transform.GetComponent<Field>().sym == null)
                        {
                            symbol = Instantiate(currentSym, hit.transform.position, Quaternion.identity);
                            hit.transform.GetComponent<Field>().sym = symbol.GetComponent<Symbol>();
                            playAllField.Add(hit.transform.GetComponent<Field>());
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    public bool CheckWin(Symbol.Value v)
    {
        for (int i = 0; i < allField.Count; i++)
        {
            if (CheckWinRow(allField[i], v))
            {
                return true;
            }
            if (CheckWinCollum(allField[i], v))
            {
                return true;
            }
            if (CheckWinDia1(allField[i], v))
            {
                return true;
            }
            if (CheckWinDia2(allField[i], v))
            {
                return true;
            }
        }
        return false;
    }

    public bool CheckWinCollum(Field f, Symbol.Value v)
    {
        Vector3 fieldPos = f.transform.position;
        for (int a = 0; a < winCon; a++)
        {
            if (GetField(fieldPos + new Vector3(0, 0, a * 10)) == null)
                return false;
            if (GetField(fieldPos + new Vector3(0, 0, a * 10)).sym == null)
                return false;
            if (GetField(fieldPos + new Vector3(0, 0, a * 10)).sym.value != v)
                return false;
        }
        return true;
    }

    public bool CheckWinRow(Field f, Symbol.Value v)
    {
        Vector3 fieldPos = f.transform.position;
        for (int a = 0; a < winCon; a++)
        {
            if (GetField(fieldPos + new Vector3(a * 10, 0, 0)) == null)
                return false;
            if (GetField(fieldPos + new Vector3(a * 10, 0, 0)).sym == null)
                return false;
            if (GetField(fieldPos + new Vector3(a * 10, 0, 0)).sym.value != v)
                return false;
        }
        return true;
    }

    public bool CheckWinDia1(Field f, Symbol.Value v)
    {
        Vector3 fieldPos = f.transform.position;
        for (int a = 0; a < winCon; a++)
        {
            if (GetField(fieldPos + new Vector3(a * 10, 0, a * 10)) == null)
                return false;
            if (GetField(fieldPos + new Vector3(a * 10, 0, a * 10)).sym == null)
                return false;
            if (GetField(fieldPos + new Vector3(a * 10, 0, a * 10)).sym.value != v)
                return false;
        }
        return true;
    }

    public bool CheckWinDia2(Field f, Symbol.Value v)
    {
        Vector3 fieldPos = f.transform.position;
        for (int a = 0; a < winCon; a++)
        {
            if (GetField(fieldPos + new Vector3(a * 10, 0, -a * 10)) == null)
                return false;
            if (GetField(fieldPos + new Vector3(a * 10, 0, -a * 10)).sym == null)
                return false;
            if (GetField(fieldPos + new Vector3(a * 10, 0, -a * 10)).sym.value != v)
                return false;
        }
        return true;
    }

    public void WinMulti(Symbol.Value v)
    {
        if (v == Symbol.Value.O)
        {
            winMulti.transform.FindChild("Background").FindChild("Headline").GetComponent<Text>().text = "O WIN";
        }
        else
        {
            winMulti.transform.FindChild("Background").FindChild("Headline").GetComponent<Text>().text = "X WIN";
        }
        winMulti.SetActive(true);
    }

    public void WinSingle()
    {
        winSingle.SetActive(true);
    }

    public void Tie()
    {
        tie.SetActive(true);
    }

    public void Lose()
    {
        loseSingle.SetActive(true);
    }

    public bool CheckTie()
    {
        for (int i = 0; i < allField.Count; i++)
        {
            if (allField[i].transform.GetComponent<Field>().sym == null)
            {
                return false;
            }
        }
        return true;
    }

    Field GetField(Vector3 v3)
    {
        Collider[] hitCollider = Physics.OverlapSphere(v3, 0.1f);
        for (int i = 0; i < hitCollider.Length; i++)
        {
            if (hitCollider[i].gameObject.layer == 8)
            {
                return hitCollider[i].transform.GetComponent<Field>();
            }
        }
        return null;
    }

    public Field FirstMove(Field f)
    {
        if (GetField(f.transform.position + new Vector3(10, 0, 10)) != null)
        {
            return GetField(f.transform.position + new Vector3(10, 0, 10));
        }
        else if (GetField(f.transform.position - new Vector3(10, 0, 10)) != null)
        {
            return GetField(f.transform.position - new Vector3(10, 0, 10));
        }
        else if (GetField(f.transform.position + new Vector3(-10, 0, 10)) != null)
        {
            return GetField(f.transform.position + new Vector3(-10, 0, 10));
        }
        else
        {
            return GetField(f.transform.position - new Vector3(-10, 0, 10));
        }
    }

    public Field AI()
    {

        Field f = null;

        int Maxpoint = -200;
        int PlayerPoint = 0;
        int ComPoint = 0;

        allField = BoardLayout.allField;

        for (int i = 0; i < allField.Count; i++)
        {
            if (allField[i].transform.GetComponent<Field>().sym == null)
            {

                // Beginnt das Kopfrechnen
                allField[i].transform.GetComponent<Field>().sym = comSym.GetComponent<Symbol>();
                comAllField.Add(allField[i]);

                // Prüfen, ob diese Bewegung schon gewonnen ist und wenn ja, gibt diesen Feld zurück
                if (CheckWin(comValue))
                {
                    Maxpoint = 100;
                    f = allField[i];
                    return f;
                }
                // Wenn es noch nicht gewonnen ist, zählt es die Länge der Kette
                else
                {
                    for (int a = 0; a < comAllField.Count; a++)
                    {
                        if (PointCalRow(allField[a], comValue) >= ComPoint)
                            ComPoint = PointCalRow(allField[a], comValue);
                        if (PointCalCollum(allField[a], comValue) >= ComPoint)
                            ComPoint = PointCalCollum(allField[a], comValue);
                        if (PointCalDia1(allField[a], comValue) >= ComPoint)
                            ComPoint = PointCalDia1(allField[a], comValue);
                        if (PointCalDia2(allField[a], comValue) >= ComPoint)
                            ComPoint = PointCalDia2(allField[a], comValue);
                    }
                }


                // Versuch zu spielen als Spieler
                for (int z = 0; z < allField.Count; z++)
                {
                    if (allField[z].transform.GetComponent<Field>().sym == null)
                    {

                        // Beginnt das Kopfrechnen
                        allField[z].transform.GetComponent<Field>().sym = currentSym.GetComponent<Symbol>();
                        playAllField.Add(allField[z]);

                        // Es findet die Bewegung, mit der der Spieler gewinnen wird und 100P hat
                        if (CheckWin(playerValue))
                        {
                            PlayerPoint = 100;
                        }

                        // Wenn es die Bewegung nicht finden kann, zählt es der max. Punkt, den der Spieler hat
                        else
                        {
                            for (int a = 0; a < playAllField.Count; a++)
                            {
                                if (PointCalRow(allField[a], playerValue) >= PlayerPoint)
                                    PlayerPoint = PointCalRow(allField[a], playerValue);
                                if (PointCalCollum(allField[a], playerValue) >= PlayerPoint)
                                    PlayerPoint = PointCalCollum(allField[a], playerValue);
                                if (PointCalDia1(allField[a], playerValue) >= PlayerPoint)
                                    PlayerPoint = PointCalDia1(allField[a], playerValue);
                                if (PointCalDia2(allField[a], playerValue) >= PlayerPoint)
                                    PlayerPoint = PointCalDia2(allField[a], playerValue);
                            }
                        }
                        // Entfernt dieses Kopfrechnen
                        playAllField.Remove(allField[z]);
                        allField[z].transform.GetComponent<Field>().sym = null;
                    }
                }

                // Vergleichen der Punkt von AI mit dem vom Spieler
                if (Maxpoint <= (ComPoint - PlayerPoint))
                {
                    // Wenn der Maxpoint besser ist, wechseln diesen neuen mit den alten
                    Maxpoint = ComPoint - PlayerPoint;

                    // Diesen Feld speichern
                    f = allField[i];
                    ComPoint = 0;
                    PlayerPoint = 0;
                }
                else
                {
                    ComPoint = 0;
                    PlayerPoint = 0;
                }

                comAllField.Remove(allField[i]);
                allField[i].transform.GetComponent<Field>().sym = null;
            }
        }

        // Gibt diesen Feld zurück
        return f;
    }

    public void ComPlay(Field f)
    {
        symbol = Instantiate(comSym, f.transform.position, Quaternion.identity);
        f.sym = symbol.GetComponent<Symbol>();
        comAllField.Add(f);
    }



    public int PointCalCollum(Field f, Symbol.Value v)
    {
        int point = 0;
        Vector3 fieldPos = f.transform.position;
        if (GetField(fieldPos - new Vector3(0, 0, 10)) != null && GetField(fieldPos - new Vector3(0, 0, 10)).sym != null && GetField(fieldPos - new Vector3(0, 0, 10)).sym.value != v)
        {
            point -= 1;
        }
        for (int a = 0; a < winCon; a++)
        {
            if (GetField(fieldPos + new Vector3(0, 0, a * 10)) == null)
                return point;
            if (GetField(fieldPos + new Vector3(0, 0, a * 10)).sym == null)
                return point;
            if (GetField(fieldPos + new Vector3(0, 0, a * 10)).sym.value != v)
            {
                point -= 1;
                return point;
            }
            else
            {
                point += 1;
            }
        }

        return point;

    }

    public int PointCalRow(Field f, Symbol.Value v)
    {
        int point = 0;
        Vector3 fieldPos = f.transform.position;
        if (GetField(fieldPos - new Vector3(10, 0, 0)) != null && GetField(fieldPos - new Vector3(10, 0, 0)).sym != null && GetField(fieldPos - new Vector3(10, 0, 0)).sym.value != v)
        {
            point -= 1;
        }
        for (int a = 0; a < winCon; a++)
        {
            if (GetField(fieldPos + new Vector3(a * 10, 0, 0)) == null)
                return point;
            if (GetField(fieldPos + new Vector3(a * 10, 0, 0)).sym == null)
                return point;
            if (GetField(fieldPos + new Vector3(a * 10, 0, 0)).sym.value != v)
            {
                point -= 1;
                return point;
            }
            else
            {
                point += 1;
            }
        }

        return point;

    }

    public int PointCalDia1(Field f, Symbol.Value v)
    {
        int point = 0;
        Vector3 fieldPos = f.transform.position;
        if (GetField(fieldPos - new Vector3(10, 0, 10)) != null && GetField(fieldPos - new Vector3(10, 0, 10)).sym != null && GetField(fieldPos - new Vector3(10, 0, 10)).sym.value != v)
        {
            point -= 1;
        }
        for (int a = 0; a < winCon; a++)
        {
            if (GetField(fieldPos + new Vector3(a * 10, 0, a * 10)) == null)
                return point;
            if (GetField(fieldPos + new Vector3(a * 10, 0, a * 10)).sym == null)
                return point;
            if (GetField(fieldPos + new Vector3(a * 10, 0, a * 10)).sym.value != v)
            {
                point -= 1;
                return point;
            }
            else
            {
                point += 1;
            }
        }

        return point;

    }

    public int PointCalDia2(Field f, Symbol.Value v)
    {
        int point = 0;
        Vector3 fieldPos = f.transform.position;
        if (GetField(fieldPos - new Vector3(10, 0, -10)) != null && GetField(fieldPos - new Vector3(10, 0, -10)).sym != null && GetField(fieldPos - new Vector3(10, 0, -10)).sym.value != v)
        {
            point -= 1;
        }
        for (int a = 0; a < winCon; a++)
        {
            if (GetField(fieldPos + new Vector3(a * 10, 0, -a * 10)) == null)
                return point;
            if (GetField(fieldPos + new Vector3(a * 10, 0, -a * 10)).sym == null)
                return point;
            if (GetField(fieldPos + new Vector3(a * 10, 0, -a * 10)).sym.value != v)
            {
                point -= 1;
                return point;
            }
            else
            {
                point += 1;
            }
        }

        return point;

    }
}
