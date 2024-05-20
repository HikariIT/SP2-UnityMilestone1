using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CubeGrid : MonoBehaviour
{
	public CubeGridProfiler Profiler;
	public SimSettings simSettings;

	[Header("Prefabs")]
	public Transform CubePrefab;
	public Transform OutsidePrefab;

	[Header("Shaders")]
	public ComputeShader CubeShader;

	private int Seed;

	private Transform _outside;

	private int CubesPerAxis;
    private float TimePerStep;
	private int numRules = 128;
	private int FreeMargin;

	private Transform[] _cubes;
	private int[] _cubesAlive;
	public static int[] Rules = new int[128]; //numRules

	private ComputeBuffer _cubesAliveBuffer;
    private ComputeBuffer _previousCubesAliveBuffer;
	private ComputeBuffer _rulesBuffer;

	[Header("Benchmark")]
	public int numberOfExecutions = 10;

	private void Awake() {
		CubesPerAxis = simSettings.CubesPerAxis;
		TimePerStep = simSettings.TimePerStep;
		Seed = simSettings.Seed;
		FreeMargin = simSettings.FreeMargin;
		
		_cubesAliveBuffer = new ComputeBuffer(CubesPerAxis * CubesPerAxis * CubesPerAxis, sizeof(int));
        _previousCubesAliveBuffer = new ComputeBuffer(CubesPerAxis * CubesPerAxis * CubesPerAxis, sizeof(int));
		_rulesBuffer = new ComputeBuffer(numRules, sizeof(int));

		Profiler = new CubeGridProfiler();

		CreateRules();
	}

	private void OnDestroy() {
		_cubesAliveBuffer.Release();
        _previousCubesAliveBuffer.Release();
	}


	private void Start() {
		CreateGrid();
	}

	void CreateGrid() {
		_cubes = new Transform[CubesPerAxis * CubesPerAxis * CubesPerAxis];
		_cubesAlive = new int[CubesPerAxis * CubesPerAxis * CubesPerAxis];

		for (int x = 0, i = 0; x < CubesPerAxis; x++) {
			for (int y = 0; y < CubesPerAxis; y++) {
				for (int z = 0; z < CubesPerAxis; z++, i++) {
					_cubes[i] = Instantiate(CubePrefab, transform);
					_cubes[i].transform.position = new Vector3(x, y, z);
				}
			}
		}

		_outside = Instantiate(OutsidePrefab, transform);
		_outside.transform.position = new Vector3(CubesPerAxis/2.0f-0.5f, CubesPerAxis/2.0f-0.5f, CubesPerAxis/2.0f-0.5f);
		_outside.transform.localScale = new Vector3(CubesPerAxis+1, CubesPerAxis+1, CubesPerAxis+1);
        UpdateAliveGPU(0);
		UpdateCubeAliveInUnity();
		StartCoroutine(UpdateCubeGrid(2));
	}

	void CreateRules() {
		var random = new System.Random(simSettings.Seed);
		for (int i=0; i<numRules; i++){
			if (random.Next(0, 10) < 4){
				Rules[i] = 1;
			} else {
				Rules[i] = 0;
			}
		}
		_rulesBuffer.SetData(Rules);
		PrintRules();
	}

	void PrintRules(){
		string res = "";
		for (int i=0; i<numRules; i++){
			res += "Rule: " + System.Convert.ToString(i, 2) + " Value: " + System.Convert.ToString(Rules[i]) + "\n";
		}
		UnityEngine.Debug.Log(res);
	}

    void UpdateCubeAliveInUnity()
    {
        for (int i = 0; i < _cubes.Length; i++)
        {
            if (_cubesAlive[i] == 0)
            {
                // Handle disappeared cube (e.g., deactivate or remove)
                _cubes[i].gameObject.SetActive(false);
            }
            else
            {
                // Update visible cube position
                _cubes[i].gameObject.SetActive(true);
            }
        }
    }

	IEnumerator UpdateCubeGrid(int kernel) {
		while (true) {
			UpdateAliveGPU(kernel);
			UpdateCubeAliveInUnity();
			yield return new WaitForSeconds(TimePerStep);
		}
	}

	void UpdateAliveGPU(int kernel) {
		CubeShader.SetBuffer(kernel, "_Alive", _cubesAliveBuffer);
        CubeShader.SetBuffer(kernel, "_PreviousAlive", _previousCubesAliveBuffer);

		_rulesBuffer.SetData(Rules);
		CubeShader.SetBuffer(kernel, "_Rules", _rulesBuffer);

		CubeShader.SetInt("_CubesPerAxis", CubesPerAxis);
		CubeShader.SetFloat("_Time", Time.deltaTime);
		CubeShader.SetFloat("_StartBeginIndex", (FreeMargin));
		CubeShader.SetFloat("_StartEndIndex", (CubesPerAxis - FreeMargin - 1));

		int workgroups = Mathf.CeilToInt(CubesPerAxis / 8.0f);

		CubeShader.Dispatch(kernel, workgroups, workgroups, workgroups);

		_cubesAliveBuffer.GetData(_cubesAlive);

	}

	public void RunBenchmark() {
		StopAllCoroutines();

		Stopwatch stopWatch = new Stopwatch();

		// GPU
		for (int i = 0; i < numberOfExecutions; i++) {
			stopWatch.Reset();
			stopWatch.Start();
			UpdateAliveGPU(0);
			stopWatch.Stop();
			Profiler.AddGPUCall(stopWatch.ElapsedMilliseconds);
		}
		StartCoroutine(UpdateCubeGrid(1));
	}
}
