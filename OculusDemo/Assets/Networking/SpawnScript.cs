using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour
{
	public Transform player;
	
	void OnServerInitialized() {
		SpawnPlayer ();
	}
	
	void OnConnectedToServer() {
		SpawnPlayer ();
	}
	
	void SpawnPlayer() {
		Network.Instantiate (player, transform.position, transform.rotation, 0);
	}
	
	void OnPlayerDisconnected(NetworkPlayer player) {
		Network.RemoveRPCs (player);
		Network.DestroyPlayerObjects (player);
	}
	
	void OnDisconnectedFromServer(NetworkDisconnection info) {
		Network.RemoveRPCs (Network.player);
		Network.DestroyPlayerObjects (Network.player);
		Application.LoadLevel (Application.loadedLevel);
	}
}

