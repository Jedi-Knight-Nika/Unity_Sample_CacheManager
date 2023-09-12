using UnityEngine;
using UnityEngine.Video;
using System.Collections;

namespace Nikolla_L
{
    public class VideoPlayerManager : MonoBehaviour
    {
        // UI and Video Elements
        [SerializeField] private GameObject cinemaPlane;
        [SerializeField] private GameObject buttonPlay;
        [SerializeField] private GameObject buttonPause;
        [SerializeField] private GameObject knob;
        [SerializeField] private GameObject progressBar;
        [SerializeField] private GameObject progressBarBG;
        [SerializeField] private VideoPlayer videoPlayer;

        // Knob and ProgressBar Values
        private float maxKnobValue;
        private float newKnobX;
        private float maxKnobX;
        private float minKnobX;
        private float knobPosY;
        private float simpleKnobValue;
        private float knobValue;
        private float progressBarWidth;

        // state Variables
        private bool knobIsDragging;
        private bool videoIsJumping = false;
        private bool videoIsPlaying = false;

        // Iinitialize variables and set up UI elements
        private void Start()
        {
            knobPosY = knob.transform.localPosition.y;
            buttonPause.SetActive(true);
            buttonPlay.SetActive(false);
            videoPlayer.frame = (long)100;
            progressBarWidth = progressBarBG.transform.localScale.x;
        }

        private void Update()
        {
            UpdateProgressBarAndKnob();
            HandleMouseClick();
        }

        // update the progress bar and knob based on the video's current frame
        private void UpdateProgressBarAndKnob()
        {
            if (!knobIsDragging && !videoIsJumping)
            {
                if (videoPlayer.frameCount > 0)
                {
                    float progress = (float)videoPlayer.frame / (float)videoPlayer.frameCount;
                    progressBar.transform.localScale = new Vector3(progressBarWidth * progress, progressBar.transform.localScale.y, 0);
                    knob.transform.localPosition = new Vector2(progressBar.transform.localPosition.x + (progressBarWidth * progress), knobPosY);
                }
            }
        }

        // handle mouse clicks on Play and Pause buttons
        private void HandleMouseClick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 pos = Input.mousePosition;
                Collider2D hitCollider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(pos));

                if (hitCollider != null && (hitCollider.CompareTag(buttonPause.tag) || hitCollider.CompareTag(buttonPlay.tag)))
                {
                    BtnPlayVideo();
                }
            }
        }

        public void KnobOnPressDown()
        {
            VideoStop();

            minKnobX = progressBar.transform.localPosition.x;
            maxKnobX = minKnobX + progressBarWidth;
        }

        public void KnobOnRelease()
        {
            knobIsDragging = false;
            CalcKnobSimpleValue();
            VideoPlay();
            VideoJump();
            StartCoroutine(DelayedSetVideoIsJumpingToFalse());
        }

        IEnumerator DelayedSetVideoIsJumpingToFalse()
        {
            yield return new WaitForSeconds(1);
            SetVideoIsJumpingToFalse();
        }

        public void KnobOnDrag()
        {
            knobIsDragging = true;
            videoIsJumping = true;

            Vector3 curScreenPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
            knob.transform.position = new Vector2(curPosition.x, curPosition.y);

            newKnobX = knob.transform.localPosition.x;
            if (newKnobX > maxKnobX) {
                newKnobX = maxKnobX;
            }
            if (newKnobX < minKnobX) {
                newKnobX = minKnobX;
            }

            knob.transform.localPosition = new Vector2(newKnobX, knobPosY);
            CalcKnobSimpleValue();
            progressBar.transform.localScale = new Vector3(simpleKnobValue * progressBarWidth, progressBar.transform.localScale.y, 0);
        }

        private void SetVideoIsJumpingToFalse()
        {
            videoIsJumping = false;
        }

        private void CalcKnobSimpleValue()
        {
            maxKnobValue = maxKnobX - minKnobX;
            knobValue = knob.transform.localPosition.x - minKnobX;
            simpleKnobValue = knobValue / maxKnobValue;
        }

        private void VideoJump()
        {
            var frame = videoPlayer.frameCount * simpleKnobValue;
            videoPlayer.frame = (long)frame;
        }

        private void BtnPlayVideo()
        {
            if (videoIsPlaying)
            {
                VideoStop();
            }
            else
            {
                VideoPlay();
            }
        }

        private void VideoStop()
        {
            videoIsPlaying = false;
            videoPlayer.Pause();

            buttonPause.SetActive(false);
            buttonPlay.SetActive(true);
        }

        private void VideoPlay()
        {
            videoIsPlaying = true;
            videoPlayer.Play();

            buttonPause.SetActive(true);
            buttonPlay.SetActive(false);
        }
    }
}