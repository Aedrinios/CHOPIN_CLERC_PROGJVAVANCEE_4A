using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataObject", menuName = "Player", order = 0)]
public class PlayerDataObject : ScriptableObject
{
    public string horizontalAxis, verticalAxis;

    public KeyCode jumpInput, hitBallInput;

    public Material playerMaterial;
}
