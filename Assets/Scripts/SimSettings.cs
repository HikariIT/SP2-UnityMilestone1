using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Simulation settings", menuName = "SP2FirstShader/Simulation settings")]
public class SimSettings: ScriptableObject
{
	[Header("Seed")]
	public int Seed = 42;


	[Header("Cubes")]
	public int CubesPerAxis = 10;
	public int StartingCubesPerAxis = 5;

    [Header("Time per step")]
    public float TimePerStep = 0.5f;

	[Header("Camera speed")]
	public float moveSpeed = 30f;

	[Header("Benchmark")]
	public int numberOfExecutions = 10;
}
