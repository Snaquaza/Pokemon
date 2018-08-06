using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokedex : MonoBehaviour
{
	public Species[] dex = new Species[1]
	{
		new Species(
			"Bulbsaur", // Name
            1, 1, // Dex numbers
            45, 49, 49, 65, 65, 45 // Stats         
		)
	};
}