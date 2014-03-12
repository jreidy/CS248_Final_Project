using UnityEngine;
using System.Collections;

public class CustomPlayerController : MonoBehaviour {
	
	public GameObject rope;
	public GameObject player_controller;
	public GameObject camera_controller;
	public GameObject camera_left;
	
	private float look_range_pos = 0.70f;
	
	
	private WindGenerator wind_generator;
	
	bool has_fallen = false;
	
	// Use this for initialization
	void Start () {
		wind_generator = GetComponent<WindGenerator>();
	}
	
	// Update is called once per frame
	void Update () {
		//RestrictHeadMovement();
		if (!has_fallen) {
			ApplyWind();
			CheckHeadYaw();
			CheckHeadRoll();	
		}
	}
	
	void CheckHeadYaw() {
		float y_rotation = camera_left.transform.rotation.y;
		//print(y_rotation);
		if (y_rotation > look_range_pos || y_rotation < -look_range_pos) {
			print ("death");
			has_fallen = true;
			ApplyFall();
		}
			
		
	}
	
	void CheckHeadRoll() {
		float z_rotation = camera_left.transform.rotation.z;
		//print(z_rotation);
	}
	
	// this appears to be impossible
	void RestrictHeadMovement() {
//		float y_rotation = player_controller.transform.rotation.y - camera_controller.transform.rotation.y;
//		if  (y_rotation < look_range) {
//			player_controller.transform.Rotate(0,1,0);
//			//player_controller.transform.rotation = Quaternion.Euler (0,70,0);
//		}
	}
	
	void ApplyWind() {
		Vector3 wind_vec = new Vector3(wind_generator.wind_value,0,0);
		Vector3 sway_position =  player_controller.transform.position - wind_vec;
		camera_controller.transform.position = sway_position;
	}
	
	void ApplyFall() {
		Vector3 fall_vec = new Vector3(0,3,0);
		Vector3 updated_position = player_controller.transform.position - fall_vec;
		player_controller.transform.position = updated_position;
	}
}
