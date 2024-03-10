using Plugin.DocuFlow.Documentation;
using UnityEngine;

namespace Plugin.DocuFlow.ExampleBreakout.Script
{
    [Doc("This class represents the player-controlled paddle, handling its movement and reset.")]
    public class Paddle : MonoBehaviour
    {
        public float movementSpeed = 10f;
        private Vector3 lastPosition;

        [Doc("The velocity of the paddle.")]
        public Vector3 Velocity { get; private set; }
    
        public Vector3 resetPosition = Vector3.zero;
    
        public float leftBoundary = -8.0f;
        public float rightBoundary = 8.0f;

        private void Start()
        {
            resetPosition = transform.position;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            lastPosition = transform.position;
        
            float horizontalInput = Input.GetAxis("Horizontal");
            float movement = horizontalInput * movementSpeed * Time.deltaTime;
            transform.Translate(movement, 0f, 0f);

            float clampedX = Mathf.Clamp(transform.position.x, leftBoundary, rightBoundary);
            transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
        
            Velocity = (transform.position - lastPosition) / Time.deltaTime;
        }
    
        [Doc("Resets the paddle's position to the start state.")]
        public void Reset()
        {
            transform.position = resetPosition;
        }
    }
}