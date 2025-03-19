using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using IsmaLB.Planets;

namespace IsmaLB
{
    public class PlanetsMinimap : MonoBehaviour
    {
        [SerializeField] List<Image> planets;
        [SerializeField] int currentPlanetIndex;
        [SerializeField] Color currentColor;
        [SerializeField] Color normalColor;

        [SerializeField] PlanetEventSO onPlanetChange;
        [SerializeField] PlanetsListSO planetsList;

        void OnEnable()
        {
            onPlanetChange.OnEvent += UpdateGraphics;
        }
        void OnDisable()
        {
            onPlanetChange.OnEvent -= UpdateGraphics;
        }
        void Start()
        {
            UpdateGraphics();
        }

        void UpdateGraphics()
        {
            for (int i = 0; i < planets.Count; i++)
            {
                planets[i].color = i == planetsList.CurrentIndex ? currentColor : normalColor;
            }
        }

    }
}
