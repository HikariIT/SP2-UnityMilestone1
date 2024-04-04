using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomatonMeshGenerator: MonoBehaviour {
    
    [SerializeField] public GameObject voxelPrefab;
    [SerializeField] public Vector3Int meshSize;
    [SerializeField] public float stepsPerSecond;

    private AutomatonMesh _automatonMesh;
    private CellularAutomatonWrapper _automatonWrapper;

    
    private void Start()
    {
        GenerateMesh();
    }

   private void GenerateMesh() {
        _automatonWrapper = new CellularAutomatonWrapper(meshSize);
        _automatonWrapper.InitializeRandomState();
        
        _automatonMesh = new AutomatonMesh();
        _automatonMesh.SetMode(AutomatonMeshMode.ColorAndInactive);

        var objects = new GameObject[meshSize.x, meshSize.y, meshSize.z];
        
        // Define start and end colors for the gradient
        Color startColor = Color.blue;
        Color endColor = Color.red;

        for (var x = 0; x < meshSize.x; x++) {
            for (var y = 0; y < meshSize.y; y++) {
                for (var z = 0; z < meshSize.z; z++) {
                    objects[x, y, z] = Instantiate(voxelPrefab, new Vector3(x, y, z), Quaternion.identity, transform);
                    objects[x, y, z].name = $"Voxel ({x}, {y}, {z})";
                    
                    // Calculate gradient color based on the position
                    float gradientFactor = (float)y / (meshSize.y - 1); // Example: Gradient based on Y position
                    Color voxelColor = Color.Lerp(startColor, endColor, gradientFactor);

                    // Apply the color to the voxel's material
                    Renderer voxelRenderer = objects[x, y, z].GetComponent<Renderer>();
                    if (voxelRenderer != null) {
                        voxelRenderer.material.color = voxelColor;
                    }
                }
            }
        }
        
        _automatonMesh.SetMesh(objects);
        
        // Start coroutine of going into the next state with specific interval
        InvokeRepeating(nameof(NextState), 0, 1.0f / stepsPerSecond);
}
    private void Update() {
        
    }

    private void NextState() {
        _automatonWrapper.NextState();
        _automatonMesh.SetAutomatonState(_automatonWrapper.AutomatonValues);
        var objects = new GameObject[meshSize.x, meshSize.y, meshSize.z];
        
        // Define start and end colors for the gradient
        Color startColor = Color.blue;
        Color endColor = Color.red;

        for (var x = 0; x < meshSize.x; x++) {
            for (var y = 0; y < meshSize.y; y++) {
                for (var z = 0; z < meshSize.z; z++) {
                    objects[x, y, z] = Instantiate(voxelPrefab, new Vector3(x, y, z), Quaternion.identity, transform);
                    objects[x, y, z].name = $"Voxel ({x}, {y}, {z})";
                    
                    // Calculate gradient color based on the position
                    float gradientFactor = (float)y / (meshSize.y - 1); // Example: Gradient based on Y position
                    Color voxelColor = Color.Lerp(startColor, endColor, gradientFactor);

                    // Apply the color to the voxel's material
                    Renderer voxelRenderer = objects[x, y, z].GetComponent<Renderer>();
                    if (voxelRenderer != null) {
                        voxelRenderer.material.color = voxelColor;
                    }
                }
            }
        }
        
        _automatonMesh.SetMesh(objects);
    }
}
