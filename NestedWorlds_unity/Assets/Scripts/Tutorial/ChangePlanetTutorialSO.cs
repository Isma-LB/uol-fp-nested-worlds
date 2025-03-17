using IsmaLB.Planets;
using UnityEngine;

namespace IsmaLB.Tutorial
{
    [CreateAssetMenu(fileName = "ChangePlanetTutorialSO", menuName = "Scriptable Objects/Tutorial/ChangePlanetTutorialSO")]
    public class ChangePlanetTutorialSO : TutorialStepSO
    {
        [SerializeField] int targetPlanetIndex = 0;
        [SerializeField] PlanetsListSO planetsList;
        public override bool Evaluate()
        {
            return planetsList.CurrentIndex == targetPlanetIndex;
        }
    }
}
