using Assets.Scripts;
using UnityEngine;

namespace Assets
{
    public class BriefCase : MonoBehaviour
    {

        public PlayerController TriggerObject;
        public CameraFollow Camera;
        private bool _triggered;
        public GameObject AllEnemies;


        public void OnTriggerEnter2D(Collider2D other)
        {
            if (!_triggered && other == TriggerObject.collider2D)
            {
                Debug.Log("BriefCase Triggered!");
                _triggered = true;

                if (AllEnemies != null)
                {
                    var enemies = AllEnemies.GetComponentsInChildren<Biker>();

                    foreach (var enemy in enemies)
                    {
                        Destroy(enemy.gameObject);
                    }
                }
                


                if (Camera != null)
                {
                    TriggerObject.HasBriefcase = true;

                    Camera.ReturnTrigger();
                    Destroy(this.gameObject);
                }

                
            }
        }
    }
}
