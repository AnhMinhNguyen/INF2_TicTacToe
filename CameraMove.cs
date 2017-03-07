using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

	public GameObject cam;
	Vector3 targetUp;
	Vector3 targetDown;
	Vector3 targetRight;
	Vector3 targetLeft;

	public bool up = false;
	public bool down = false;
	public bool right = false;
	public bool left = false;

	public float speed;

	public float boundary;

	void Start(){
		if (GameSetting.gameSetting != null) {
			boundary = (GameSetting.gameSetting.boardSize - 3) * 5;
		} else {
			boundary = 0;
		}
	}

	void FixedUpdate(){
		targetUp = cam.transform.position + new Vector3 (0, 0, 1);
		targetDown = cam.transform.position + new Vector3 (0, 0, -1);
		targetRight = cam.transform.position + new Vector3 (1, 0, 0);
		targetLeft = cam.transform.position + new Vector3 (-1, 0, 0);

		if (up && cam.transform.position.z <= boundary) {
			cam.transform.position = Vector3.Lerp (cam.transform.position, targetUp, speed * Time.deltaTime);
		}
		else if (down && cam.transform.position.z >= -boundary) {
			cam.transform.position = Vector3.Lerp (cam.transform.position, targetDown, speed * Time.deltaTime);
		}
		else if (right && cam.transform.position.x <= boundary) {
			cam.transform.position = Vector3.Lerp (cam.transform.position, targetRight, speed * Time.deltaTime);
		}
		else if (left && cam.transform.position.x >= -boundary) {
			cam.transform.position = Vector3.Lerp (cam.transform.position, targetLeft, speed * Time.deltaTime);
		}

	
	}

	public void Moveup(){
		up = true;
		down = false;
		right = false;
		left = false;
	}

	public void MoveDown(){
		up = false;
		down = true;
		right = false;
		left = false;
	}

	public void MoveRight(){
		up = false;
		down = false;
		right = true;
		left = false;
	}

	public void MoveLeft(){
		up = false;
		down = false;
		right = false;
		left = true;
	}

	public void Stop(){
		up = false;
		down = false;
		right = false;
		left = false;
	}
}
