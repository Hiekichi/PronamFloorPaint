using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {
	int gameStatus; // 0:before  1:game 2:gameover
	int stageNumber;
	int floorTileCount;
	string[,] stageCell = new string[21, 13];
	GameObject[,] actors = new GameObject[19,11];
	GameObject mainActor;
	//public Transform RockTr, BlueRockTr, BlockTr, FloorTr, PronamaTr;
	public GameObject BlockGO, FloorGO, PronamaGO;

	int left = -320;
	int right = 320;
	int top = 192;
	int bottom = -192;
	int diff = 32;
	

	// Use this for initialization
	void Start () {
		stageNumber = 1;
		LoadStageData (stageNumber);
		CreateGameFieldWall ();
		StartStage ();
	}

	void LoadStageData(int number) {
		string filename = string.Format ("Map{0:00}.txt", number);
		Debug.Log (filename);
		TextAsset textAsset = (TextAsset)Resources.Load (filename);
		string allString = textAsset.text;

		char[] delimiterChars = {'\r', '\n'};
		string[] recStrings = allString.Split (delimiterChars);

		for (int y = 0; y < 13; ++y) {
			for (int x = 0; x < 21; ++x) {
				stageCell [x, y] = recStrings [y + 1].Substring (x, 1);
			}
		}
	}

	void StartStage() {
		gameStatus = 0;
		CreateGameFieldStage ();
		gameStatus = 1;
		StartGame();
	}

	void StartGame() {
		
	}

	// Update is called once per frame
	void Update () {
		if (gameStatus == 1 && floorTileCount == 0) {
			NextStage ();
		}
	}

	void NextStage() {
		// 配置した役者さんたち（外周除く）にご退場頂く
		Destroy(mainActor); // プロ生ちゃん
		for (int x = 0; x < 19; ++x) { // 床やブロック
			for (int y = 0; y < 11; ++y) {
				Destroy (actors[x,y]);
			}
		}
		stageNumber++;
		LoadStageData (stageNumber);
		StartStage ();
	}

	void CreateGameFieldWall() {
		Vector3 v = new Vector3 ();

		// 周りの壁(1) 上と下
		for (int x = 0; x < 21; ++x) {
			v.x = left + (diff * x);
			v.y = bottom;
			Instantiate (BlockGO, v, transform.rotation);  
			v.y = top;
			Instantiate (BlockGO, v, transform.rotation);
		}
		// 周りの壁(2) 左右の端
		for (int y = 0; y < 11; ++y) {
			v.x = left;
			v.y = bottom + diff + (diff * y);
			Instantiate (BlockGO, v, transform.rotation);  
			v.x = right;
			Instantiate (BlockGO, v, transform.rotation);  
		}
	}

	void CreateGameFieldStage() {
		Vector3 v = new Vector3 ();


		floorTileCount = 1;
		for (int y = 1; y < 12; ++y) {
			//Debug.Log (recStrings [y + 1]);
			for (int x = 1; x < 20; ++x) {
				v.y = top - (diff * y);
				v.x = left + (diff * x);
				string s = stageCell[x,y]; //###
				//Debug.Log (s);
				if ( s.Equals("P")) {
					actors[x - 1,y - 1] = (GameObject)Instantiate (FloorGO, v, transform.rotation);
					mainActor = (GameObject)Instantiate (PronamaGO, v, transform.rotation);
				}
				else if ( s.Equals("B")) {
					actors[x - 1,y - 1] = (GameObject)Instantiate (BlockGO, v, transform.rotation);
				}
				else { //if ( s.Equals(".")) {
					actors[x - 1,y - 1] = (GameObject)Instantiate (FloorGO, v, transform.rotation);
					floorTileCount++;
				}

			}
		}
	}

	void FloorTileCountUp() {
		floorTileCount++;
	}
	void FloorTileCountDown() {
		floorTileCount--;
	}

	public string GetStageCell(int x, int y) {
		return stageCell [x, y];
	}
}
