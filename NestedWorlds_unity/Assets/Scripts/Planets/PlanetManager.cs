using System;
using System.Collections;
using UnityEngine;

namespace IsmaLB.Planets
{
    public class PlanetManager : MonoBehaviour
    {
        [SerializeField] GravityBody player;
        [SerializeField] PlanetsListSO planetsList;
        public static PlanetsListSO PlanetsList { get; private set; } = null;

        void Awake()
        {
            PlanetsList = planetsList;
        }
        void Start()
        {
            if (planetsList == null)
            {
                Planet currentPlanet = FindAnyObjectByType<Planet>();
                if (currentPlanet != null)
                {
                    player.attractor = currentPlanet.Attractor;
                }
                else
                {
                    Debug.LogWarning("Could not find current planet: please assign a PlanetListSO asset to automatically load planets or ensure a planet is loaded");
                }
            }
            else
            {
                StartCoroutine(LoadPlanets());
            }
        }

        private IEnumerator LoadPlanets()
        {
            planetsList.LoadCurrentPlanet();
            yield return new WaitUntil(IsCurrentPlanetLoaded);
            player.attractor = planetsList.CurrentPlanet.Attractor;
        }

        private bool IsCurrentPlanetLoaded() => planetsList.CurrentPlanet != null;

        // Update is called once per frame
        void Update()
        {

        }
    }
}
