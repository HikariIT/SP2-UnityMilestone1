using UnityEngine;

public class CellularAutomatonWrapper {

    public bool[,,] AutomatonValues => _automatonValues;
    private readonly bool[,,] _automatonValues;

    private int _lookAhead = 3;

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

    public void GameOfLife() {
        for (var x = 0; x < _automatonValues.GetLength(0); x++) {
            for (var y = 0; y < _automatonValues.GetLength(1); y++) {
                for (var z = 0; z < _automatonValues.GetLength(2); z++) {
                    int neighbors = GetAliveNeighborhood2D(_automatonValues, x, y, z);
                    //Debug.Log("Nei" + neighbors);
                    if ((neighbors == 2 || neighbors == 3) && _automatonValues[x, y, z]){
                        Debug.Log("True");
                        _automatonValues[x, y, z] = true;
                    }else if (neighbors == 3 && !_automatonValues[x, y, z]) {
                        _automatonValues[x, y, z] = true;
                    }else {
                        _automatonValues[x, y, z] = false;
                    }

                }
            }
        }
    }

    public void GameOfLife3D() {
        for (var x = 0; x < _automatonValues.GetLength(0); x++) {
            for (var y = 0; y < _automatonValues.GetLength(1); y++) {
                for (var z = 0; z < _automatonValues.GetLength(2); z++) {
                    int neighbors = GetAliveNeighborhood3D(_automatonValues, x, y, z);
                    //Debug.Log("Nei" + neighbors);
                    if (neighbors >= 8 && neighbors <= 12 && _automatonValues[x, y, z]){
                        Debug.Log("True");
                        _automatonValues[x, y, z] = true;
                    }else if (neighbors >= 8 && neighbors <= 10 && !_automatonValues[x, y, z]) {
                        _automatonValues[x, y, z] = true;
                    }else {
                        _automatonValues[x, y, z] = false;
                    }

                }
            }
        }
    }

    private int GetAliveNeighborhood3D(bool[,,] grid, int x, int y, int z)
    {
        int neighbors = 0;

        for (int i = -_lookAhead; i <= _lookAhead; i++)
        {
            for (int j = -_lookAhead; j <= _lookAhead; j++)
            {
                for (int k = -_lookAhead; k <= _lookAhead; k++)
                {
                    int newX = x + i;
                    int newY = y + j;
                    int newZ = z + k;

                    // Check bounds
                    if (newX >= 0 && newX < grid.GetLength(0) &&
                        newY >= 0 && newY < grid.GetLength(1) &&
                        newZ >= 0 && newZ < grid.GetLength(2))
                    {
                        if (grid[newX, newY, newZ] == true){
                            neighbors++;
                        }
                    }
                }
            }
        }

        return neighbors;
    }

    private int GetAliveNeighborhood2D(bool[,,] grid, int x, int y, int z)
    {
        int neighbors = 0;

        for (int i = -_lookAhead; i <= _lookAhead; i++)
        {
            for (int j = -_lookAhead; j <= _lookAhead; j++)
            {
                int newX = x + i;
                int newY = y + j;

                // Check bounds
                if (newX >= 0 && newX < grid.GetLength(0) &&
                    newY >= 0 && newY < grid.GetLength(1))
                {
                    if (grid[newX, newY, z] == true){
                        neighbors++;
                    }
                }
            }
        }

        return neighbors;
    }


    public void NextState() {
        GameOfLife();
    }
}
