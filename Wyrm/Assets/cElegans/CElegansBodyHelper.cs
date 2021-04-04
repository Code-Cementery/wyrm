using c302;
using System;
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

    public GameObject musclePrefab;

    public Connectome conn;
    MaterialPropertyBlock propBlock;

    const string _COL = "_BaseColor";

    void Start()
    {
        conn = GetComponent<CElegans>().conn;
        propBlock = new MaterialPropertyBlock();

        SetupMuscles();
    }

    /// GameMessage
    void __Simulation(Connectome conn)
    {
        // @TODO:  later: solve within update?


        //_render.GetPropertyBlock(propBlock, 0);
        //propBlock.SetTexture(_TEX, texture);
        //_render.SetPropertyBlock(propBlock, 0);
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
                var obj = PrefabUtility.InstantiatePrefab(musclePrefab, mWrapper);
                obj.name = m.MuscleName;
            }
        }

        if (conn != null)
            SetupMuscles();
    }
}
