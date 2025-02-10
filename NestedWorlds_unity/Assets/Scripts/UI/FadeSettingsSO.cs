using UnityEngine;

[CreateAssetMenu(fileName = "FadeSettings", menuName = "Scriptable Objects/UI/FadeSettings")]
public class FadeSettingsSO : ScriptableObject
{
    [SerializeField] float speed = 2;
    [SerializeField] AnimationCurve ease;

    public float Speed => speed;
    public float EvaluateEase(float amount) => ease.Evaluate(amount);

}