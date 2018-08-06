using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveset : MonoBehaviour {
	private List<Move>[] levelUp;
	private List<Move> eggMoves;
	private bool[] tmMoves;

	private void Start()
	{
		levelUp = new List<Move>[100];
		eggMoves = new List<Move>();
		tmMoves = new bool[100];
	}
}
