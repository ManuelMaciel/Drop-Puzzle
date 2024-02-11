using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ScreenSizer : MonoBehaviour
{
    [SerializeField] private Collider2D rightObstacle;
    [SerializeField] private Collider2D leftObstacle;
    
    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

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
    }

    private void AdjustCameraSize(float leftEdge, float rightEdge)
    {
        float cameraWidth = rightEdge - leftEdge;
        
        _camera.orthographicSize = (cameraWidth * Screen.height / Screen.width * 0.5f) * -1;
    }
}