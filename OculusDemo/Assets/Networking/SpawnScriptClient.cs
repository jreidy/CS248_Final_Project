using UnityEngine;
using System.Collections;

public class SpawnScriptClient : MonoBehaviour
{
	public Transform player;
	
	void OnConnectedToServer() {
		SpawnPlayer ();
	}
	
	void SpawnPlayer() {
		Network.Instantiate (player, transform.position, transform.rotation, 0);
	}
	
	void OnDisconnectedFromServer(NetworkDisconnection info) {
		Network.RemoveRPCs (Network.player);
		Network.DestroyPlayerObjects (Network.player);
		Application.LoadLevel (Application.loadedLevel);
	}
}

