using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FishData", menuName = "Data/FishData")]
public class FishData : ScriptableObject
{
    public float LeaderWeight;
    public float CohesionWeight;
    public float AlignmentWeight;
    public float SeparationWeight;
    public float MinSpeed;
    public float MaxSpeed;
    public float MaxDistanceFromLeader;
    public float MinDistanceFromLeader;
}
