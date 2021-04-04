using UnityEngine;
using c302;


public class CElegans : MonoBehaviour
{
    public Connectome conn;
    public bool debug = true;


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


    public void SmellFoodBinary()
    {
        // @TODO: check these out

        conn.Activate("ADFL");
        conn.Activate("ADFR");
        conn.Activate("ASGR");
        conn.Activate("ASGL");
        conn.Activate("ASIL");
        conn.Activate("ASIR");
        conn.Activate("ASJR");
        conn.Activate("ASJL");
    }

    public void NoseBumpBinary() 
    {
        // @TODO: check these out

        conn.Activate("FLPR");
        conn.Activate("FLPL");
        conn.Activate("ASHL");
        conn.Activate("ASHR");
        conn.Activate("IL1VL");
        conn.Activate("IL1VR");
        conn.Activate("OLQDL");
        conn.Activate("OLQDR");
        conn.Activate("OLQVR");
        conn.Activate("OLQVL");
    }


    void FixedUpdate()
    {
        // @todo: stimulate senses

        conn.RunSimulation();

        // @todo: motor simulation
        SendMessage("__Simulation", conn, SendMessageOptions.DontRequireReceiver);

        // @todo: display motor color?

        conn.StepSimulation();
    }

    private void LateUpdate()
    {
    }
}
