using UnityEngine;

public class CellularAutomatonWrapper {

    public bool[,,] AutomatonValues => _automatonValues;
    private readonly bool[,,] _automatonValues;

    public CellularAutomatonWrapper(Vector3Int automatonShape) {
        _automatonValues = new bool[automatonShape.x, automatonShape.y, automatonShape.z];
    }

    public void InitializeRandomState() {
        for (var x = 0; x < _automatonValues.GetLength(0); x++) {
            for (var y = 0; y < _automatonValues.GetLength(1); y++) {
                for (var z = 0; z < _automatonValues.GetLength(2); z++) {
                    _automatonValues[x, y, z] = Random.value >= 0.5;
                }
            }
        }
    }

    public void NextState() {
        InitializeRandomState();
    }
}
