using UnityEngine;
using System.Collections;

public class CoinGenerator : MonoBehaviour {

	public GameObject coin_prefab;
	public GameObject coin_collection;

	public bool should_generate = false; 

	private int time_step = 0;
	private int coin_generate_interval = 100;


	// Use this for initialization
	void Start () {
		print(coin_collection);
		coin_collection = (GameObject)Instantiate(coin_collection);
		coin_collection.transform.parent = GameObject.Find("Platform").transform;
		coin_collection.transform.localPosition = new Vector3(0,0,0);
		print("coins");
	}
	
	// Update is called once per frame
	void Update () {
		time_step++;
		if (should_generate &&  time_step % coin_generate_interval == 0) {
			GameObject new_coin = (GameObject)Instantiate(coin_prefab);
			new_coin.transform.parent = coin_collection.transform;
			new_coin.transform.localPosition = new Vector3(0,2,0);
			print("should generate");
		} else if (!should_generate) {
			DestroyCoins();
		}
	}

	void DestroyCoins() {
		foreach(Transform child in coin_collection.transform) {
			Destroy(child.gameObject);
		}
	}

}
