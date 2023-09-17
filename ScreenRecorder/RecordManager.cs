using System.IO;
using UnityEngine;
using UnityEngine.Video;

namespace Nikolla_L
{
    public class RecordManager : MonoBehaviour
    {
        [SerializeField] private string folderName;
        [SerializeField] private bool isAudioRecording = true;
        [SerializeField] private int bitRate;
        [SerializeField] private int fps;
        [SerializeField] private RecordController.VideoEncoder videoEncoder = RecordController.VideoEncoder.H264;
        [SerializeField] private GameObject afterVideoCompletePanel;
        [SerializeField] private VideoPlayer _videoPlayer;
        private string _recordedFilePath;

        void Start()
        {
            HideAfterVideoCompletePanel();
            Setup();
        }

        void Setup()
        {
            RecordController.instance.SetStoredFolderName(folderName);
            RecordController.instance.SetBitRate(bitRate);
            RecordController.instance.SetFPS(fps);
            RecordController.instance.SetVideoEncoder((int)videoEncoder);
            RecordController.instance.SetVideoSize((int)(Screen.width), (int)(Screen.height));
            RecordController.instance.SetAudioCapabilities(isAudioRecording);
        }

        public void StartRecording()
        {
            SetFileName();

            bool isIos = RecordController.instance.IsIosPlatform();
            if (isIos && RecordController.instance.IsRecordingAvailable() == false)
                return;

            RecordController.instance.StartRecording();
        }

        public void StopRecording()
        {
            _recordedFilePath = null;
            _recordedFilePath = RecordController.instance.StopRecording();

            if (RecordController.instance.IsIosPlatform())
            {
                PreviewVideo();
            }
            if (RecordController.instance.IsAndroidPlatform())
            {
                PreviewVideo();
            }
        }

        private void SetFileName()
        {
            System.DateTime now = System.DateTime.Now;
            string date = now.ToShortDateString().Replace('/', '_') + "_" + now.ToLongTimeString().Replace(':', '_');
            string fileName = "Rec_" + date + ".mp4";

            RecordController.instance.SetVideoName(fileName);
        }

        public void PreviewVideo()
        {
            if (_videoPlayer == null)
            {
                _videoPlayer = FindObjectOfType<VideoPlayer>();
            }
            if (_recordedFilePath != null && File.Exists(_recordedFilePath))
            {
                _videoPlayer.url = "file://" + _recordedFilePath; // Attaching URL to VideoPlayer

                // Optionally, prepare and then play the video.
                _videoPlayer.Prepare();
                _videoPlayer.prepareCompleted += OnVideoPrepared;
            }
        }

        private void OnVideoPrepared(VideoPlayer player)
        {
            player.prepareCompleted -= OnVideoPrepared; // Unsubscribe from the event
            player.Play(); // Play when prepared
        }

        // TODO: needs more implementation
        public void ShareVideo()
        {
            RecordController.instance.ShareVideo(_recordedFilePath, "Nnnn", "dddd");
        }

        public void HideAfterVideoCompletePanel()
        {
            afterVideoCompletePanel.SetActive(false);
        }
    }
}