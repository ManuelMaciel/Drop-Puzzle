using UnityEngine;

public class ScreenSizer : MonoBehaviour
{
    [SerializeField] private Collider2D rightObstacle;
    [SerializeField] private Collider2D leftObstacle;
    [SerializeField] private SpriteRenderer dropBoxSpriteRenderer;
    [SerializeField] private Camera camera;

    private void Start()
    {
        if (leftObstacle == null || rightObstacle == null)
        {
            Debug.LogError("Left or right obstacle collider is not assigned!");
            return;
        }
        
        Bounds leftBounds = leftObstacle.bounds;
        Bounds rightBounds = rightObstacle.bounds;
        
        float rightEdge = leftBounds.max.x;
        float leftEdge = rightBounds.min.x;
        
        AdjustCameraSize(leftEdge, rightEdge);
        AdjustWidth(dropBoxSpriteRenderer);
    }

    private void AdjustCameraSize(float leftEdge, float rightEdge)
    {
        float cameraWidth = rightEdge - leftEdge;
        
        camera.orthographicSize = (cameraWidth * Screen.height / Screen.width * 0.5f) * -1;
    }
    
    private void AdjustWidth(SpriteRenderer spriteRenderer)
    {
        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
        float spriteWidth = spriteRenderer.sprite.bounds.size.x;
        float scaleX = worldScreenWidth / spriteWidth;
        
        spriteRenderer.transform.localScale = new Vector3(scaleX, spriteRenderer.transform.localScale.y, 1f);
    }
}