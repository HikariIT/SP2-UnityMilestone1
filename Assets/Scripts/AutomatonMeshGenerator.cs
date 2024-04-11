using Automatons;
using UnityEngine;

public class AutomatonMeshGenerator: MonoBehaviour {

    [SerializeField] public Camera meshCamera;
    [SerializeField] public GameObject voxelPrefab;
    [SerializeField] public Vector3Int meshSize;
    [SerializeField] public float stepsPerSecond;
    [SerializeField] public AutomatonMeshMode meshMode;

    private AutomatonMesh _automatonMesh;
    private CellularAutomatonWrapper _automatonWrapper;

    
    private void Start() {
        meshCamera.transform.position
            = new Vector3((float)meshSize.x / 2, meshSize.x * Mathf.Sqrt(3) / 2, (float)meshSize.z / 2);
        GenerateMesh();
    }

   private void GenerateMesh() {
       var automaton = new GameOfLife(meshSize);
       _automatonWrapper = new CellularAutomatonWrapper(automaton);
       _automatonWrapper.InitializeRandomState();
        
       _automatonMesh = new AutomatonMesh();
       _automatonMesh.SetMode(meshMode);

       var objects = new GameObject[meshSize.x, meshSize.y, meshSize.z];
        
        // Define start and end colors for the gradient
       var startColor = Color.blue;
       var endColor = Color.red;

       for (var x = 0; x < meshSize.x; x++) { 
           for (var y = 0; y < meshSize.y; y++) { 
               for (var z = 0; z < meshSize.z; z++) { 
                   objects[x, y, z] = Instantiate(voxelPrefab, new Vector3(x, y, z), Quaternion.identity, transform); 
                   objects[x, y, z].name = $"Voxel ({x}, {y}, {z})";
                   
                   // Calculate gradient color based on the position
                   var gradientFactor = (float)y / (meshSize.y - 1); // Example: Gradient based on Y position
                   var voxelColor = Color.Lerp(startColor, endColor, gradientFactor);

                   // Apply the color to the voxel's material
                   var voxelRenderer = objects[x, y, z].GetComponent<Renderer>();
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
    }
}
