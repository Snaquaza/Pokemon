using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
	
	[SerializeField]
    private string moveName;

	// Battle Mechanics
	[SerializeField]
	private Type type;
	[SerializeField]
	private Categories category;
	[SerializeField]
	private int pp;
	[SerializeField]
	private int bp;
    [SerializeField]
	private int acc;
    [SerializeField]
    private int priority;

	// Add effects   

	// Add bunch of flags
	public delegate void Delegate();
	[SerializeField]
	public Delegate GetDelegate; 

	// Other   
    [SerializeField]
	private string desc;

	// Prefab methods
	// Have lists for some - loop through each function.
    [SerializeField]
	private BasePower GetBasePower;
    [SerializeField]
	private STAB GetSTAB;
    [SerializeField]
	private Effectiveness GetEffectiveness;
    [SerializeField]
	private Category GetCategory;
    [SerializeField]
	private Accuracy GetAccuracy;
    [SerializeField]
	private Priority GetPriority;
    [SerializeField]
	private Effect GetEffect;

	// Flags
    // Pass target and user always - Allows a lot
    // Delegate Function

	private void Start()
	{
		// Make this look for file instead;
		if (!GetBasePower)
			GetBasePower = (BasePower)Instantiate(Resources.Load("StandardBasePower"));
        if (!GetSTAB)
			GetBasePower = (BasePower)Instantiate(Resources.Load("StandardSTAB"));
        if (!GetEffectiveness)
			GetBasePower = (BasePower)Instantiate(Resources.Load("StandardEffectiveness"));
        if (!GetCategory)
			GetBasePower = (BasePower)Instantiate(Resources.Load("StandardCategory"));
        if (!GetAccuracy)
			GetBasePower = (BasePower)Instantiate(Resources.Load("StandardAccuracy"));
		if (!GetPriority)
			GetBasePower = (BasePower)Instantiate(Resources.Load("StandardPriority"));
        if (!GetEffect)
            GetBasePower = (BasePower)Instantiate(Resources.Load("StandardEffect"));
	}
    
	public void OnMove()
	{
		int moveBP = GetBasePower.GetBasePower();
	}
}
