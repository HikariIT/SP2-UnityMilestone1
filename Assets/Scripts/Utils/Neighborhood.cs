using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils {
    public static class Neighborhood {
        
        public static IEnumerable<Vector3Int> VonNeumann2D(int size, bool includeFocalCell) {
            var cells = new List<Vector3Int>();

            for (var x = -size; x <= size; x++) {
                for (var z = -size; z <= size; z++) {
                    if (Math.Abs(x) + Math.Abs(z) <= size)
                        cells.Add(new Vector3Int(x, 0, z));
                }
            }

            if (!includeFocalCell)
                cells.Remove(new Vector3Int(0, 0, 0));

            return cells;
        }

        public static IEnumerable<Vector3Int> VonNeumann3D(int size, bool includeFocalCell) {
            var cells = new List<Vector3Int>();

            for (var x = -size; x <= size; x++) {
                for (var y = -size; y <= size; y++) {
                    for (var z = -size; z <= size; z++) {
                        if (Math.Abs(x) + Math.Abs(y) + Math.Abs(z) <= size)
                            cells.Add(new Vector3Int(x, y, z));
                    }
                }
            }

            if (!includeFocalCell)
                cells.Remove(new Vector3Int(0, 0, 0));

            return cells;
        }

        public static IEnumerable<Vector3Int> Moore2D(int size, bool includeFocalCell) {
            var cells = new List<Vector3Int>();

            for (var x = -size; x <= size; x++) {
                for (var z = -size; z <= size; z++) {
                    cells.Add(new Vector3Int(x, 0, z));
                }
            }

            if (!includeFocalCell)
                cells.Remove(new Vector3Int(0, 0, 0));

            return cells;
        }

        public static IEnumerable<Vector3Int> Moore3D(int size, bool includeFocalCell) {
            var cells = new List<Vector3Int>();

            for (var x = -size; x <= size; x++) {
                for (var y = -size; y <= size; y++) {
                    for (var z = -size; z <= size; z++) {
                        cells.Add(new Vector3Int(x, y, z));
                    }
                }
            }

            if (!includeFocalCell)
                cells.Remove(new Vector3Int(0, 0, 0));

            return cells;
        }

        public static float SumNeighborhood(IEnumerable<Vector3Int> neighborhood, Vector3Int cellCoords,
                                              float[,,] automatonValues) {
            neighborhood = neighborhood.ToList();
            var neighbors = neighborhood.Where(offset => {
                var newPos = cellCoords + offset;

                return newPos.x >= 0 && newPos.x < automatonValues.GetLength(0) &&
                       newPos.y >= 0 && newPos.y < automatonValues.GetLength(1) &&
                       newPos.z >= 0 && newPos.z < automatonValues.GetLength(2);
            }).Select(pos => pos + cellCoords);
            
            
            return neighborhood
                .Where(offset => {
                    var newPos = cellCoords + offset;
                    
                    return newPos.x >= 0 && newPos.x < automatonValues.GetLength(0) &&
                           newPos.y >= 0 && newPos.y < automatonValues.GetLength(1) &&
                           newPos.z >= 0 && newPos.z < automatonValues.GetLength(2);
                })
                .Sum(offset => automatonValues[
                    (cellCoords + offset).x,
                    (cellCoords + offset).y,
                    (cellCoords + offset).z
                ]);
        }
    }
}