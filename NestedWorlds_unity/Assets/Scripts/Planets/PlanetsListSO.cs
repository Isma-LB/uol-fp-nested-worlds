using UnityEngine;
using Eflatun.SceneReference;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


namespace IsmaLB.Planets
{
    [CreateAssetMenu(fileName = "PlanetsListSO", menuName = "Scriptable Objects/PlanetsListSO")]
    public class PlanetsListSO : ScriptableObject
    {
        [SerializeField] int startPlanet = 0;
        [SerializeField] List<SceneReference> planetScenes;

        static Dictionary<int, Planet> loadedPlanets = new();
        public Planet CurrentPlanet { get => GetLoadedPlanet(currentPlanetIndex); }
        int currentPlanetIndex = 0;
        void OnValidate()
        {
            if (planetScenes.Count > 0)
            {
                startPlanet = Mathf.Clamp(startPlanet, 0, planetScenes.Count - 1);
            }
            else
            {
                startPlanet = 0;
            }
        }

        public static void OnPlanetLoaded(Planet planet)
        {
            loadedPlanets[planet.SceneIndex] = planet;
        }
        public static void OnPlanetUnloaded(Planet planet)
        {
            loadedPlanets.Remove(planet.SceneIndex);
        }
        internal void LoadCurrentPlanet()
        {
            currentPlanetIndex = startPlanet;
            LoadPlanet(startPlanet);
        }

        void LoadPlanet(int planetIndex)
        {
            Debug.Log("Loading planet scene: " + planetScenes[planetIndex].Name);
            SceneManager.LoadSceneAsync(planetScenes[planetIndex].BuildIndex, LoadSceneMode.Additive);
        }
        Planet GetLoadedPlanet(int planetIndex)
        {
            if (IsValidPlanetIndex(planetIndex) == false) return null;

            if (loadedPlanets.ContainsKey(planetScenes[planetIndex].BuildIndex))
            {
                return loadedPlanets[planetScenes[planetIndex].BuildIndex];
            }
            return null;
        }
        bool IsValidPlanetIndex(int planetIndex)
        {
            return 0 <= planetIndex && planetIndex < planetScenes.Count;
        }
    }
}
