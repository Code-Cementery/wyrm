using System;
using System.Collections.Generic;
using UnityEngine;

public class Connectome
{
    // Constants:
    const int threshold = 30;
    static readonly HashSet<string> otherMuscles = new HashSet<string>(new string[] {
            "MVULVA", "MANAL"
    });

    static readonly HashSet<string> musclePrefix = new HashSet<string>(new string[] { 
        "MVL", "MDL", "MVR", "MDR" 
    });

    public const int minMuscleId = 1;
    public const int maxMuscleId = 24;


    int currState = 0;
    int nextState = 1;

    int _synCount = 0;

    // Connectome neuron charges
    // node -> [0, 0]
    Dictionary<string, int[]> m_NeuronState;
    Dictionary<string, int> m_MuscleState;
    Dictionary<string, float> m_DendriteFrequencies;

    // Neuron synapses & their firing weigths
    // node -> (node, weight)
    private Dictionary<string, List<(string, int)>> syn;

    public int Count => syn.Count;
    public int SynCount => _synCount;
    //public static IEnumerable<string> muscleSets => musclePrefix;

    public Connectome(Dictionary<string, List<(string, int)>> syn)
    {
        this.syn = syn;
        Reset();
    }

    public void Activate(string neuron)
    {
        if (syn.TryGetValue(neuron, out var nNeurons))
        {
            // accumulate charge (at dendrite or muscle) towards neurons that are connected:
            foreach ((string neuronTo, int weight) in nNeurons)
            {
# if UNITY_EDITOR
                if (!m_NeuronState.ContainsKey(neuronTo))
                    Debug.Log("!!!" + neuronTo);
# endif
                m_NeuronState[neuronTo][nextState] += weight;
            }
        }
        else
        {
            Debug.LogError($"[Connectome] Neuron not found: {neuron}");
        }
    }

    public void Fire(string neuron)
    {
        // The threshold has been exceeded and we fire the neurite
        if (IsMuscle(neuron))
            return;

        Activate(neuron);

        // discharge neuron
        m_NeuronState[neuron][nextState] = 0;
    }

    public void SetSensoryNeuronActivationFrequency(string neuron, float frequency)
    {
        m_DendriteFrequencies[neuron] = frequency;
    }

    public void RunSimulation()
    {
        // 1. activate dendrites
        //foreach()

        // 2. activate neurons
        foreach (var kvp in m_NeuronState)
        {
            string neuron = kvp.Key;
            int charge = kvp.Value[currState];

            if (abs(charge) > threshold)
                Fire(neuron);
        }

        // 3. motor control
        foreach (var kvp in m_NeuronState)
        {
            if (IsMuscle(kvp.Key))
            {
                int charge = kvp.Value[nextState];
                //if (charge > 0)
                //    Debug.Log(kvp.Key + " " + charge);

                m_MuscleState[kvp.Key] = charge;

                // discharge muscle
                kvp.Value[nextState] = 0;
            }

            // 4. step state
            kvp.Value[currState] = kvp.Value[nextState];
        }

        // 4. reserve unused state for next state (var swap)
        var _s = currState;
        currState = nextState;
        nextState = _s;
    }

    public IEnumerable<(MuscleDescriptor, int)> GetMuscleStates(bool showNextState = false)
    {
        foreach (var m in GetMuscleDescriptors())
            yield return (m, m_MuscleState[m.MuscleName]);
    }

    public static IEnumerable<MuscleDescriptor> GetMuscleDescriptors()
    {
        foreach (string prefix in musclePrefix)
            for (int i = minMuscleId; i <= maxMuscleId; i++)
            {
                var m = prefix + i.ToString("00");
                var muscle = new MuscleDescriptor() { MuscleName = m };

                //if (otherMuscles.Contains(muscle.MuscleName))
                //{
                //    muscle.MuscleId = 1;
                //    muscle.Quadrant = CEMuscleQuadrant.NONE;
                //}
                //else
                //{
                    muscle.MuscleId = i;
                    muscle.Quadrant = (CEMuscleQuadrant)Enum.Parse(typeof(CEMuscleQuadrant), prefix);
                //}

                yield return muscle;
            }

        foreach (var m in otherMuscles) {
            yield return new MuscleDescriptor() {
                MuscleName = m,
                MuscleId = 1,
                Quadrant = CEMuscleQuadrant.NONE
            };
        }
    }

    public static bool IsMuscle(string node)
    {
        return node.Length > 2 && musclePrefix.Contains(node.Substring(0, 3)) || otherMuscles.Contains(node);
    }

    public IEnumerable<string> GetNeurons()
    {
        return syn.Keys;
    }

    public int GetNeuronState(string neuron)
    {
        return m_NeuronState[neuron][currState];
    }

    public IEnumerable<(string, int)> GetNeuronStates(bool showNextState = false)
    {
        foreach(var kvp in m_NeuronState)
            if (!IsMuscle(kvp.Key))
                yield return (kvp.Key, kvp.Value[showNextState ? nextState : currState]);
    }

    private static int abs(int charge)
    {
        return charge < 0 ? -charge : charge;
    }

    /// <summary>
    /// Reset neuron charge state
    /// </summary>
    public void Reset()
    {
        // reset:
        var neuronState = new Dictionary<string, int[]>();
        var muscleState = new Dictionary<string, int>();
        _synCount = 0;
        currState = 0;
        nextState = 1;

        // fill up with empty values
        foreach (var kvp in syn)
        {
            // dict default set [0,0] as currState,nextState
            string neuron = kvp.Key;
            if (!neuronState.ContainsKey(neuron))
                neuronState[neuron] = new int[2] { 0, 0 };

            // count neurons
            foreach ((string neuron2, _) in kvp.Value)
            {
                //if (!neuronState.ContainsKey(neuron2))
                //    neuronState[neuron2] = new int[2] { 0, 0 };

                // count only neurons:
                if (!IsMuscle(neuron2))
                    _synCount++;
            }
        }

        // set up muscle state wrapper dict
        foreach (var m in GetMuscleDescriptors())
        {
            neuronState[m.MuscleName] = new int[2] { 0, 0 };
            muscleState[m.MuscleName] = 0;
        }

        // replace & GC
        this.m_NeuronState?.Clear();
        this.m_NeuronState = null;
        this.m_NeuronState = neuronState;

        this.m_MuscleState?.Clear();
        this.m_MuscleState = null;
        this.m_MuscleState = muscleState;

        this.m_DendriteFrequencies?.Clear();
        this.m_DendriteFrequencies = null;
        m_DendriteFrequencies = new Dictionary<string, float>();
    }
}
