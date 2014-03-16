using UnityEngine;
using System.Collections;

public class SpawnScriptHost : MonoBehaviour
{
	public Transform player;
	
	void OnServerInitialized() {
		SpawnPlayer ();
	}
	

	void SpawnPlayer() {
		Network.Instantiate (player, transform.position, transform.rotation, 0);
	}
	
	void OnPlayerDisconnected(NetworkPlayer player) {
		Network.RemoveRPCs (player);
		Network.DestroyPlayerObjects (player);
	}
	

}

