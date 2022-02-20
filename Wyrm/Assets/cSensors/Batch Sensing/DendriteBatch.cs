using System.Collections.Generic;
using UnityEngine;

public class DendriteBatch : MonoBehaviour
{
    public ConnectomeSimulator elegans;
    public StimuliType type;

    List<Transform> sensors;
    Dictionary<int, Stimuli> stimuliInRange;

    private void Awake()
    {
        sensors = new List<Transform>();
        stimuliInRange = new Dictionary<int, Stimuli>();

        // find all attached sensors
        foreach (Transform child in transform)
            //if (child.TryGetComponent(out DendriteSensor s))
                sensors.Add(child);
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Sensing " + other.name);

        if (other.TryGetComponent(out Stimuli stimu))
            stimuliInRange[stimu.GetInstanceID()] = stimu;
     
        //switch (LayerMask.LayerToName(other.gameObject.layer))
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("No longer sensing " + other.name);

        if (other.TryGetComponent(out Stimuli stimu))
            stimuliInRange.Remove(stimu.GetInstanceID());
    }

    private void FixedUpdate()
    {
        // stimulate all nerves in batch for performance reasons
        foreach (var stimu in stimuliInRange.Values)
        {
            foreach (var sensor in sensors)
            {
                float dist = (stimu.transform.position - sensor.transform.position).magnitude;
                elegans.DendriteStimuli(sensor.name, stimu.type, stimu.value, dist);
            }
        }
    }
}
