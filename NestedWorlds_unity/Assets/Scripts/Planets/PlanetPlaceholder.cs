using UnityEngine;

namespace IsmaLB.Planets
{
    public class PlanetPlaceholder : PlayerRangeItem
    {
        void Start()
        {
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            renderer.enabled = false;
        }
    }
}
