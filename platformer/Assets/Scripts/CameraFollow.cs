﻿using UnityEngine;

namespace Assets.Scripts
{
    public class CameraFollow : MonoBehaviour
    {

        public Transform ObjectToFollow;

        private const float XMargin = 0.1f; // Distance in the x axis the player can move before the camera follows.
        private const float YMargin = 0.1f; // Distance in the y axis the player can move before the camera follows.
        private const float XSmooth = 8f; // How smoothly the camera catches up with it's target movement in the x axis.
        private const float YSmooth = 8f; // How smoothly the camera catches up with it's target movement in the y axis.
        
        public Vector2 MaxXAndY;		// The maximum x and y coordinates the camera can have.
        public Vector2 MinXAndY;		// The minimum x and y coordinates the camera can have.



        bool CheckXMargin()
        {
            // Returns true if the distance between the camera and the player in the x axis is greater than the x margin.
            return Mathf.Abs(transform.position.x - ObjectToFollow.position.x) > XMargin;
        }


        bool CheckYMargin()
        {
            // Returns true if the distance between the camera and the player in the y axis is greater than the y margin.
            return Mathf.Abs(transform.position.y - ObjectToFollow.position.y) > YMargin;
        }


        public void FixedUpdate()
        {
            TrackPlayer();
        }


        void TrackPlayer()
        {
            // By default the target x and y coordinates of the camera are it's current x and y coordinates.
            float targetX = transform.position.x;
            float targetY = transform.position.y;

            // If the player has moved beyond the x margin...
            if (CheckXMargin())
                // ... the target x coordinate should be a Lerp between the camera's current x position and the player's current x position.
                targetX = Mathf.Lerp(transform.position.x, ObjectToFollow.position.x, XSmooth * Time.deltaTime);

            // If the player has moved beyond the y margin...
            if (CheckYMargin())
                // ... the target y coordinate should be a Lerp between the camera's current y position and the player's current y position.
                targetY = Mathf.Lerp(transform.position.y, ObjectToFollow.position.y, YSmooth * Time.deltaTime);

            //// The target x and y coordinates should not be larger than the maximum or smaller than the minimum.
            targetX = Mathf.Clamp(targetX, MinXAndY.x, MaxXAndY.x);
            targetY = Mathf.Clamp(targetY, MinXAndY.y, MaxXAndY.y);

            // Set the camera's position to the target position with the same z component.
            transform.position = new Vector3(targetX, targetY, transform.position.z);
        }
    }
}
