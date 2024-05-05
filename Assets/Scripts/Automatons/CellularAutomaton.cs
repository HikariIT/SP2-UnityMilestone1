using UnityEngine;

namespace Automatons {
    public abstract class CellularAutomaton {
        protected Vector3Int _automatonSize;
        protected float[,,] _automatonValues;
        
        public float[,,] AutomatonValues => _automatonValues;

        protected CellularAutomaton(Vector3Int automatonSize) {
            _automatonSize = automatonSize;
        }

        public void Step() {
            _automatonValues = Common.PerformActionForEveryCell(_automatonSize, _automatonValues, _localTransitionFunction);
        }

        public abstract void InitializeRandomState();
        protected abstract float _localTransitionFunction(float[,,] automatonValues, Vector3Int cellCoords);
    }
}