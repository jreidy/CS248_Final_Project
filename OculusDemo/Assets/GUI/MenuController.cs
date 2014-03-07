using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {
	
	private Head_Up_Display menu;

	// Use this for initialization
	void Start () {
		menu = GetComponent<Head_Up_Display>();
		Screen.showCursor = false;
	}
	
	// Update is called once per frame
	void Update () {
		// toggle the menu on/off
		if (Input.GetKeyDown(KeyCode.Space))
		{
			menu.enabled = !menu.enabled;
		}
	}
	
}
