using UnityEngine;

[CreateAssetMenu(fileName = "New Sensing profile", menuName = "Sensing Profile/Presence Sensing", order = 1)]
public class PresenceSensing : SensingProfile
{
    [Tooltip("Activation frequency when sensing. 0: activation turned off, -1: constant activation")]
    public int OnFrequency = -1;

    [Tooltip("Activation frequency when not sensing. 0: activation turned off, -1: constant activation")]
    public int OffFrequency = 0;

    public override float OnActivationInterval(float valueDiff)
    {
        if (valueDiff < 0.5f)
            return OffFrequency;
        else
            return OnFrequency;
    }

}
