using UnityEngine;
using Firebase;
using Firebase.Analytics;
using System;
using System.Collections.Generic;

namespace Nikolla_L
{
    /// <summary>
    /// Manages analytics events using Firebase
    /// </summary>
    public class AnalyticsManager : MonoBehaviour
    {
        // event types
        private enum EventType
        {
            Start,
            // ...
        }

        private enum Action
        {
            Init,
            Play,
            // ...
        }

        private Dictionary<Action, string> actionMappings = new Dictionary<Action, string>()
        {
            { Action.Init, "init" },
            { Action.Play, "play" },
            //...
        };

        /// <summary>
        /// Initializes Firebase and logs a "start" event
        /// </summary>
        private void InitializeFirebase()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
                LogEvent(EventType.Start, Action.Init);
            });
        }

        /// <summary>
        /// Log a custom event with specified parameters
        /// </summary>
        /// <param name="eventType">Type of the event</param>
        /// <param name="action">Specific action that occurred</param>
        private void LogEvent(EventType eventType, Action action)
        {
            string eventName = eventType.ToString().ToLower();
            string actionName = actionMappings.ContainsKey(action) ? actionMappings[action] : action.ToString().ToLower();

            Debug.Log($"Logging event: {eventName}, Action: {actionName}");
            FirebaseAnalytics.LogEvent(eventName, new Parameter("action", actionName));
        }


        /// <summary>
        /// Log an error message to Firebase Analytics
        /// </summary>
        /// <param name="errorMessage">Error message</param>
        private void LogErrorToFirebaseAnalytics(string errorMessage)
        {
            Debug.Log($"Logging error: {errorMessage}");
            FirebaseAnalytics.LogEvent("error", "message", errorMessage);
        }

        /// <summary>
        /// Reusable method to invoke function and handle errors by logging into analytics
        /// </summary>
        /// <param name="action">Function or action to invoke</param>
        public void TryCatch(System.Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception e)
            {
                LogErrorToFirebaseAnalytics(e.Message);
                return;
            }
        }

        // Initialize
        void Start()
        {
            InitializeFirebase();
        }

        // Event handlers
        //...
    }
}