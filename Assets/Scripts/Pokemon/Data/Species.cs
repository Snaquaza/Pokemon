using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Species : MonoBehaviour {

	[SerializeField]
	private string speciesName;

	// Battle mechanics
    [SerializeField]
	private int nationalDex, regionalDex;
    [SerializeField]
	private int hp, atk, def, spa, spd, spe;
    [SerializeField]
	private Type[] types;
    [SerializeField]
	private Moveset moveset;
    [SerializeField]
	private Ability[] abilities;

	// Out of battle mechanics
    [SerializeField]
	private EggGroups[] eggGroups;   
    [SerializeField]
	private int genderRatio; // int = male percentage
    [SerializeField]
	private int catchRate;
    [SerializeField]
	private int expYield;
    [SerializeField]
	private int friendship;
    [SerializeField]
	private Stats[] evYield;
    [SerializeField]
	private LevelCurve levelCurve;

	// Other
    [SerializeField]
	private string dexEntry;
    [SerializeField]
	private Sprite frontSprite, backSprite, shinySprite, shinyBackSprite;
    [SerializeField]
	private int height, weight;
    [SerializeField]
	private Colors color;
	[SerializeField]
	private List<Species> forms;
    
	// Use this for initialization
	void Start () {
		
	}

	public Species(string speciesName, int nationalDex, int regionalDex, int hp, int atk, int def, int spa, int spd, int spe)
	{
		this.speciesName = speciesName;
		this.nationalDex = nationalDex;
		this.regionalDex = regionalDex;
		this.hp = hp;
		this.atk = atk;
		this.def = def;
		this.spa = spa;
		this.spd = spd;
		this.spe = spe;
	}

    public int[] GetStats()
	{
		int[] result = new int[6];
		result[0] = hp; result[1] = atk; result[2] = def;
		result[3] = spa; result[4] = spd; result[5] = spe;
		return result;
	}
}
