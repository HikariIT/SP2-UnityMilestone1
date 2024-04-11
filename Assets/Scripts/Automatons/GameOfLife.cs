using System;
using System.Linq;
using UnityEngine;
using Automatons;
using Utils;
using Random = UnityEngine.Random;

namespace Automatons {
    public class GameOfLife : CellularAutomaton {

        public GameOfLife(Vector3Int automatonSize) : base(automatonSize) {}
        
        public override void InitializeRandomState() {
            _automatonValues = Common.PopulateArrayUsingMethod(_automatonSize, () => Random.Range(0, 2));
        }

        protected override float _localTransitionFunction(float[,,] automatonValues, Vector3Int cellCoords) {
            var neighborhood = Neighborhood.Moore2D(1, false).ToList();
            var isCellAlive = Mathf.RoundToInt(automatonValues[cellCoords.x, cellCoords.y, cellCoords.z]) == 1;
            var aliveNeighbors = Mathf.RoundToInt(Neighborhood.SumNeighborhood(neighborhood, cellCoords, automatonValues));

            var cellAliveAfterStep = (!isCellAlive && aliveNeighbors == 3) ||
                                     (isCellAlive && aliveNeighbors == 2 || aliveNeighbors == 3);
            
            if (!isCellAlive && aliveNeighbors == 3)
                return 1f;

            if (isCellAlive && (aliveNeighbors == 2 || aliveNeighbors == 3))
                return 1f;
            
            return 0f;
        }
    }
}