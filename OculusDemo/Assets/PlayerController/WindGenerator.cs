using UnityEngine;
using System.Collections;

public class WindGenerator : MonoBehaviour {
	
	// Wind value from -1 to 1
	private float AMP = 1f;
	private float FREQ = 0.001f;
	
	private int time_step = 0;
	
	public float wind_value = 0f;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		time_step++;
		UpdateWind();
	}
			
	void UpdateWind() {
		wind_value = AMP * Mathf.Sin(2* Mathf.PI * FREQ * time_step);
	}
}
