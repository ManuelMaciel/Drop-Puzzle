using Plugin.DocuFlow.Documentation;
using UnityEngine;

namespace Plugin.DocuFlow.ExampleBreakout.Script
{
    [Doc("This class manages the overall game state including starting and ending the game, keeping score, and managing game objects.")]
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Doc("The current score of the game.")]
        public int Score { get; private set; }
    
        public Paddle paddle;
        public Ball ball;
        public Transform brickContainer;
        public GameObject brickPrefab;

        private BrickMapGenerator brickMapGenerator;
        private bool isGameActive = false;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                brickMapGenerator = new BrickMapGenerator(brickPrefab, brickContainer);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            StartGame();
        }

        [Doc("Starts a new game, resetting score and enabling game objects.")]
        public void StartGame()
        {
            Score = 0;
            isGameActive = true;
            paddle.gameObject.SetActive(true);
            ball.gameObject.SetActive(true);
            brickMapGenerator.CreateBricks();
            ball.Launch();
        }

        [Doc("Ends the current game, disabling game objects and clearing the play field.")]
        public void EndGame()
        {
            isGameActive = false;
            paddle.gameObject.SetActive(false);
            ball.gameObject.SetActive(false);
            brickMapGenerator.DestroyBricks();
        }

        private void Update()
        {
            if (!isGameActive)
                return;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ball.Launch();
            }

            CheckBallOutOfBounds();
        }

        private void CheckBallOutOfBounds()
        {
            if (ball.transform.position.y < paddle.transform.position.y - 1)
            {
                ball.Reset();
                paddle.Reset();
            }
        }

        [Doc("Increments the current game score by the specified points.")]
        public void IncrementScore(int points)
        {
            Score += points;
        }
    }
}
