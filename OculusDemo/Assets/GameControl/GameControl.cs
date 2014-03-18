using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {

	public GameObject player;
	public GameObject platform;
	
	private GameObject player_instance;

	private CustomPlayerController PlayerController;

	//Vector3 Positions
	Vector3 game_pos_vec = new Vector3(0.5f,1.5f,0.9f);
	Vector3 menu_pos_vec = new Vector3(-15.0f,-91.0f,-18.0f);


	//game mode control
	private bool is_playing = false;

	// Use this for initialization
	void Start () {
		//player_instance = new GameObject();
		player_instance = (GameObject)Instantiate(player);
		player_instance.transform.parent = platform.transform;
		player_instance.transform.localPosition = menu_pos_vec;
		PlayerController = player_instance.GetComponentInChildren<CustomPlayerController>();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp ("g")) {
			ChangeGameMode();

		}
	}

	void ChangeGameMode() {
		is_playing = !is_playing;
		if (is_playing) {
			PlayerController.game_mode = true;
			print("we're playing noaw");
			player_instance.transform.localPosition = game_pos_vec;
		} else {
			PlayerController.game_mode = false;
			print("we're not playing noaw");
			player_instance.transform.localPosition = menu_pos_vec;
		}
	}
}
