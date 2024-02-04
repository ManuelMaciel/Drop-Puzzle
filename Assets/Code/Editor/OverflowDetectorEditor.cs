using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
    [CustomEditor(typeof(OverflowDetector))]
    public class OverflowDetectorEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void DrawCustomGizmo(OverflowDetector overflowDetector, GizmoType gizmoType)
        {
            Vector3 position = overflowDetector.transform.position;
            Collider2D collider2D = overflowDetector.GetComponent<Collider2D>();
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(position, collider2D.bounds.size);
        }
    }
}