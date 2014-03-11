using UnityEngine;
using System.Collections;
using System;

public class NetworkBase : MonoBehaviour {
	
	public string connectToIp = "127.0.0.1";
	public int connectPort = 25000;
	public bool useNAT = false;
	public string ipAddress = "";
	public string port = "";
	const string noName = "<NAME>";
	public string playerName = noName;
	
	void OnGUI() {
		if (Network.peerType == NetworkPeerType.Disconnected) {
			int offsetX = Screen.width/4-40,
			offsetY = Screen.height/2-65;
			if (GUI.Button(new Rect(offsetX,offsetY,80,20), "Connect")) {
				if (playerName != noName) {
					Network.Connect (connectToIp, connectPort);
					PlayerPrefs.SetString ("playername", playerName);
				}
			}
			
			if (GUI.Button(new Rect(offsetX,offsetY+25,80,20), "Start Server")) {
				if (playerName != noName) {
					Network.InitializeServer (32, connectPort, useNAT);
					
					foreach (GameObject go in FindObjectsOfType (typeof(GameObject))) {
						go.SendMessage ("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);
					}
					PlayerPrefs.SetString ("playername", playerName);
				}
			}
			
			playerName = GUI.TextField(new Rect(offsetX,offsetY+50,80,20), playerName);
			connectToIp = GUI.TextField(new Rect(offsetX,offsetY+75,80,20), connectToIp);
			connectPort = Convert.ToInt32(GUI.TextField (new Rect(offsetX,offsetY+100,80,20), connectPort.ToString()));
			
		} else {
			if (Network.peerType == NetworkPeerType.Connecting) {
				GUILayout.Label("Connection Status: Connecting");
			} else if (Network.peerType == NetworkPeerType.Client) {
				GUILayout.Label ("Connection Status: Client");
				GUILayout.Label ("Ping to Host: " + Network.GetAveragePing (Network.connections[0]));
			} else if (Network.peerType == NetworkPeerType.Server) {
				GUILayout.Label ("Connection Status: Host");
				GUILayout.Label ("Connections: " + Network.connections.Length);
				if (Network.connections.Length >= 1) {
					GUILayout.Label ("Ping to Opponent: " + Network.GetAveragePing (Network.connections[0]));
				}
			}
			
			if (GUILayout.Button ("Disconnect")) {
				Network.Disconnect (200);
			}
			
			ipAddress = Network.player.ipAddress;
			port = Network.player.port.ToString ();
			GUILayout.Label ("IP Address: " + ipAddress + ":" + port);
		}
	}
	
	void OnConnectedToServer() {
		foreach (GameObject go in FindObjectsOfType (typeof(GameObject))) {
			go.SendMessage ("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);
		}
	}
}
