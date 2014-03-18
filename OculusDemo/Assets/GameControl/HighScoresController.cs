using UnityEngine;
using System.Collections;

public class HighScoresController : MonoBehaviour {

	private GameObject gold;
	private GameObject silver;
	private GameObject bronze;

	private int[] top_scores= new int[] {200, 120, 80};

	// Use this for initialization
	void Start () {
		gold = GameObject.Find("gold");
		silver = GameObject.Find("silver");
		bronze = GameObject.Find("bronze");

	}
	
	// Update is called once per frame
	void Update () {
		TextMesh goldMesh = (TextMesh)gold.GetComponent("TextMesh");
		TextMesh silverMesh = (TextMesh)silver.GetComponent("TextMesh");
		TextMesh bronzeMesh = (TextMesh)bronze.GetComponent("TextMesh");
		goldMesh.text = top_scores[0].ToString();
		silverMesh.text = top_scores[1].ToString();
		bronzeMesh.text = top_scores[2].ToString();
	}

	public void SubmitScore(int new_score) {
		if (new_score >= top_scores[0]) {
			top_scores[2] = top_scores[1];
			top_scores[1] = top_scores[0];
			top_scores[0] = new_score;
		} else if (new_score >= top_scores[1]) {
			top_scores[2] = top_scores[1];
			top_scores[1] = new_score;
		} else if (new_score >= top_scores[2]) {
			top_scores[2] = new_score;
		}
	}
}
