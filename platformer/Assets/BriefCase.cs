using Assets.Scripts;
using UnityEngine;

namespace Assets
{
    public class BriefCase : MonoBehaviour
    {

        public Collider2D TriggerObject;
        public CameraFollow Camera;
        private bool _triggered;

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (!_triggered && other == TriggerObject)
            {
                Debug.Log("BriefCase Triggered!");
                _triggered = true;

                if (Camera != null)
                {
                    Camera.ReturnTrigger();
                }

                
            }
        }
    }
}
