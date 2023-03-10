using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "player", menuName = "playerHealth")]
public class PlayerScriptableObj : ScriptableObject
{
    public float maxHealth;
    public float currentHealth;

    public bool playerdead = false; 
}
