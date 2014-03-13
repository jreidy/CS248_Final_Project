using UnityEngine;
using System.Collections;

public class CustomPlayerController : MonoBehaviour {
	
	public GameObject rope;
	public GameObject player_controller;
	public GameObject camera_controller;
	public GameObject camera_left;
	
	//GUI GameObjects
	public GameObject BalanceBar;
	public GameObject BalanceMeter;
	public GameObject WindMeter;
	public GameObject CorrectedMeter;
	
	//Current roll and wind values
	private float roll_value = 0;
	private float wind_value = 0;
	
	private float roll_sensitivity = 3.0f;
	
	private WindGenerator wind_generator;
	
	private bool has_fallen = false;

	private bool yaw_is_negative = true;
	
	private int player_health = 100;
	
	void Start () {
		wind_generator = GetComponent<WindGenerator>();
	}
	
	void Update () {
		if (!has_fallen) {
			ApplyWind();
			CheckHeadYaw();
			CheckHeadRoll();
			ApplyCorrection();
		}
	}
	
	void ApplyWind() {
		Vector3 wind_vec = new Vector3(wind_generator.wind_value,0,0);
		Vector3 sway_position =  player_controller.transform.position + wind_vec;
		//camera_controller.transform.position = sway_position;
		wind_value = wind_generator.wind_value;
		UpdateWindMeter();
	}
	
	void CheckHeadYaw() {
		float y_rotation = camera_left.transform.rotation.y;
		if (y_rotation < 0.0f) {
			yaw_is_negative = true;
		} else {
			yaw_is_negative = false;	
		}
	}
	
	void CheckHeadRoll() {
		roll_value = camera_left.transform.localRotation.z;
		UpdateBalanceMeter();
	}
	
	void UpdateWindMeter() {
		float balance_bar_length = BalanceBar.transform.localScale.x;
		float wind_meter_position_x = BalanceBar.transform.localPosition.x + wind_value * balance_bar_length / 2;
		Vector3 wind_meter_vector = new Vector3(wind_meter_position_x,
								WindMeter.transform.localPosition.y,
								WindMeter.transform.localPosition.z);
		WindMeter.transform.localPosition = wind_meter_vector;
	}
	
	void UpdateBalanceMeter() {
		float balance_bar_length = BalanceBar.transform.localScale.x;
		float balance_meter_position_x = BalanceBar.transform.localPosition.x - roll_value * roll_sensitivity * balance_bar_length / 2;
		if (yaw_is_negative) {
			balance_meter_position_x = -balance_meter_position_x;	
		}
		Vector3 balance_meter_vector = new Vector3(balance_meter_position_x,
								BalanceMeter.transform.localPosition.y,
								BalanceMeter.transform.localPosition.z);
		BalanceMeter.transform.localPosition = balance_meter_vector;
	}
	
	void ApplyCorrection() {
		float balance_bar_length = BalanceBar.transform.localScale.x;
		float roll_correction_component = BalanceBar.transform.localPosition.x - roll_value * roll_sensitivity * balance_bar_length / 2;
		if (yaw_is_negative) {
			roll_correction_component = -roll_correction_component;
		}
		float corrected_meter_position_x = BalanceBar.transform.localPosition.x + (wind_value + roll_correction_component) * balance_bar_length / 2;
		Vector3 corrected_meter_vector = new Vector3(corrected_meter_position_x,
								CorrectedMeter.transform.localPosition.y,
								CorrectedMeter.transform.localPosition.z);
		CorrectedMeter.transform.localPosition = corrected_meter_vector;
		print(corrected_meter_position_x);
	}
	
	void ApplyFall() {
		Vector3 fall_vec = new Vector3(0,3,0);
		Vector3 updated_position = player_controller.transform.position - fall_vec;
		player_controller.transform.position = updated_position;
	}
}
