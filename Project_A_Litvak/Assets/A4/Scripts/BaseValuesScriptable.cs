using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Base/Values")]
public class BaseValuesScriptable : ScriptableObject
{
    [Header("Base Cost")]
    public int baseCost;
    [Header("Multiplier")]
    public float multiplier;
    [Header("Max amount of upgrades")]
    public int maxUpgrades;

}
