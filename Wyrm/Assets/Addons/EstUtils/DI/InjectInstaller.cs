using Setup;
using System;

using UnityEngine;

public class InjectInstaller : MonoBehaviour
{
    private void Awake()
    {
        // Create DI binds
#if ENABLE_INPUT_SYSTEM
        InputMaster input = new InputMaster();
        DIInjector.Bind(input);

        // Setup
        input.Enable();
#endif
        // Resolve dependencies
        var monos = FindObjectsOfType<MonoBehaviour>();
        DIInjector.PopulateAll(monos);

        //Debug.Log("injecting");

        Destroy(this);
    }
}
