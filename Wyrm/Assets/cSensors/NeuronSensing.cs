using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum ChangeType { Higher, Lower, Either }
public enum ActivationType { OnOff, Frequency }


[Serializable]
public class Sense
{
    [Tooltip("What stimuli triggers this receptor")]
    public Stimuli stimuli;

    [Tooltip("Describes how to sense stimuli and firing frequency")]
    public SensingProfile profile;
}


/// <summary>
/// Detects external stimuli
/// </summary>
public class NeuronSensing : MonoBehaviour
{
    public Sense[] sensing;

    [Tooltip("What stimuli triggers this dendrite")]
    public StimuliType[] type;

    DendriteTrigger trigger;

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
