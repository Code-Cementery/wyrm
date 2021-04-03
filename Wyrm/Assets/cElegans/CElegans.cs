using UnityEngine;
using c302;


public class CElegans : MonoBehaviour
{
    public Connectome conn;

    void Awake()
    {
        if (conn == null)
            gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        // @todo: stimulate senses

        conn.RunSimulation();

        // @todo: motor simulation

        // @todo: display motor
        conn.StepSimulation();
    }
}
