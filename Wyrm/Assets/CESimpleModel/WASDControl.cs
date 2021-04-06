using c302;
using System;
using UnityEngine;

namespace CESimpleModel
{
    [RequireComponent(typeof(CharacterController))]
    public class WASDControl : MonoBehaviour, ICEControl
    {
        Connectome conn;

        public float speed = 75;
        public float rotateSpeed = 30;
        [Space]
        public float simulationStepInterval = 0.03f;

        CharacterController control;

        Vector3 moveDirection;

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

                float x = Input.GetAxis("Horizontal");
                float z = Input.GetAxis("Vertical");

                //moveDirection = transform.TransformDirection(new Vector3(x, 0, z)) * speed;
                transform.Rotate(0, x * rotateSpeed * Time.fixedDeltaTime, 0);
                control.Move(transform.right * z * -speed * Time.fixedDeltaTime);
            }
        }
    }
}