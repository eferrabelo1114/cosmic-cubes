using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/StartingFaceConfig", order = 1)]
public class StartingFaceConfig : ScriptableObject
{
    public int[] verticalDiceReel = new int[3];
    public int[] horizontalDiceReel = new int[4];
}
