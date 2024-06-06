using System;
using Automatons;
using UnityEngine;
using Random = UnityEngine.Random;

public class CellularAutomatonWrapper {

    public float[,,] AutomatonValues => _automaton.AutomatonValues;
    private readonly CellularAutomaton _automaton;
    
    public CellularAutomatonWrapper(CellularAutomaton automaton) {
        _automaton = automaton;
    }

    public void InitializeRandomState() {
        _automaton.InitializeRandomState();
    }

    public void NextState() {
        _automaton.Step();
    }

    /*
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
    }*/
}
