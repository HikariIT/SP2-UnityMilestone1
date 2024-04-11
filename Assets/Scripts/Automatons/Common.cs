using System;
using UnityEngine;

namespace Automatons {
    public static class Common {
        public static float[,,] PopulateArrayUsingMethod(Vector3Int automatonSize, Func<float> generatingFunction) {
            var arrayValues = new float[automatonSize.x, automatonSize.y, automatonSize.z];
            for (var x = 0; x < automatonSize.x; x++) {
                for (var y = 0; y < automatonSize.y; y++) {
                    for (var z = 0; z < automatonSize.z; z++) {
                        arrayValues[x, y, z] = generatingFunction();
                    }
                }
            }

            return arrayValues;
        }

        public static float[,,] PerformActionForEveryCell(Vector3Int automatonSize, float[,,] automatonValues,
                                                          Func<float[,,], Vector3Int, float> localTransitionFunction) {
            var nextValues = new float[automatonSize.x, automatonSize.y, automatonSize.z];
            for (var x = 0; x < automatonSize.x; x++) {
                for (var y = 0; y < automatonSize.y; y++) {
                    for (var z = 0; z < automatonSize.z; z++) {
                        nextValues[x, y, z]
                            = localTransitionFunction(automatonValues, new Vector3Int(x, y, z));
                    }
                }
            }

            return nextValues;
        }
    }
}