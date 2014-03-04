using UnityEngine;
using System.Collections;

public class GameTime : MonoBehaviour {
	
	public Transform[] sunMoonTransform;
	public float dayCycleInMinutes = 10;
	
	public Light moon_light;
	public Light sun_light;
	
	public Light moon_spot_light;
	public Light sun_spot_light;
	
	private const float SECOND = 1;
	private const float MINUTE = 60 * SECOND;
	private const float HOUR = 60 * MINUTE;
	private const float DAY = 24 * HOUR;
	
	private const float DEGREES_PER_SECOND = 360 / DAY;
	
	private float _degreeRotation;
	
	private float _timeOfDay;

	// Use this for initialization
	void Start () {
		_timeOfDay = 0;
		_degreeRotation = DEGREES_PER_SECOND * DAY / (dayCycleInMinutes *MINUTE);
	}
	
	// Update is called once per frame
	void Update () {
		sunMoonTransform[0].Rotate(new Vector3(_degreeRotation, 0, 0) * Time.deltaTime);
		print(moon_light.transform.position.y);
		if (moon_light.transform.position.y < 0) {
			moon_light.enabled = false;
			moon_spot_light.enabled = false;
		} else {
			moon_light.enabled = true;
			moon_spot_light.enabled = true;
		}
		if (sun_light.transform.position.y < 0) {
			sun_light.enabled = false;
			sun_spot_light.enabled = false;
		} else {
			sun_light.enabled = true;
			sun_spot_light.enabled = true;
		}
		_timeOfDay += Time.deltaTime;
	}
}
