using UnityEngine;
using c302;
using System;

public class CElegans : MonoBehaviour
{
    public Connectome conn;
    public bool debug = true;

    [Tooltip("~neuron firing interval")]
    public float simulationStepInterval = 0.03f;

    void Awake()
    {
        if (conn == null)
            gameObject.SetActive(false);
    }

    private void Start()
    {
        if (conn == null)
            Debug.LogError("[C.Elegans] Connectome not found!");
    }

    /**
     * mechanical stimuli:
     *   ALM, PLM, AVM, PVM, PVD, ADE, PDE (midbody)
     *   ASH, FLP, OLQ, CEP, IL1 (nose tip)
     *   
     * hard body touch:
     *   PVD
     * 
     * gustatory attractant:
     *   ASE
     * 
     * gustatory repellent:
     *   ASH
     *   ASI, ADF, ASG, ASJ, ASK, ADL (less)
     *   IL2 (head)
     *   PHA, PHB (tail)
     * 
     * Chemotaxis to volatile odorants
     *   AWA, AWB, AWC, ASH
     *   
     * CO2:
     *   AFD, BAG, ASE
     *   AQR, PQR, URX (less)
     *   
     * O2: 
     *   AQR, PQR, URX
     * 
     * thermo:
     *   AFD
     *   AWC, ASI, FLP, PHC (less)
     *   
     * Low noxious temperatures:
     *   PVD
     *   
     * ADL, ASH and AWB neurons respond to several repulsive stimuli 
     * to produce avoidance behaviour [27,28]. 
     * These stimuli include hyperosmolarity, mechanical stimuli and volatile repellents. 
     * By contrast, sensory neurons called AWA, AWC and ASE are involved 
     * in responses to an attractant [19,28]. 
     * Moreover, ASH together with ASJ, AWB and ASK neurons mediate 
     * light avoidance and electrosensory navigation [29,30].
     */
    internal void DendriteStimuli(string name, StimuliType type, float value, float dist)
    {
        //Debug.Log($"Dendrite stimuli: {name} {type} {dist}");

        conn.Activate(name);
    }




    //public void SmellFoodBinary()
    //{
    //    // @TODO: check these out

    //    conn.Activate("ADFL");
    //    conn.Activate("ADFR");
    //    conn.Activate("ASGR");
    //    conn.Activate("ASGL");
    //    conn.Activate("ASIL");
    //    conn.Activate("ASIR");
    //    conn.Activate("ASJR");
    //    conn.Activate("ASJL");
    //}

    //public void NoseBumpBinary()
    //{
    //    // @TODO: check these out

    //    conn.Activate("FLPR");
    //    conn.Activate("FLPL");
    //    conn.Activate("ASHL");
    //    conn.Activate("ASHR");
    //    conn.Activate("IL1VL");
    //    conn.Activate("IL1VR");
    //    conn.Activate("OLQDL");
    //    conn.Activate("OLQDR");
    //    conn.Activate("OLQVR");
    //    conn.Activate("OLQVL");
    //}


    float timePassed = 0;

    void FixedUpdate()
    {
        timePassed += Time.fixedDeltaTime;

        if (timePassed >= simulationStepInterval)
        {
            timePassed = 0f;

            conn.RunSimulation();
            conn.StepSimulation();
        }
    }

    private void LateUpdate()
    {
    }
}
