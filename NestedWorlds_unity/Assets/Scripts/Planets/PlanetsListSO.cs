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

        public Planet CurrentPlanet { get; private set; }

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
        internal void LoadCurrentPlanet()
        {
            LoadPlanet(startPlanet);
        }

        void LoadPlanet(int planetIndex)
        {
            Debug.Log("Loading planet scene: " + planetScenes[planetIndex].Name);
            SceneManager.LoadSceneAsync(planetScenes[planetIndex].BuildIndex, LoadSceneMode.Additive);
        }

        internal void OnPlanetLoaded(Planet planet)
        {
            CurrentPlanet = planet;
        }
    }
}
