using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(RawImage), typeof(AspectRatioFitter))]
public class VideoAspectRatioFitter : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    private RawImage rawImage;
    private AspectRatioFitter aspectRatioFitter;

    void Start()
    {
        rawImage = GetComponent<RawImage>();
        aspectRatioFitter = GetComponent<AspectRatioFitter>();

        // Attach prepare completed event
        videoPlayer.prepareCompleted += OnVideoPrepared;
    }

    void OnVideoPrepared(VideoPlayer vp)
    {
        // Set the RawImage texture
        rawImage.texture = videoPlayer.texture;

        // Update the AspectRatioFitter's aspect ratio
        float videoRatio = (float)vp.width / (float)vp.height;
        aspectRatioFitter.aspectRatio = videoRatio;
    }
}