using UnityEngine;
using System.Collections;

public class Head_Up_Display : VRGUI 
{
	public GUISkin skin;
	
	public TextMesh _round_text_mesh;
	public GameObject _balance_cylinder;
	public GameObject _balance_needle;
	
	// Interface Components
	// Rounds
	private int _total_rounds = 5;
	private int _round_number = 5;
	// Balance
	private int _user_degree = 0;
	private int _balance_degree = 0;
	private int _balance_range = 30;
	// Stamina
	private int _user_stamina = 100;
	
	private int clickCount = 0;
	
	public void RoundNumberIs(int round_number){
		_round_number = round_number;
	}
	
	public void TotalRoundsIs(int total_rounds){
		_total_rounds = total_rounds;
	}
	
	public void UserDegreeIs(int user_degree){
		_user_degree = user_degree;
	}
	
	public void BalanceDegreeIs(int balance_degree){
		_balance_degree = balance_degree;
	}
	
	public void BalanceRangeIs(int balance_range) {
		_balance_range = balance_range;
	}
	
	public override void OnVRGUI()
	{
		_round_text_mesh.text = _round_number.ToString();
	}
}