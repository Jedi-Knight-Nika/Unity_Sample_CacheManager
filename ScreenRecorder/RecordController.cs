using UnityEngine;
using System.IO;
using System.Collections;

namespace Nikolla_L
{
    public class RecordController : MonoBehaviour
    {
        public static RecordController instance;
        private string _fileProvider = "com.SmileSoft.unityplugin.ScreenRecordProviderUNIQUE";

        private AndroidJavaObject screenRecorder;
        private ReplayKitHelper _iosRecorder;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            Setup();
        }

        void Setup()
        {
            if (IsAndroidPlatform())
            {
                screenRecorder = new AndroidJavaObject("com.SmileSoft.unityplugin.ScreenCapture.ScreenRecordFragment");
                screenRecorder?.Call("SetUp");
            }

            if (IsIosPlatform())
            {
                _iosRecorder = new ReplayKitHelper();
            }
        }

        public void StartRecording()
        {
            if (IsAndroidPlatform())
                screenRecorder?.Call("StartRecording");

            if (IsIosPlatform())
            {
                if (_iosRecorder.IsRecorderAvailable())
                {
                    _iosRecorder.StartRecording();
                }
            }
        }

        public string StopRecording()
        {
            if (IsAndroidPlatform())
            {
                string recordedPath = screenRecorder?.Call<string>("StopRecording");
                Debug.Log("Unity>> record path : " + recordedPath);
                return recordedPath;
            }

            if (IsIosPlatform())
            {
                _iosRecorder.StopRecording();
            }


            return null;
        }

        public void SetVideoStoringDestination(string destination)
        {
            if (IsAndroidPlatform())
                screenRecorder?.Call("SetVideoStoringDestination", destination);
        }

        public void SetStoredFolderName(string folderName)
        {
            if (IsAndroidPlatform())
                screenRecorder?.Call("SetVideoStoredFolderName", folderName);
        }

        public void SetVideoName(string videoName)
        {
            if (IsAndroidPlatform())
                screenRecorder?.Call("SetVideoName", videoName);
        }

        public void SetGalleryAddingCapabilities(bool canAddintoGallery)
        {
            if (IsAndroidPlatform())
                screenRecorder?.Call("SetGalleryAddingCapabilities", canAddintoGallery);
        }

        public void SetAudioCapabilities(bool canRecordAudio)
        {
            if (IsAndroidPlatform())
                screenRecorder?.Call("SetAudioCapabilities", canRecordAudio);
            if (IsIosPlatform())
                _iosRecorder.SetAudioCapability(canRecordAudio);
        }

        public void SetFPS(int fps)
        {
            if (IsAndroidPlatform())
                screenRecorder?.Call("SetVideoFps", fps);
        }

        public void SetBitRate(int bitRate)
        {
            if (IsAndroidPlatform())
                screenRecorder?.Call("SetBitrate", bitRate);
        }

        public void SetVideoSize(int width, int height)
        {
            if (IsAndroidPlatform())
                screenRecorder?.Call("SetVideoSize", width, height);
        }

        public void SetVideoEncoder(int encoder)
        {
            if (IsAndroidPlatform())
                screenRecorder?.Call("SetVideoEncoder", encoder);
        }

        public void PreviewVideo(string videoPath)
        {
            if (IsAndroidPlatform() && (videoPath != null && File.Exists(videoPath)))
            {
                screenRecorder?.Call("PreviewVideo", videoPath);
                return;
            }

            if (IsIosPlatform())
            {
                StartCoroutine(IosPreview((isSuccess) => { }));
            }
        }

        private IEnumerator IosPreview(System.Action<bool> callback)
        {
            yield return new WaitForSeconds(1.0f);
            bool isSuccess = _iosRecorder.ShowPreview();
            callback(isSuccess);
        }

        public void ShareVideo(string filePath, string message, string title)
        {
            if (IsAndroidPlatform() && (filePath != null && filePath.EndsWith(filePath)))
                screenRecorder?.Call("ShareVideo", filePath, message, title, _fileProvider);
        }

        public enum VideoEncoder
        {
            DEFAULT = 0, H263 = 1, H264 = 2, MPEG_4_SP = 3, VP8 = 4, HEVC = 5
        }

        public void OnRecordPermissionGranted(string status)
        {
            if (status == "True")
            {
                Debug.Log("Unity>> Record Permission Granted");
            }
            else
            {
                Debug.Log("Unity>> Record Permission Not Granted");
            }
        }

        // callBack From Android library
        public void OnRecordStartStatus(string status)
        {
            if (status == "True")
            {
                Debug.Log("Unity>> Record Started");
            }
            else
            {
                Debug.Log("Unity>> Record Failed");
            }
        }

        // works only in IOS
        public bool IsRecordingAvailable()
        {
            if (IsIosPlatform())
            {
                return _iosRecorder.IsRecorderAvailable();
            }

            return true;
        }

        public bool IsAndroidPlatform()
        {
            bool result = false;

#if UNITY_ANDROID && !UNITY_EDITOR
	result = true;
#endif

            return result;
        }

        public bool IsIosPlatform()
        {
            bool result = false;

#if UNITY_IPHONE && !UNITY_EDITOR
		result = true;
#endif
            return result;
        }
    }
}