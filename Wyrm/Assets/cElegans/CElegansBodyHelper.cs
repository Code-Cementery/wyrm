using c302;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CElegansBodyHelper : MonoBehaviour
{
    /**
     * Helper Mono object to demonstrate muscle & neuron activation of C.elegans
     * 
     */
    public float vMuscleOffset = 7f;
    public float dMuscleOffset = 14f;
    public float muscleVerticalOffset = 10f;
    public Vector3 muscleScale = Vector3.one;

    [Header("Muscle Charge")]
    public float maxMuscleCharge = 10f;
    //public Color posChargeCol = Color.red;
    //public Color negChargeCol = Color.blue;

    Dictionary<string, Renderer> muscles;

    public Connectome conn;
    MaterialPropertyBlock propBlock;

    const string _COL = "_BaseColor";

    void Start()
    {
        conn = GetComponent<CElegans>().conn;
        muscles = new Dictionary<string, Renderer>();
        propBlock = new MaterialPropertyBlock();

        SetupMuscles();
    }

    /// GameMessage
    private void FixedUpdate()
    {
        foreach ((MuscleDescriptor muscle, int charge) in conn.GetMuscleStates())
        {
            if (muscles.TryGetValue(muscle.MuscleName, out var _render))
            {
                // Map charge to color
                var val = Mathf.Clamp(charge, 0, maxMuscleCharge);
                var p = val / maxMuscleCharge;
                Color col = charge >= 0 ? new Color(p, 0, 0) : new Color(0, p, 0);

                // Color muscle based on value
                _render.GetPropertyBlock(propBlock, 0);
                propBlock.SetColor(_COL, col);
                _render.SetPropertyBlock(propBlock, 0);
            }
            else
            {
                Debug.LogError("[CE Helper] muscle not found: " + muscle.MuscleName);
            }
        }
    }


    // @TODO: tochable cylinder for senses?

    void SetupMuscles()
    {
        Dictionary<string, MuscleDescriptor> descriptors = new Dictionary<string, MuscleDescriptor>();

        foreach (var muscle in Connectome.GetMuscleDescriptors())
            descriptors[muscle.MuscleName] = muscle;

        foreach(Transform child in transform.Find("Muscles"))
        {
            var m = descriptors[child.name];

            if (m.Quadrant != CEMuscleQuadrant.NONE)
            {
                // positional muscles
                Vector3 pos = Vector3.zero;

                switch (m.Quadrant)
                {
                    case CEMuscleQuadrant.MDL: pos.z += dMuscleOffset; break;
                    case CEMuscleQuadrant.MVL: pos.z += vMuscleOffset; break;
                    case CEMuscleQuadrant.MVR: pos.z -= vMuscleOffset; break;
                    case CEMuscleQuadrant.MDR: pos.z -= dMuscleOffset; break;
                }

                pos.x = (m.MuscleId - 1) * muscleVerticalOffset;

                child.localPosition = pos;
                child.localScale = muscleScale;
            }

            muscles[child.name] = child.GetComponent<Renderer>();
        }
    }

    [ContextMenu("Spawn Muscles")]
    private void __SpawnMuscles()
    {
        var mWrapper = transform.Find("Muscles");

        // spawn muscles 7-23 
        foreach (MuscleDescriptor m in Connectome.GetMuscleDescriptors())
        {
            if (!mWrapper.Find(m.MuscleName))
            {
                var obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                obj.transform.SetParent(mWrapper, false);
                obj.name = m.MuscleName;
            }
        }

        if (conn != null)
            SetupMuscles();
    }
}
