using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.cElegans
{
    public class CElegansNeuronHelper : MonoBehaviour
    {
        public CElegans elegans;
        public GameObject CellPrefab;

        [Space]
        public float maxNeuronCharge = 32f;

        Dictionary<string, Image> m_Neurons;

        private void Start()
        {
            m_Neurons = new Dictionary<string, Image>();

            foreach (var neuron in elegans.conn.GetNeurons())
            {
                var obj = Instantiate(CellPrefab, transform);

                m_Neurons[neuron] = obj.GetComponent<Image>();
            }
        }

        float timePassed = 0;

        private void FixedUpdate()
        {
            timePassed += Time.fixedDeltaTime;

            if (timePassed >= 0.05f)
            {
                timePassed = 0f;

                foreach ((string neuron, int charge) in elegans.conn.GetNeuronStates(false))
                {
                    var val = Mathf.Clamp(charge, 0, maxNeuronCharge);
                    var p = val / maxNeuronCharge;
                    Color col = charge >= 0 ? new Color(p, 0, 0) : new Color(0, p, 0);

                    m_Neurons[neuron].color = col;
                }
            }
        }
    }
}
