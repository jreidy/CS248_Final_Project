using UnityEngine;
using System.Collections;

public class EagleGenerator : MonoBehaviour {
	
	public GameObject eagle_prefab;
	public GameObject eagle_collection;
	
	public bool should_generate = false; 
	public int number_of_eagles = 10;
	private bool is_generated = false;

	private int[,] boundaries = new int[,] {{-10,10},{5,10},{-10,10}};

	// Use this for initialization
	void Start () {
		eagle_collection = (GameObject)Instantiate(eagle_collection);
		eagle_collection.transform.parent = GameObject.Find("Platform").transform;
		eagle_collection.transform.localPosition = new Vector3(0.57f,0.33f,2.41f);
	}
	
	// Update is called once per frame
	void Update () {
		if (should_generate && !is_generated) {
			is_generated = true;
			for (int i = 0; i < number_of_eagles; i++) {
				GameObject newEagle = (GameObject)Instantiate (eagle_prefab);
				float x = Random.Range (boundaries [0, 0], boundaries [0, 1]);
				float y = Random.Range (boundaries [1, 0], boundaries [1, 1]);
				float z = Random.Range (boundaries [2, 0], boundaries [2, 1]);
				newEagle.transform.parent = eagle_collection.transform;
				newEagle.transform.localPosition = new Vector3 (x, y, z);
			}
		} else if (!should_generate) {
			DestroyEagles();
		}
	}
	
	void DestroyEagles() {
		foreach(Transform child in eagle_collection.transform) {
			Destroy(child.gameObject);
			is_generated = false;
		}
	}
	
}
