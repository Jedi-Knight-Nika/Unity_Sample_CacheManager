using System;
using UnityEngine;

namespace Nikolla_L
{
    /// <summary>
    /// Managing input events
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }

        // Cache for the current mouse position
        private Vector2 _mousePosition;

        // Cached screen dimensions and boundaries for event triggering
        private float halfScreenWidth;
        private float lowerYBound;
        private float upperYBound;

        /// <summary>
        /// Events for different quadrants of the screen
        /// </summary>
        public Action onClickedUpLeft, onClickedUpRight, onClickedDownLeft, onClickedDownRight;

        /// <summary>
        /// General event for mouse clicks
        /// </summary>
        public Action onClickedMouse;

        private void Awake()
        {
            // Destroy object if an instance already exists.
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            // Assign singleton instance.
            Instance = this;
        }

        private void Start()
        {
            halfScreenWidth = Screen.width / 2;
            lowerYBound = Screen.height / 10;
            upperYBound = 5 * (Screen.height - lowerYBound) / 12;
        }

        void Update()
        {
            CheckInput();
        }

        /// <summary>
        /// Checks for mouse clicks and fires corresponding events
        /// </summary>
        void CheckInput()
        {
            _mousePosition = Input.mousePosition;

            if (Input.GetMouseButtonDown(0))
            {
                onClickedMouse?.Invoke();
                ClickOnQuarter();
            }
        }

        /// <summary>
        /// Determines which quadrant of the screen was clicked and invokes the corresponding event
        /// </summary>
        void ClickOnQuarter()
        {
            bool isLeft = _mousePosition.x < halfScreenWidth;
            bool isLowerHalf = _mousePosition.y > lowerYBound && _mousePosition.y < upperYBound;
            bool isUpperHalf = _mousePosition.y >= upperYBound && _mousePosition.y < (Screen.height - lowerYBound);

            // Trigger specific quadrant events
            if (isLeft && isLowerHalf)
            {
                onClickedDownLeft?.Invoke();
            }
            else if (!isLeft && isLowerHalf)
            {
                onClickedDownRight?.Invoke();
            }
            else if (isLeft && isUpperHalf)
            {
                onClickedUpLeft?.Invoke();
            }
            else if (!isLeft && isUpperHalf)
            {
                onClickedUpRight?.Invoke();
            }
        }
    }
}