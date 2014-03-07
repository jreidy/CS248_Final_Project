using UnityEngine;
using System.Collections;

public class GameTime : MonoBehaviour {
	
	public enum TimeOfDay {
		Idle,
		SunRise,
		SunSet
	}
	
	public Transform[] sunMoonTransform;
	public float dayCycleInMinutes = 0.1f;
	
	public Light moon_light;
	public Light sun_light;
	
	public Light moon_spot_light;
	public Light sun_spot_light;
	
	public float sunRise;
	public float sunSet;
	public float skyboxBlendModifier;
	
	private const float SECOND = 1;
	private const float MINUTE = 60 * SECOND;
	private const float HOUR = 60 * MINUTE;
	private const float DAY = 24 * HOUR;
	
	private const float DEGREES_PER_SECOND = 360 / DAY;
	
	private float _degreeRotation;
	
	private float _timeOfDay;
	
	private float _dayCycleInSeconds;
	
	private TimeOfDay _tod;

	// Use this for initialization
	void Start () {
		_tod = TimeOfDay.Idle;
		
		_timeOfDay = 0;
		_degreeRotation = DEGREES_PER_SECOND * DAY / (dayCycleInMinutes *MINUTE);
		_dayCycleInSeconds = dayCycleInMinutes * MINUTE;
		Color c = new Color(0.5f, 0.5f, 0.5f, 0.0f);
		RenderSettings.skybox.SetColor("_Tint", c);
		
		
		sunRise *= _dayCycleInSeconds;
		sunSet *= _dayCycleInSeconds;
	}
	
	// Update is called once per frame
	void Update () {
		sunMoonTransform[0].Rotate(new Vector3(_degreeRotation, 0, 0) * Time.deltaTime);
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
		
		if (_timeOfDay > _dayCycleInSeconds) _timeOfDay -= _dayCycleInSeconds;
		
		if (_timeOfDay > sunRise && 
			_timeOfDay < sunSet && 
			RenderSettings.skybox.GetColor("_Tint")[3] < 1) {
			_tod = GameTime.TimeOfDay.SunRise;
			updateSkyBox();
		} else if (_timeOfDay > sunSet && RenderSettings.skybox.GetColor("_Tint")[3] > 0) {
			_tod = GameTime.TimeOfDay.SunSet;
			updateSkyBox();
		} else {
			_tod = GameTime.TimeOfDay.Idle;	
		}
			
		
	}
	
	void updateSkyBox() {
		float blend_value = 0;
		
		switch(_tod) {
		case TimeOfDay.SunRise:
			blend_value = (_timeOfDay - sunRise) / _dayCycleInSeconds * skyboxBlendModifier;
			break;
		case TimeOfDay.SunSet:
			blend_value = (_timeOfDay - sunSet) / _dayCycleInSeconds * skyboxBlendModifier;
			blend_value = 1 - blend_value;
			break;
		}
		
		//blend_value = _timeOfDay / _dayCycleInSeconds * 2;
			
		Color c = new Color(0.5f, 0.5f, 0.5f, blend_value);
		RenderSettings.skybox.SetColor("_Tint", c);
	}
}
