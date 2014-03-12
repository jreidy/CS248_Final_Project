using UnityEngine;
using System.Collections;

public class Atmosphere_Sphere_Trans : MonoBehaviour {

	int cloud_time_step = 2;
	int cloud_time = 0;
	private float degree_of_change = 0.02f;
	
	public GameObject cloud;
	public GameObject cloud_container;
	private GameObject[] clouds;
	
	private float cloud_spacer = 6.0f;
	
	private int number_clouds = 30;
	private int cloud_base_offset = 300;
	
	private float min_size = 100.0f;
	private float max_size = 200.0f;
	
	private float min_height = 30.0f;
	private float max_height = 100.0f;
	
	private float rand_rotation = 360.0f;

	// Use this for initialization
	void Start () {
		
		clouds = new GameObject[number_clouds];
		
		Vector3 start_position = new Vector3(transform.position.x, 
											transform.position.y, 
											transform.position.z);
		
		for (int i = 0; i < number_clouds; i++) {
			
			//Instantiate GameObject at offset
			GameObject new_cloud = Instantiate(cloud,
				start_position + new Vector3(0,0,0), 
				Quaternion.identity) as GameObject;
			new_cloud.SetActive(true);
			new_cloud.transform.parent = cloud_container.transform;
			float rand_width = Random.Range(min_size, max_size);
			float rand_length = Random.Range(min_size, max_size);
			float rand_height = Random.Range (min_height, max_height);
			new_cloud.transform.localScale = new Vector3(rand_width, rand_height, rand_length);
			
			float rand_rotation_x = Random.Range(-rand_rotation, rand_rotation);
			float rand_rotation_z = Random.Range(-rand_rotation, rand_rotation);
			Vector3 rotation =  new Vector3(rand_rotation_x, 0.0f,rand_rotation_z);
			
			new_cloud.transform.Rotate(rotation);
			new_cloud.transform.Translate(new Vector3(0, cloud_base_offset + i * cloud_spacer, 0));
			clouds[i] = new_cloud;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (cloud_time % cloud_time_step == 0) {
			updateClouds();
		}
		cloud_time++;
	}
	
	void updateClouds() {
		cloud_container.transform.Rotate(new Vector3(degree_of_change,
																	0,
													degree_of_change));
	}

}
