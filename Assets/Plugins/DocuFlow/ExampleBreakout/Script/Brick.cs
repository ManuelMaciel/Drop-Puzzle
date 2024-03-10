using Plugin.DocuFlow.Documentation;
using UnityEngine;

namespace Plugin.DocuFlow.ExampleBreakout.Script
{
    [Doc("This class represents a brick in the game, handling its destruction.")]
    public class Brick : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ball"))
            {
                DestroyBrick();
            }
        }

        [Doc("Destroys the brick and increments the game score.")]
        public void DestroyBrick()
        {
            GameManager.Instance.IncrementScore(1);
            Destroy(gameObject);
        }
    }
}

