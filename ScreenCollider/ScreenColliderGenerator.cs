using UnityEngine;

/// <summary>
/// Generates colliders around the screen edges at the start of the scene
/// </summary>
namespace Nikolla_L
{
    public class ScreenColliderGenerator : MonoBehaviour
    {
        void Start()
        {
            Vector2 lDCorner = GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0, 0f, GetComponent<Camera>().nearClipPlane));
            Vector2 rUCorner = GetComponent<Camera>().ViewportToWorldPoint(new Vector3(1f, 1f, GetComponent<Camera>().nearClipPlane));
            Vector2[] colliderpoints;

            EdgeCollider2D upperEdge = new GameObject("UpperEdge").AddComponent<EdgeCollider2D>();
            colliderpoints = upperEdge.points;
            colliderpoints[0] = new Vector2(lDCorner.x, rUCorner.y);
            colliderpoints[1] = new Vector2(rUCorner.x, rUCorner.y);
            upperEdge.points = colliderpoints;

            EdgeCollider2D lowerEdge = new GameObject("LowerEdge").AddComponent<EdgeCollider2D>();
            colliderpoints = lowerEdge.points;
            colliderpoints[0] = new Vector2(lDCorner.x, lDCorner.y);
            colliderpoints[1] = new Vector2(rUCorner.x, lDCorner.y);
            lowerEdge.points = colliderpoints;

            EdgeCollider2D leftEdge = new GameObject("LeftEdge").AddComponent<EdgeCollider2D>();
            colliderpoints = leftEdge.points;
            colliderpoints[0] = new Vector2(lDCorner.x, lDCorner.y);
            colliderpoints[1] = new Vector2(lDCorner.x, rUCorner.y);
            leftEdge.points = colliderpoints;

            EdgeCollider2D rightEdge = new GameObject("RightEdge").AddComponent<EdgeCollider2D>();

            colliderpoints = rightEdge.points;
            colliderpoints[0] = new Vector2(rUCorner.x, rUCorner.y);
            colliderpoints[1] = new Vector2(rUCorner.x, lDCorner.y);
            rightEdge.points = colliderpoints;
        }
    }
}
