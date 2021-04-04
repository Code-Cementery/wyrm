using System;
using System.Collections.Generic;
using UnityEngine;

namespace c302
{
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

        public const int minMuscleId = 7;
        public const int maxMuscleId = 23;


        int currState = 0;
        int nextState = 1;

        int _synCount = 0;

        // Connectome neuron charges
        // node -> [0, 0]
        Dictionary<string, int[]> neuronState;
        //Dictionary<string, Muscle> muscleState;

        // Neuron synapses & their firing weigths
        // node -> (node, weight)
        private Dictionary<string, List<(string, int)>> syn;

        public int Count => neuronState.Count;
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
                // accumulate charge (at dendrite) towards neurons that are connected:
                foreach ((string neuronTo, int weight) in nNeurons)
                    neuronState[neuronTo][nextState] += weight;
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

            //discharge
            neuronState[neuron][nextState] = 0;
        }

        public void FireSense(string sense)
        {
            // not implemented!
        }

        public void RunSimulation()
        {
            foreach (var kvp in neuronState)
            {
                string neuron = kvp.Key;
                int charge = kvp.Value[currState];

                if (abs(charge) > threshold)
                    Fire(neuron);
            }
        }

        public void StepSimulation()
        {
            foreach (var st in neuronState.Values)
                st[currState] = st[nextState];
        }

        public Muscle GetMuscle(string m)
        {
            if (neuronState.TryGetValue(m, out int[] state))
            {
                return new Muscle()
                {
                    MuscleName = m,
                    MuscleId = int.Parse(m.Substring(3,2)),
                    Quadrant = (CEMuscleQuadrant)Enum.Parse(typeof(CEMuscleQuadrant), m.Substring(0,3)),

                    CurrentCharge = state[currState],
                    NextCharge = state[nextState],
                };
            }
            else
                return null;
        }


        public IEnumerable<Muscle> GetMuscleCharges()
        {
            foreach (string prefix in musclePrefix)
                for (int i = minMuscleId; i < maxMuscleId; i++)
                {
                    var m = prefix + i.ToString("00");

                    yield return new Muscle() {
                        MuscleName = m,
                        MuscleId = i,
                        Quadrant = (CEMuscleQuadrant) Enum.Parse(typeof(CEMuscleQuadrant), prefix),
                        // @todo: later: HEAD | NECK | BODY 

                        CurrentCharge = neuronState[m][currState],
                        NextCharge = neuronState[m][nextState],
                    };
                }
        }

        public static IEnumerable<Muscle> GetMuscles()
        {
            foreach (string prefix in musclePrefix)
                for (int i = minMuscleId; i < maxMuscleId; i++)
                {
                    var m = prefix + i.ToString("00");

                    yield return new Muscle()
                    {
                        MuscleName = m,
                        Quadrant = (CEMuscleQuadrant)Enum.Parse(typeof(CEMuscleQuadrant), prefix),
                    };
                }
        }

        public int GetNeuronCharge(string neuron)
        {
            return neuronState[neuron][currState];
        }

        public static bool IsMuscle(string node)
        {
            return node.Length > 2 && musclePrefix.Contains(node.Substring(0, 3)) || otherMuscles.Contains(node);
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
            var emptyState = new Dictionary<string, int[]>();
            //var muscleState = new Dictionary<string, Muscle>();
            _synCount = 0;
            currState = 0;
            nextState = 1;

            // fill up with empty values
            foreach (var kvp in syn)
            {
                // dict default set [0,0] as currState,nextState
                string neuron = kvp.Key;
                if (!emptyState.ContainsKey(neuron))
                    emptyState[neuron] = new int[2] { 0, 0 };

                foreach ((string neuron2, _) in kvp.Value)
                {
                    if (!emptyState.ContainsKey(neuron2))
                        emptyState[neuron2] = new int[2] { 0, 0 };

                    // count only neurons:
                    if (!IsMuscle(neuron2))
                        _synCount++;
                }
            }

            // fill muscles up

            // replace & GC
            neuronState?.Clear();
            neuronState = null;
            neuronState = emptyState;
        }
    }
}
