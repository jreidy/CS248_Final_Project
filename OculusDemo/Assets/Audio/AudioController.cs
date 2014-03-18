using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {
	
	public AudioSource background_music_source;
	public GameObject wind;
	public bool loop_background = false;
	public float wind_position_offset;

	// Use this for initialization
	void Start () {
		//background_music_clip.
//		if (loop_background == true) {
//			background_music_source.Play();
//			background_music_source.loop = true;	
//		}
		wind_position_offset = 7.0f;
	}
	
	// Update is called once per frame
	void Update () {
		updateWindAudioPosition();
	}
	
	void updateWindAudioPosition() {
		
	}
	
}
