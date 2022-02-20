using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum ChangeType { Higher, Lower, Either }
public enum ActivationType { OnOff, Frequency }


public enum SenseType { SimplePresence, Adaptive }


[Serializable]
public class Sense
{
    public ReceptorType receptorType;
    [Tooltip("What stimuli triggers this receptor")]
    public StimuliType stimuli;

    [Tooltip("Presence triggers if stimuli is nearby. Adaptive")]
    public SenseType howToSense;

    //// if set; filters stimuli based on name
    //public string filterName;
    //// filters by stimuli type

    //// filters only change in 1 direction or either
    //public ChangeType senseChangeDirection;

    //public ValueType senseChange;

    //// How much seconds does it take to adatp to new level of value
    //// adaptation means that the value won't stimulate anymore
    //// cooldown == 0 means immediate adaptation after firing
    //// cooldown == -1 means no adaptation happens
    //public float adaptationToValueCooldown = 0f;

    //public float fixedValue = 0f;
}


/// <summary>
/// Detects external stimuli
/// </summary>
public class NeuronSensing : MonoBehaviour
{
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
