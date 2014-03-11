using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {
	
	public AudioSource background_music_source;
	public AudioSource strike_source;
	public bool loop_background = false;

	// Use this for initialization
	void Start () {
		//background_music_clip.
		if (loop_background == true) {
			background_music_source.loop = true;	
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
}
