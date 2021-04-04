using c302;
using UnityEngine;

public class ConnectomeInstaller : MonoBehaviour
{
    public TextAsset m_ConnectomeFile;

    public CElegans worm;


    void Awake()
    {
        using (var s = new SynapseWeightReader())
        {
            var conn = s.ReadSynapses(m_ConnectomeFile);

            worm.conn = conn;
            worm.gameObject.SetActive(true);

            //if (conn.Count != 302)
            //    Debug.LogError($"Error! Connectome should have 302 connections, {conn.Count} found.");
            Debug.Log($"Created connectome with {conn.Count} neurons and {conn.SynCount} synapses");
        }

        Destroy(this);
    }
}
