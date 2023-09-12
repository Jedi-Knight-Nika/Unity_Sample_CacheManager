using UnityEngine;
using Nikolla_L;

public class Knob : MonoBehaviour
{
    [SerializeField] private VideoPlayerManager videoPlayerScript;

    void OnMouseDown()
    {
        videoPlayerScript.KnobOnPressDown();
    }

    void OnMouseUp()
    {
        videoPlayerScript.KnobOnRelease();
    }

    void OnMouseDrag()
    {
        videoPlayerScript.KnobOnDrag();
    }
}

