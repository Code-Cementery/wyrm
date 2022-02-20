//using System;
//using UnityEngine;

//public class SensoryNeuron : MonoBehaviour
//{
//    public ConnectomeSimulator elegans;
//    public string neuron;

//    [Header("Activation:")]
//    public bool invert;
//    [Tooltip("True => neuron is either continously activating or not at all. False => sensing causes rapid activation, otherwise random, slow activation")]
//    public bool continousFire;

//    [Space]
//    public Sense[] sensing;


//    void OnSense(Stimuli stimu)
//    {
//        float dist = (stimu.transform.position - transform.position).magnitude;

//        /*
//         sense:

//        - filters sense?
//        - condition is triggered?
//        - activation

//        update:
//        - activation continous
//         */

//        //bool isTriggered = IsConditionTriggered(stimu.value, sense);

//        //if (sense.senseChange == ValueType.Fixed)
//        //    if (stimu.value > sense.fixedValue)
//    }
//}

