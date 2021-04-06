using c302;
using System;
using UnityEngine;

namespace CESimpleModel
{
    public class CESimpleControl : MonoBehaviour, ICEControl
    {
        Connectome conn;
        //Rigidbody rb;
        CharacterController control;

        public float minSpeed = 75;
        public float maxSpeed = 150;
        public float rotateSpeed = 30f;
        [Space]
        public float simulationStepInterval = 0.03f;

        [Space]
        public int minMuscleGroup = 7;
        public int maxMuscleGroup = 23;

        private void Start()
        {
            conn = GetComponent<CElegans>().conn;
            control = GetComponent<CharacterController>();
        }

        float timePassed = 0;

        void FixedUpdate()
        {
            timePassed += Time.fixedDeltaTime;

            if (timePassed >= simulationStepInterval)
            {
                timePassed = 0f;

                float accumLeft = 0;
                float accumRight = 0;


                foreach ((MuscleDescriptor m, int charge) in conn.GetMuscleStates())
                {
                    // this model only accounts muscles 7 -> 23
                    if (m.MuscleId < minMuscleGroup || m.MuscleId > maxMuscleGroup)
                        continue;


                    if (m.Quadrant == CEMuscleQuadrant.MDL || m.Quadrant == CEMuscleQuadrant.MVL)
                        accumLeft += charge;
                    else if (m.Quadrant == CEMuscleQuadrant.MDR || m.Quadrant == CEMuscleQuadrant.MVR)
                        accumRight += charge;
                }

                float x, z, p_speed = 0f;

                if (Math.Abs(accumLeft) <= 0.0001f || Math.Abs(accumRight) <= 0.0001f)
                {
                    // stop
                    z = 0f;
                    x = 0f;
                }
                else if (accumRight <= 0 && accumLeft >= 0)
                {
                    // right rotate
                    z = 0f;
                    x = 1f;
                }
                else if (accumRight >= 0 && accumLeft <= 0)
                {
                    // left rotate
                    z = 0f;
                    x = -1f;
                }
                else
                {
                    float turnratio = accumRight / accumLeft;

                    // @TODO: what the fuck is this logic?
                    if (turnratio <= 0.6)
                        // left rotate
                        x = -1f;
                    else if (turnratio >= 2)
                        // right ratio
                        x = 1f;
                    else
                        x = 0f;

                    if (accumRight >= 0 && accumLeft >= 0)
                    {
                        // forward
                        z = 1f;
                        p_speed = Mathf.InverseLerp(75f, 150f, Mathf.Clamp(Mathf.Abs(accumLeft) + Mathf.Abs(accumRight), 75f, 150f));
                    }
                    else
                    {
                        // backward
                        z = -1f;
                        p_speed = 1f;
                    }
                }

                float speed = Mathf.Lerp(minSpeed, maxSpeed, p_speed);

                // control by calculated x,z  vectors
                transform.Rotate(0, x * rotateSpeed * Time.fixedDeltaTime, 0);
                control.Move(-transform.right * z * speed * Time.fixedDeltaTime);
            }
        }
    }
}
