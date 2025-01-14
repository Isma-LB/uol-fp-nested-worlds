using UnityEngine;
using Eflatun.SceneReference;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;
using System.Collections;


namespace IsmaLB.Planets
{
    [CreateAssetMenu(fileName = "PlanetsListSO", menuName = "Scriptable Objects/PlanetsListSO")]
    public class PlanetsListSO : ScriptableObject
    {
        [SerializeField] int startPlanet = 0;
        [SerializeField] List<SceneReference> planetScenes;

        static Dictionary<int, Planet> loadedPlanets = new();
        static event Action CheckLoadedPlanets;
        public Planet PreviousPlanet { get; private set; }
        public Planet CurrentPlanet { get; private set; }
        public Planet NextPlanet { get; private set; }
        int currentPlanetIndex = 0;
        public int CurrentIndex { get => currentPlanetIndex; }

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
        void OnEnable() => CheckLoadedPlanets += UpdateLoadedPlanets;
        void OnDisable() => CheckLoadedPlanets -= UpdateLoadedPlanets;

        private void UpdateLoadedPlanets()
        {
            PreviousPlanet = GetLoadedPlanet(currentPlanetIndex - 1);
            CurrentPlanet = GetLoadedPlanet(currentPlanetIndex);
            NextPlanet = GetLoadedPlanet(currentPlanetIndex + 1);
            if (CurrentPlanet != null)
            {
                CurrentPlanet.SetupAsCurrent();
            }
            if (CurrentPlanet != null && PreviousPlanet != null)
            {
                PreviousPlanet.SetupRelativeTo(CurrentPlanet, Planet.Transition.backwards);
            }
            if (CurrentPlanet != null && NextPlanet != null)
            {
                NextPlanet.SetupRelativeTo(CurrentPlanet, Planet.Transition.forward);
            }
        }

        public static void OnPlanetLoaded(Planet planet)
        {
            loadedPlanets.Add(planet.SceneIndex, planet);
            CheckLoadedPlanets?.Invoke();
        }
        public static void OnPlanetUnloaded(Planet planet)
        {
            loadedPlanets.Remove(planet.SceneIndex);
            CheckLoadedPlanets?.Invoke();
        }
        internal IEnumerator LoadCurrentPlanet()
        {
            ChangeCurrentPlanet(startPlanet);
            yield return new WaitUntil(() => CurrentPlanet != null);
        }
        public void ChangeCurrentPlanet(int planetIndex)
        {
            if (IsValidPlanetIndex(planetIndex) == false)
            {
                throw new ArgumentOutOfRangeException("Planet index is out of range");
            }
            currentPlanetIndex = planetIndex;
            UnloadPlanetScene(planetIndex - 2);
            LoadPlanetScene(planetIndex - 1);
            LoadPlanetScene(planetIndex);
            LoadPlanetScene(planetIndex + 1);
            UnloadPlanetScene(planetIndex + 2);
        }
        void LoadPlanetScene(int planetIndex)
        {
            if (IsValidPlanetIndex(planetIndex) && !IsPlanetLoaded(planetIndex))
            {
                SceneManager.LoadSceneAsync(planetScenes[planetIndex].BuildIndex, LoadSceneMode.Additive);
            }
        }
        void UnloadPlanetScene(int planetIndex)
        {
            if (IsValidPlanetIndex(planetIndex) && IsPlanetLoaded(planetIndex))
            {
                SceneManager.UnloadSceneAsync(planetScenes[planetIndex].BuildIndex);
            }
        }
        Planet GetLoadedPlanet(int planetIndex)
        {
            if (IsPlanetLoaded(planetIndex))
            {
                return loadedPlanets[planetScenes[planetIndex].BuildIndex];
            }
            return null;
        }
        public bool IsValidPlanetIndex(int planetIndex)
        {
            return 0 <= planetIndex && planetIndex < planetScenes.Count;
        }
        bool IsPlanetLoaded(int planetIndex)
        {
            return IsValidPlanetIndex(planetIndex) && loadedPlanets.ContainsKey(planetScenes[planetIndex].BuildIndex);
        }
    }
}
