﻿using UnityEngine;
using System.Collections;

public class CoinGenerator : MonoBehaviour {

	public GameObject coin_prefab;
	public GameObject coin_collection;

	public bool should_generate = false; 

	private int time_step = 0;
	private int coin_generate_interval = 100;

	private float[,] positions = new float[,] {{7.5f, 1.0f, -5.3f},{9.5f, 1.1f, -6.7f},{11.7f, 1.2f, -8.4f},{13.7f, 1.4f, -9.8f},{5.7f, 1.0f,-3.9f},{4.0f, 1.4f, -2.8f},{2.1f, 1.5f, -1.3f},{0.7f, 1.7f, -0.4f},{-0.6f, 1.8f, 0.5f}};


	// Use this for initialization
	void Start () {
		print(coin_collection);
		coin_collection = (GameObject)Instantiate(coin_collection);
		coin_collection.transform.parent = GameObject.Find("Platform").transform;
		coin_collection.transform.localPosition = new Vector3(-1.0f,0.42f,-7.1f);

	}
	
	// Update is called once per frame
	void Update () {
		time_step++;
		if (should_generate &&  time_step % coin_generate_interval == 0) {
			GameObject new_coin = (GameObject)Instantiate(coin_prefab);
			new_coin.transform.parent = coin_collection.transform;
			int ri = Random.Range(0,8);
			new_coin.transform.localPosition = new Vector3(positions[ri,0],positions[ri,1],positions[ri,2]);
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