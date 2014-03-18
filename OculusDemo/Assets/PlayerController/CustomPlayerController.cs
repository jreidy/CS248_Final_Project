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

	//private instances of other classes
	private WindGenerator wind_generator;
	private CoinGenerator coin_generator;
	
	private bool has_fallen = false;

	private bool yaw_is_negative = true;
	
	private float player_health = 1.0f;
	private float balance_bar_scale_x = 2.0f;
	private float balance_bar_length;
	private float balance_bar_length_init;
	
	private float prev_roll_correction_component;
	private float prev_roll_component;

	public bool game_mode = false;
	
	void Start () {
		wind_generator = GetComponent<WindGenerator>();
		coin_generator = GetComponent<CoinGenerator>();
		coin_generator.should_generate = false;
		balance_bar_length = player_health * balance_bar_scale_x;
		balance_bar_length_init = balance_bar_length;
	}

	void Update () {
		if (game_mode) {
			if (!has_fallen) {
				if (Input.GetKey ("f")) player_health -= 0.1f;
				coin_generator.should_generate = true;
				UpdateBalanceBar();
				ApplyWind();
				CheckHeadYaw();
				CheckHeadRoll();
				ApplyCorrection();
			} else {
				// has fallen script
				coin_generator.should_generate = false;
			}
		} else { // at homescreen
			player_health = 1.0f;
			balance_bar_length = player_health * balance_bar_scale_x;
			has_fallen = false;
			coin_generator.should_generate = false;
			ResetMeters();
		}

	}

	void ResetMeters() {
		wind_generator.time_step = 0;
		BalanceMeter.transform.localPosition = new Vector3(0,
		                                                   BalanceMeter.transform.localPosition.y,
		                                                   BalanceMeter.transform.localPosition.z);
		WindMeter.transform.localPosition = new Vector3(0,
		                                                WindMeter.transform.localPosition.y,
		                                                WindMeter.transform.localPosition.z);
		CorrectedMeter.transform.localPosition = new Vector3(0,
		                                                     CorrectedMeter.transform.localPosition.y,
		                                                     CorrectedMeter.transform.localPosition.z);
		prev_roll_component = 0f;
		prev_roll_correction_component = 0f;
	}
	
	void UpdateBalanceBar() {
		balance_bar_length = player_health * balance_bar_scale_x;
		BalanceBar.transform.localScale = new Vector3(balance_bar_scale_x * player_health,
			BalanceBar.transform.localScale.y,
			BalanceBar.transform.localScale.z);
	}
	
	void ApplyWind() {
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
		float wind_meter_position_x = BalanceBar.transform.localPosition.x + wind_value * balance_bar_length_init / 2;
		Vector3 wind_meter_vector = new Vector3(wind_meter_position_x,
								WindMeter.transform.localPosition.y,
								WindMeter.transform.localPosition.z);
		WindMeter.transform.localPosition = wind_meter_vector;
	}
	
	void UpdateBalanceMeter() {
		float balance_meter_position_x = BalanceBar.transform.localPosition.x - roll_value * roll_sensitivity * balance_bar_length / 2;
		if (yaw_is_negative) {
			balance_meter_position_x = -balance_meter_position_x;	
		}
		if (Mathf.Abs(balance_meter_position_x - prev_roll_component) > 0.5) {
			balance_meter_position_x = prev_roll_component;
		} else {
			prev_roll_component = balance_meter_position_x;
		}
		Vector3 balance_meter_vector = new Vector3(balance_meter_position_x,
								BalanceMeter.transform.localPosition.y,
								BalanceMeter.transform.localPosition.z);
		BalanceMeter.transform.localPosition = balance_meter_vector;
	}
	
	void ApplyCorrection() {
		float roll_correction_component = BalanceBar.transform.localPosition.x - roll_value * roll_sensitivity * balance_bar_length_init / 2;
		if (yaw_is_negative) {
			roll_correction_component = -roll_correction_component;
		}
		if (Mathf.Abs(roll_correction_component - prev_roll_correction_component) > 0.2) {
			roll_correction_component = prev_roll_correction_component;
		} else {
			prev_roll_correction_component = roll_correction_component;
		}
		float corrected_meter_position_x = BalanceBar.transform.localPosition.x + (wind_value + roll_correction_component) * balance_bar_length_init / 2;
		Vector3 corrected_meter_vector = new Vector3(corrected_meter_position_x,
								CorrectedMeter.transform.localPosition.y,
								CorrectedMeter.transform.localPosition.z);
		CorrectedMeter.transform.localPosition = corrected_meter_vector;
		if (Mathf.Abs(corrected_meter_position_x) > balance_bar_length / 2) {
			has_fallen = true;
			ApplyFall();
		} else {
			Vector3 sway_position =  player_controller.transform.position - corrected_meter_vector / 10.0f;
		camera_controller.transform.position = sway_position;
		}
	}
	
	void ApplyFall() {
		Vector3 fall_vec = new Vector3(0,3,0);
		Vector3 updated_position = player_controller.transform.position - fall_vec;
		player_controller.transform.position = updated_position;
	}

	void OnTriggerEnter(Collider other) {
		print("collision");
		Destroy(other.gameObject);
	}
}
