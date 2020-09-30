using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player", order = 0)]
public class PlayerData : ScriptableObject
{
    public string horizontalAxis, verticalAxis;

    public KeyCode jumpInput, hitBallInput;

    public Material playerMaterial;
}
