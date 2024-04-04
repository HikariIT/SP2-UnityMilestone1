using System;
using UnityEngine;

public class AutomatonMesh {
    private GameObject[,,] _objects;
    private AutomatonMeshMode _meshMode;
    private Color _automatonMeshColor;

    public AutomatonMesh() {
        _meshMode = AutomatonMeshMode.BlackAndWhite;
    }

    public void SetMesh(GameObject[,,] objects) {
        _objects = objects;
    }

    public void SetAutomatonState(bool[,,] automatonValues) {
        for (var x = 0; x < _objects.GetLength(0); x++) {
            for (var y = 0; y < _objects.GetLength(1); y++) {
                for (var z = 0; z < _objects.GetLength(2); z++) {
                    _modifyVoxel(_objects[x, y, z], automatonValues[x, y, z]);
                }
            }
        }
    }

    private void _modifyVoxel(GameObject targetObject, bool voxelState) {
        var objectRenderer = targetObject.GetComponent<MeshRenderer>();

        switch (_meshMode) {
            case AutomatonMeshMode.BlackAndWhite: {
                objectRenderer.materials[0].color = voxelState ? Color.black : Color.white;
                break;
            }
            case AutomatonMeshMode.WhiteAndBlack: {
                objectRenderer.materials[0].color = voxelState ? Color.white : Color.black;
                break;
            }
            case AutomatonMeshMode.ColorAndInactive: {
                objectRenderer.materials[0].color = _automatonMeshColor;
                targetObject.SetActive(voxelState);
                break;
            }
            default:
                throw new InvalidOperationException("Invalid automaton mesh mode");
        }
    }

    public void SetMode(AutomatonMeshMode mode) {
        _meshMode = mode;
    }
    
    public void SetColor(Color color) {
        _automatonMeshColor = color;
    }
}

public enum AutomatonMeshMode {
    BlackAndWhite,      // Active nodes are set to black, inactive nodes are set to white
    WhiteAndBlack,      // Active nodes are set to white, inactive nodes are set to black
    ColorAndInactive,   // Active nodes are set to a color, inactive nodes are invisible (requires supplying of Automaton Mesh Color)
}
