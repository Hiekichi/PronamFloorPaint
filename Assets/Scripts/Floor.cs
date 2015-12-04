using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {

	public Sprite FloorYellowSp, FloorGreenSp;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ChangeColor() {
		GameObject go = GameObject.Find ("GameMaster");
		//Debug.Log (go.name);

		if (GetComponent<SpriteRenderer> ().sprite != FloorGreenSp) {
			GetComponent<SpriteRenderer> ().sprite = FloorGreenSp;
			go.SendMessage ("FloorTileCountDown");
		} else {
			GetComponent<SpriteRenderer> ().sprite = FloorYellowSp;
			go.SendMessage ("FloorTileCountUp");
		}
	}
}
