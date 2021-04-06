using UnityEngine;

public class Stimuli : MonoBehaviour
{
    public StimuliType type;

    public float value;

    private void Awake()
    {
        Setup();
    }

    [ContextMenu("Setup Color")]
    void Setup()
    {
        // @TODO: set mat color
    }
}
