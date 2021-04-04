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
    public float maxMuscleCharge = 10f;
    public Vector3 muscleScale = Vector3.one;

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
        foreach (var muscle in conn.GetMuscleCharges())
        {
            var _render = muscles[muscle.MuscleName];

            var val = Mathf.Clamp(muscle.CurrentCharge, 0, maxMuscleCharge);

            Color col = new Color(val / maxMuscleCharge, 0, 0);

            _render.GetPropertyBlock(propBlock, 0);
            propBlock.SetColor(_COL, col);
            _render.SetPropertyBlock(propBlock, 0);
        }
    }


    // @TODO: tochable cylinder for senses?

    void SetupMuscles()
    {
        foreach(Transform child in transform.Find("Muscles"))
        {
            var m = conn.GetMuscle(child.name);

            Vector3 pos = Vector3.zero;
            
            switch (m.Quadrant)
            {
                case CEMuscleQuadrant.MDL:  pos.z += dMuscleOffset;  break;
                case CEMuscleQuadrant.MVL:  pos.z += vMuscleOffset;  break;
                case CEMuscleQuadrant.MVR:  pos.z -= vMuscleOffset;  break;
                case CEMuscleQuadrant.MDR:  pos.z -= dMuscleOffset;  break;
            }

            pos.x = (m.MuscleId - 7) * muscleVerticalOffset;

            child.localPosition = pos;
            child.localScale = muscleScale;

            muscles[child.name] = child.GetComponent<Renderer>();
        }
    }

    [ContextMenu("Spawn Muscles")]
    private void __SpawnMuscles()
    {
        var mWrapper = transform.Find("Muscles");

        // spawn muscles 7-23 
        foreach (Muscle m in Connectome.GetMuscles())
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
