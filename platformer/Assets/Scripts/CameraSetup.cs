using UnityEngine;

namespace Assets
{
    public class CameraSetup : MonoBehaviour {

        private const float PixelsPerUnit = 100.0f; //This can be PixelsPerUnit, or you can change it during runtime to alter the camera.
        private const float Scale = 1f;


        public void Start()
        {

            this.camera.orthographicSize = Screen.height * gameObject.camera.rect.height / (PixelsPerUnit * Scale) / 2.0f;//- 0.1f;

        }

    }
}
