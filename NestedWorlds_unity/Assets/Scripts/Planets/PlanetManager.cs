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

        [Header("Transition")]
        [SerializeField, Range(1, 3)] float playerPosMultiplayer = 1.25f;
        [SerializeField] float playerJumpOnForwardsTransition = 8;
        [SerializeField] AnimationCurve speed = AnimationCurve.EaseInOut(0f, 0.25f, 1.1f, 1f);

        enum Direction { forwards, backwards }
        bool transitionInProcess = false;
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
            MakeTransition(Direction.forwards);
        }
        private void OnChangePreviousPlanet()
        {
            Debug.Log("Visit previous planet");
            MakeTransition(Direction.backwards);
        }

        private void MakeTransition(Direction dir)
        {
            if (transitionInProcess) return;

            int targetPlanetIndex = planetsList.CurrentIndex;
            targetPlanetIndex += dir == Direction.forwards ? 1 : -1;

            if (planetsList.IsValidPlanetIndex(targetPlanetIndex))
            {
                transitionInProcess = true;
                Planet targetPlanet = dir == Direction.forwards ? planetsList.NextPlanet : planetsList.PreviousPlanet;
                player.attractor = targetPlanet.Attractor;
                // trigger the transition
                StartCoroutine(Transition(planetsList.CurrentPlanet, targetPlanet));
                /// if the transition is backwards...
                if (dir == Direction.backwards)
                {
                    player.Rigidbody.AddForce(player.transform.up * playerJumpOnForwardsTransition, ForceMode.Impulse);
                }

                planetsList.ChangeCurrentPlanet(targetPlanetIndex);
            }
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
            yield return planetsList.LoadCurrentPlanet();
            player.attractor = planetsList.CurrentPlanet.Attractor;
        }


        IEnumerator Transition(Planet fromPlanet, Planet toPlanet)
        {
            float targetScale = fromPlanet.transform.localScale.x / toPlanet.transform.localScale.x;
            // start planet transition coroutines
            Coroutine from = StartCoroutine(fromPlanet.TransitionTransform(targetScale, speed));
            Coroutine to = StartCoroutine(toPlanet.TransitionTransform(targetScale, speed));
            // execute player transition (will take the same as planet transitions)
            yield return TransitionPlayer(fromPlanet, targetScale);
            // ensure all coroutines have ended.
            yield return from;
            yield return to;
            transitionInProcess = false;
        }

        IEnumerator TransitionPlayer(Planet fromPlanet, float deltaScale)
        {

            Vector3 playerPos = player.transform.position - fromPlanet.transform.position;
            Vector3 playerTargetPos = playerPos * deltaScale;

            float value = 0;
            yield return new WaitForFixedUpdate();
            while (value < 1)
            {
                value += Time.fixedDeltaTime * speed.Evaluate(value);

                player.Rigidbody.MovePosition(fromPlanet.transform.localPosition + Vector3.Lerp(playerPos, playerTargetPos * playerPosMultiplayer, value));

                yield return new WaitForFixedUpdate();
            }
        }

    }
}
