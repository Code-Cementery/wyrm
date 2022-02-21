using System;
using UnityEngine;

[Flags]
public enum ValueChangeType
{
    Higher = 1,
    Lower = 2
}

[CreateAssetMenu(fileName = "New Sensing profile", menuName = "Sensing Profile/Adaptive Sensing", order = 1)]
public class AdaptiveSensing : SensingProfile
{
    [Tooltip("Defines firing frequency dependent on value difference")]
    public AnimationCurve valDifToFrequency;

    //[Tooltip("Minimum difference to habitual value ")]
    //public float minDiff = 0.05f;

    [Tooltip("In which direction difference to habitual value counts")]
    public ValueChangeType valueChange = ValueChangeType.Higher | ValueChangeType.Lower;


    public override float OnActivationInterval(float valueDiff)
    {
        return 1000f / valDifToFrequency.Evaluate(valueDiff);
    }

}
