using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoardLayout : MonoBehaviour {

	public int size;
	public GameObject white;
	public GameObject black;
	GameObject field;

	public static List<Field> allField;

	// Use this for initialization
	void Start () {
		if (GameSetting.gameSetting != null) {
			size = GameSetting.gameSetting.boardSize;
		} else {
			size = 3;
		}
		allField = new List<Field> ();
		if (size % 2 == 0) {
			SetUp2(this.size / 2);
		} else {
			SetUp1((this.size - 1) / 2);
		}

	}
	
	// Update is called once per frame
	void Update () {

	}

	void SetUp1(int size){
		for (int x = -size; x <= size; x++) {
			for (int z = -size; z <= size; z++) {
				if ((Mathf.Abs(x)%2) == (Mathf.Abs(z)%2)) {
					field = Instantiate (white, new Vector3 (x * 10, 0, z * 10), Quaternion.identity, transform);
					allField.Add(field.GetComponent<Field>());
				}
				if ((Mathf.Abs(x) % 2) != (Mathf.Abs(z) % 2)) {
					field= Instantiate (black, new Vector3 (x * 10, 0, z * 10), Quaternion.identity, transform);
					allField.Add (field.GetComponent<Field> ());
				}

			}
		}
	}

	void SetUp2(int size){
		for (int x = -size; x < size; x++) {
			for (int z = -size; z < size; z++) {
				if ((Mathf.Abs(x)%2) == (Mathf.Abs(z)%2)) {
					field = Instantiate (white, new Vector3 (x * 10, 0, z * 10), Quaternion.identity, transform);
					allField.Add(field.GetComponent<Field>());
				}
				if ((Mathf.Abs(x) % 2) != (Mathf.Abs(z) % 2)) {
					field= Instantiate (black, new Vector3 (x * 10, 0, z * 10), Quaternion.identity, transform);
					allField.Add (field.GetComponent<Field> ());
				}

			}
		}
	}
}
