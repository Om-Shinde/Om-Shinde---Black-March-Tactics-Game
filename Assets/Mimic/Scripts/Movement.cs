using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MimicSpace
{
    public class Movement : MonoBehaviour
    {
        [Header("Controls")]
        [Tooltip("Body Height from ground")]
        [Range(0.5f, 5f)]
        public float height = 0.8f;
        public float speed = 5f;
        public float velocityLerpCoef = 4f;

        [HideInInspector] public Vector3 velocity = Vector3.zero; // <- Now public, so external scripts can set it
        private Mimic myMimic;

        private void Start()
        {
            myMimic = GetComponent<Mimic>();
        }

        void Update()
        {
            // Assign velocity to the mimic to assure great leg placement
            myMimic.velocity = velocity;

            // Move character
            transform.position = transform.position + velocity * Time.deltaTime;

            // Keep height aligned with ground
            RaycastHit hit;
            Vector3 destHeight = transform.position;
            if (Physics.Raycast(transform.position + Vector3.up * 5f, -Vector3.up, out hit))
                destHeight = new Vector3(transform.position.x, hit.point.y + height, transform.position.z);

            transform.position = Vector3.Lerp(transform.position, destHeight, velocityLerpCoef * Time.deltaTime);
        }
    }
}
