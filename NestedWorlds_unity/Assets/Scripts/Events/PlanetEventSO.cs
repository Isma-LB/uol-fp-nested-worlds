using UnityEngine;

namespace IsmaLB
{
    [CreateAssetMenu(fileName = "PlanetEventSO", menuName = "Scriptable Objects/PlanetEventSO")]
    public class PlanetEventSO : ScriptableObject
    {
        public delegate void PlanetAction(Vector3 planetPos);
        public event PlanetAction OnEvent;
        public void Raise(Vector3 position)
        {
            OnEvent?.Invoke(position);
        }
    }
}