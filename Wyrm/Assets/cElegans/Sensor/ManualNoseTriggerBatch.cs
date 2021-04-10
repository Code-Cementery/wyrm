using System;
using System.Collections.Generic;
using UnityEngine;

namespace CESimpleModel
{
    public class ManualNoseTriggerBatch : MonoBehaviour
    {
        public CElegans elegans;
        public float foodSenseDelay = 0.5f;
        [Space(12)]
        public string[] foodSensors;
        public string[] noseSensors;


        Stimuli foodStimuli;
        Stimuli noseStimuli;


        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Sensing " + other.name);

            if (other.TryGetComponent(out Stimuli stimu))
            {
                switch(stimu.type)
                {
                    case StimuliType.GustatoryAttractant: foodStimuli = stimu; break;
                    case StimuliType.Touch: noseStimuli = stimu; break;
                }
            }

        }

        private void OnTriggerExit(Collider other)
        {
            Debug.Log("No longer sensing " + other.name);

            if (other.TryGetComponent(out Stimuli stimu))
            {
                switch (stimu.type)
                {
                    case StimuliType.GustatoryAttractant: foodStimuli = null; break;
                    case StimuliType.Touch: noseStimuli = null; break;
                }
            }
        }


        float timePassed = 0;
        bool waiting = false;

        private void FixedUpdate()
        {
            // delay for food sense
            if (waiting)
            {
                timePassed += Time.fixedDeltaTime;

                if (timePassed < foodSenseDelay)
                    return;
            }

            waiting = false;
            timePassed = 0;


            // simplified stimuli batch
            if (noseStimuli != null)
            {
                float dist = (noseStimuli.transform.position - transform.position).magnitude;

                foreach (var sen in noseSensors)
                    elegans.DendriteStimuli(sen, StimuliType.Touch, noseStimuli.value, dist);
            }
            else if (foodStimuli != null)
            {
                float dist = (foodStimuli.transform.position - transform.position).magnitude;

                foreach (var sen in foodSensors)
                    elegans.DendriteStimuli(sen, StimuliType.GustatoryAttractant, foodStimuli.value, dist);

                waiting = true;
                timePassed = 0;
            }
        }
    }
}
