using UnityEngine;

namespace IsmaLB
{
    [CreateAssetMenu(fileName = "PlanetEventSO", menuName = "Scriptable Objects/PlanetEventSO")]
    public class PlanetEventSO : EventSO
    {
        public delegate void PlanetAction(Vector3 planetPos);
        public event PlanetAction OnPlanetEvent;
        public void Raise(Vector3 position)
        {
            base.RaiseOnEvent();
            OnPlanetEvent?.Invoke(position);
        }
    }
}