using System.Linq;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Automatons {
    public class GameOfLife3D : CellularAutomaton {
        public GameOfLife3D(Vector3Int automatonSize) : base(automatonSize) {}
        
        public override void InitializeRandomState() {
            _automatonValues = Common.PopulateArrayUsingMethod(_automatonSize, () => Random.Range(0, 2));
        }

        protected override float _localTransitionFunction(float[,,] automatonValues, Vector3Int cellCoords) {
            var neighborhood = Neighborhood.Moore3D(1, false).ToList();
            var isCellAlive = Mathf.RoundToInt(automatonValues[cellCoords.x, cellCoords.y, cellCoords.z]) == 1;
            var aliveNeighbors = Mathf.RoundToInt(Neighborhood.SumNeighborhood(neighborhood, cellCoords, automatonValues));

            if (!isCellAlive && aliveNeighbors == 5)
                return 1f;

            if (isCellAlive && (aliveNeighbors == 4 || aliveNeighbors == 5 || aliveNeighbors == 6 || aliveNeighbors == 7))
                return 1f;
            
            return 0f;
        }
    }
}