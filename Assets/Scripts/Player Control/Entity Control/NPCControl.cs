﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NPCControl : EntityControl {

    // Code "alert" - notice if running.
	// Code "bounds" while random walking
    // Code running / turning into here, not behavior
    public bool isTrainer;
	public int sight;
    private bool hasBattled;

	NPCBehavior behavior;

	private void Start()
	{
		behavior = GetComponent<NPCBehavior>();
		if (!isTrainer)
			sight = 0;
		else
			hasBattled = false;
	}

	// Update is called once per frame
	void FixedUpdate () {
		// Can glitch and appear near player
		// Walks before player animation is done (may be good)
        // Can walk through NPCs sometimes. Sometimes spots with no NPCs.
		if (isTrainer && !hasBattled)
		{
			if (!GetComponent<Movement>().DetectPlayer(sight))
			{
				behavior.Behavior();
			} else {
				hasBattled = true;
			}
		} else if (!isTrainer) {
			behavior.Behavior();
		}
		
	}
}

[CustomEditor(typeof(NPCControl))]
public class MyScriptEditor : Editor
{   
    // Trainer   
    private SerializedProperty isTrainer;
    private SerializedProperty sight;

    private void OnEnable()
    {      
        // Trainer
        isTrainer = serializedObject.FindProperty("isTrainer");
        sight = serializedObject.FindProperty("sight");      
    }

    public override void OnInspectorGUI()
    {
        serializedObject.UpdateIfRequiredOrScript();

        EditorGUILayout.PropertyField(isTrainer, new GUIContent("Trainer"));
        if (isTrainer.boolValue)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(sight, new GUIContent("Sight"));
            EditorGUI.indentLevel--;
        }
      
        serializedObject.ApplyModifiedProperties();
    }
}