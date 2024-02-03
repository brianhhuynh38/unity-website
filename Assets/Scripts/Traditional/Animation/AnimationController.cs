using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Website.Traditional.Animation {
    /// <summary>
    ///    A script to be attached to any GameObject planning to use any of the animations contained.
    ///    If any more simple animations are completed, an AnimationFactory may work better for this 
    ///    implementation if a significant amount of animations are added in the future.
    /// </summary>
    public class AnimationController : MonoBehaviour
    {
        ///<summary>Attributes for the Sin Animation</summary>
        [SerializeField] SinAnimation sinAnimation;
        ///<summary>Attributes for the Size Change Animation</summary>
        [SerializeField] SizeChangeAnimation sizeChangeAnimation;
        
        // Start is called at the beginning of instantiation
        void Start() {
            // Set original reference points
            sinAnimation.SetOriginalPosition(transform.position);
            sizeChangeAnimation.SetOriginalScale(transform.localScale);
        }

        // Update is called once per frame
        void Update() {
            sinAnimation.Animate(transform);
            sizeChangeAnimation.Animate(transform);
        }

        /// <summary>
        ///    An interface that describes the necessary functions for a simple animation: Animate and Reset
        /// </summary>
        private abstract class Animation {
            /// <summary>Whether or not the animation should play</summary>
            [SerializeField] public bool enabled;
            /// <summary>Disables animation temporarily if true</summary>
            protected bool asleep;
            /// <summary>
            ///    Incrementally animates on each frame based on the factors necessary for a given Animation
            /// </summary>
            ///   <param name="tf">The Transform component being modified</param>
            public abstract void Animate(Transform tf);
            /// <summary>
            ///    Stops the animation temporarily
            /// </summary>
            ///   <param name="asleep">Whether or not the animation is asleep or not</param>
            public void SetSleepStatus(bool asleep) {
                this.asleep = asleep;
            }
            /// <summary>
            ///    Resets the Transform to its original values
            /// </summary>
            ///   <param name="tf">The Transform component being reset</param>
            public abstract void Reset(Transform tf);
        }

        /// <summary>
        ///    An animation class where the GameObject moves up and down in a Sin pattern using a Sin function
        /// </summary>
        [Serializable]
        private class SinAnimation : Animation {
            /// <summary>The height of the wave from the middle</summary>
            [SerializeField] private float amplitude;
            /// <summary>The width of each of the waves</summary>
            [SerializeField] private float frequency;
            /// <summary>The direction the animation will occur, will be (1, 0, 0) by default</summary>
            private Vector3 direction = new Vector3(1, 0, 0);
            /// <summary>The original position of the Transform before the animation begins</summary>
            private Vector3 originalPosition;

            public override void Animate(Transform tf) {
                // Do not run if animation is disabled or asleep
                if (!enabled || asleep)
                    return;
                // Change position based on sin function
                tf.position = originalPosition + (direction * amplitude * Mathf.Sin(frequency * Time.time));
            }

            public override void Reset(Transform tf) {
                tf.position = originalPosition;
            }

            /// <summary>
            ///    Sets the original position for the animation
            /// </summary>
            /// <param name="originalPosition">Original position to be set for reference</param>
            public void SetOriginalPosition(Vector3 originalPosition) {
                this.originalPosition = originalPosition;
            }
        }

        /// <summary>
        ///    An animation class where the GameObject scales either up or down in size
        /// </summary>
        [Serializable]
        private class SizeChangeAnimation : Animation {
            /// <summary>The amount that the scale is multiplied by</summary>
            [SerializeField] private float multiplier;
            /// <summary>The amount of offset required for the object vertically to avoid clipping</summary>
            [SerializeField] private float verticalOffset;
            /// <summary>The original local scale of the object before animating</summary>
            private Vector3 originalScale;

            public override void Animate(Transform tf) {
                // Do not run if animation is disabled or asleep
                if (!enabled || asleep)
                    return;
                // Modify the local scale of the transform to be bigger
                tf.transform.localScale = multiplier * originalScale;
                // Apply the offset necessary to prevent clipping
                tf.position = new Vector3(tf.position.x,
                    tf.position.y + verticalOffset,
                    tf.position.z
                );
            }

            public override void Reset(Transform tf) {
                // Remove vertical offset
                tf.position = new Vector3(tf.position.x,
                    tf.position.y - verticalOffset,
                    tf.position.z
                );
                // Revert scale
                tf.transform.localScale = originalScale;
            }

            /// <summary>
            ///    Sets the original local scale for the animation
            /// </summary>
            /// <param name="originalScale">Original local scale to be set for reference</param>
            public void SetOriginalScale(Vector3 originalScale) {
                this.originalScale = originalScale;
            }
        }
    }
}
