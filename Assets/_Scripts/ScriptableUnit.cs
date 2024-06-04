using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Unit", menuName ="Scriptable Unit")] 

public class ScriptableUnit : ScriptableObject
{
    public Faction Faction;
    public BaseUnit UnitPrefab;

}

public enum Faction
{
    Rock = 0,
    Paper = 1,
    Scissor = 2
}
