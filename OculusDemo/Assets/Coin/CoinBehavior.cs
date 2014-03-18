using UnityEngine;
using System.Collections;

public class CoinBehavior : MonoBehaviour {

	public GameObject coin;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		coin.transform.Rotate(0,1,0);
	}
}
