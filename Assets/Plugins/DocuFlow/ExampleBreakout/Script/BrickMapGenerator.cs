using Plugin.DocuFlow.Documentation;
using UnityEngine;

namespace Plugin.DocuFlow.ExampleBreakout.Script
{
    [Doc("This class generates a grid of bricks, manages their creation, color, and destruction.")]
    public class BrickMapGenerator
    {
        private GameObject brickPrefab;
        private Transform brickContainer;

        private float x_min = -30f;
        private float x_max = 30f;
        private float y_min = 8f;
        private float y_max = 16f;

        private int gridRows = 5; 
        private int gridColumns = 10;

        private Color[] colors = new Color[] {
            Color.red,
            Color.green,
            Color.blue,
            Color.yellow,
            Color.magenta,
            Color.cyan
        };
    
        public BrickMapGenerator(GameObject brickPrefab, Transform brickContainer)
        {
            this.brickPrefab = brickPrefab;
            this.brickContainer = brickContainer;
        }
    
        [Doc("Creates a grid of bricks with random colors within specified bounds.")]
        public void CreateBricks()
        {
            System.Random rand = new System.Random();

            float cellWidth = (x_max - x_min) / gridColumns;
            float cellHeight = (y_max - y_min) / gridRows;

            for (int i = 0; i < gridRows; i++)
            {
                for (int j = 0; j < gridColumns; j++)
                {
                    float x = x_min + j * cellWidth;
                    float y = y_min + i * cellHeight;

                    Vector3 position = new Vector3(x, y, 0f);

                    GameObject brick = Object.Instantiate(brickPrefab, position, Quaternion.identity, brickContainer);

                    brick.GetComponent<Renderer>().material.color = colors[rand.Next(colors.Length)];
                }
            }
        }
    
        [Doc("Destroys all bricks in the specified brick container.")]
        public void DestroyBricks()
        {
            foreach (Transform brick in brickContainer)
            {
                Object.Destroy(brick.gameObject);
            }
        }
    }
}

