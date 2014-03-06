using UnityEngine;
using System.Collections;

public class Atmosphere : MonoBehaviour {
	
	int cloud_time_step = 100;
	int cloud_time = 0;
	
	public GameObject cloud;
	private GameObject[, ,] clouds;
	private bool[, ,] cloud_updates;
	
	private int clouds_x = 20;
	private int clouds_z = 20;
	private int clouds_y = 3;
	
	private int cloud_width = 10;
	private float cloud_padding = 0.3f;
	
	
	private int neighbor_empty = 0;
	private int neighbor_isolation = 2;
	private int neighbor_overpopulation = 5;
	private int neighbor_breed = 4;

	// Use this for initialization
	void Start () {
		
		clouds = new GameObject[clouds_x, clouds_y, clouds_z];
		cloud_updates = new bool[clouds_x, clouds_y, clouds_z];
		
		Vector3 start_position = new Vector3(transform.position.x - (cloud_width*clouds_x)/2, 
											transform.position.y, 
											transform.position.z - (cloud_width*clouds_z)/2);
		
		for (int x = 0; x < clouds_x; x++) {
			for (int y = 0; y < clouds_y; y++) {
				for (int z = 0; z < clouds_z; z++) {
					//Create new cloud
					GameObject new_cloud = Instantiate(cloud, 
						start_position + new Vector3(x*cloud_width,y*cloud_width,z*cloud_width), 
						Quaternion.identity) as GameObject;
					// determine if space is occupied
					int breed_cell = Random.Range(0, 4);
					if (breed_cell == 1) {
						cloud_updates[x,y,z] = true;
					} else {
						cloud_updates[x,y,z] = false;	
					}
					new_cloud.SetActive(cloud_updates[x,y,z]);
					new_cloud.transform.localScale = new Vector3(cloud_width-cloud_padding,
																cloud_width-cloud_padding,
																cloud_width-cloud_padding);
					clouds[x,y,z] = new_cloud;
				}
			}
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
		for (int x = 0; x < clouds_x; x++) {
			for (int y = 0; y < clouds_y; y++) {
				for (int z = 0; z < clouds_z; z++) {
					int number_neighbors = getNumberNeighbors(x, y, z);
					print(number_neighbors);
					if (number_neighbors == neighbor_empty) {
						cloud_updates[x,y,z] = false;
				    } else if (number_neighbors <= neighbor_isolation) {
						cloud_updates[x,y,z] = false;
					} else if (number_neighbors == neighbor_breed  && cloud_updates[x,y,z] == false) {
						cloud_updates[x,y,z] = true;
					} else if (number_neighbors >= neighbor_overpopulation) {
						cloud_updates[x,y,z] = false;	
					}
				}
			}
		}
		//Change state for all cells
		for (int i = 0; i < clouds_x; i++) {
			for (int j = 0; j < clouds_y; j++) {
				for (int k = 0; k < clouds_z; k++) {
					clouds[i,j,k].SetActive(cloud_updates[i,j,k]);
				}
			}
		}
		
	}
	
	int getNumberNeighbors(int x, int y, int z) {
		int number_neighbors = 0;
		
		int min_x = (x > 0) ? x-1 : x;
		int min_y = (y > 0) ? y-1 : y;
		int min_z = (z > 0) ? z-1 : z;
		
		int max_x = (x < clouds_x - 1) ? x+1 : x;
		int max_y = (y < clouds_y - 1) ? y+1 : y;
		int max_z = (z < clouds_z - 1) ? z+1 : z;
		
		for (int i = min_x; i <= max_x; i++) {
			for (int j = min_y; j <= max_y; j++) {
				for (int k = min_z; k <= max_z; k++) {
					if (!(i == x && j == y && k == z)) {
						if (clouds[i,j,k].activeSelf) {
							number_neighbors++;
						}
					}
				}
			}	
		}
		
		return number_neighbors;
	}
}
