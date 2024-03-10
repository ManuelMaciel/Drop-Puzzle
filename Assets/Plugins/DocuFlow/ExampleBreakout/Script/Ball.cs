using Plugin.DocuFlow.Documentation;
using UnityEngine;

namespace Plugin.DocuFlow.ExampleBreakout.Script
{
    [Doc("This class represents a ball in a game, handling its movement, launching, collisions, and reset.")]
    public class Ball : MonoBehaviour
    {
        public float launchSpeed = 5.0f;
        public float minSpeed = 4.0f;
        private Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            Launch();
        }

        private void FixedUpdate()
        {
            if (rb.velocity.magnitude < minSpeed)
            {
                rb.velocity = rb.velocity.normalized * minSpeed;
            }
        }

        [Doc("Launches the ball with a specific initial speed.")]
        public void Launch()
        {
            if (rb == null)
            {
                rb = GetComponent<Rigidbody>();
            }
            rb.velocity = Vector3.up * launchSpeed;
        }

        [Doc("Resets the ball's position and velocity to the start state.")]
        public void Reset()
        {
            rb.velocity = Vector3.zero;
            transform.position = Vector3.zero;
        }
    }
}



