using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class SensingProfile : ScriptableObject
{



    /// <summary>
    /// </summary>
    /// <returns>Activation interval [ms]</returns>
    public abstract float OnActivationInterval(float valueDiff);

}
