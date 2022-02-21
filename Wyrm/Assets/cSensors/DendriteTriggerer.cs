using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class DendriteTriggerer : MonoBehaviour
{
    public float Frequency;
    public ConnectomeSimulator elegans;

    [Tooltip("Which neurons to active with frequency")]
    public string[] neurons;

    private void OnEnable()
    {
        if (elegans == null)
        {
            Debug.LogError("[DendriteTrigger] Elegans connectome is null.");
        }
    }

    private void Update()
    {
        //elegans
        
    }
}
