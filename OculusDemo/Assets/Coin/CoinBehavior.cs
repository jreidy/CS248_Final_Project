using UnityEngine;
using System.Collections;

public class CoinBehavior : MonoBehaviour {

	public GameObject coin;
	private int time_step = 0;
	private int interval = 100;
	private int decay_time =  10;
	private int decay = 0;

	public AudioClip coin_sound;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		time_step++;
		if (time_step % interval == 0) {
			decay++;
			if (decay >= decay_time) {
				Destroy(coin);
			}
		}
		coin.transform.Rotate(0,1,0);
	}

	void OnTriggerEnter(Collider other)
	{
		if (coin_sound.isReadyToPlay) {
			AudioSource.PlayClipAtPoint(coin_sound, transform.position);
		}
	}
}
