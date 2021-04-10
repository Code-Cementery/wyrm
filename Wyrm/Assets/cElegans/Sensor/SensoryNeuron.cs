using System;
using UnityEngine;

public enum ChangeType { Higher, Lower, Either }
public enum ValueType { Fixed, Adaptive, Cultivation }
public enum ActivationType { OnOff,  }


[Serializable]
public class Sense
{
    // if set; filters stimuli based on name
    public string filterName;
    // filters by stimuli type
    public StimuliType stimuli;
    // filters only change in 1 direction or either
    public ChangeType senseChangeDirection;

    public ValueType senseChange;

    // How much seconds does it take to adatp to new level of value
    // adaptation means that the value won't stimulate anymore
    // cooldown == 0 means immediate adaptation after firing
    // cooldown == -1 means no adaptation happens
    public float adaptationToValueCooldown = 0f;

    public float fixedValue = 0f;
}


public class SensoryNeuron : MonoBehaviour
{
    public CElegans elegans;
    public string neuron;

    [Header("Activation:")]
    public bool invert;
    [Tooltip("True => neuron is either continously activating or not at all. False => sensing causes rapid activation, otherwise random, slow activation")]
    public bool continousFire;

    [Space]
    public Sense[] sensing;
}
