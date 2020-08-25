using System;
using UnityEngine;

/// <summary>
/// 
/// </summary>
[CreateAssetMenu(fileName = "TileAnimationSettings", menuName = "Animation Settings/Create New Tile Animation Settings", order = 0)]
public sealed class TileAnimationSettings : ScriptableObject
{
    public float tileFallMaxTime;


    public float tileFallSpeed;


    public AnimationCurve tileFallCurve;


}