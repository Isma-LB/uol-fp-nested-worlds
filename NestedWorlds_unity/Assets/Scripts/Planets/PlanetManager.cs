using System;
using System.Collections;
using UnityEngine;
using IsmaLB.Input;

namespace IsmaLB.Planets
{
    public class PlanetManager : MonoBehaviour
    {
        [SerializeField] GravityBody player;
        [SerializeField] PlanetsListSO planetsList;
        [SerializeField] InputReader input;

        void OnEnable()
        {
            input.nextEvent += OnChangeNextPlanet;
            input.previousEvent += OnChangePreviousPlanet;
        }
        void OnDisable()
        {
            input.nextEvent -= OnChangeNextPlanet;
            input.previousEvent -= OnChangePreviousPlanet;
        }

        // input events
        private void OnChangeNextPlanet()
        {
            Debug.Log("Visit next planet");
            MakeTransition(planetsList.CurrentIndex + 1);
        }

        private void MakeTransition(int targetPlantIndex)
        {
            if (planetsList.IsValidPlanetIndex(targetPlantIndex))
            {
                planetsList.ChangeCurrentPlanet(targetPlantIndex);
            }
        }

        private void OnChangePreviousPlanet()
        {
            Debug.Log("Visit previous planet");
            MakeTransition(planetsList.CurrentIndex - 1);
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
