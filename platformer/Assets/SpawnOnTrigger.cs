using UnityEngine;

namespace Assets
{
    public class SpawnOnTrigger : MonoBehaviour
    {
        public Transform PrefabToSpawn;
        public Transform TriggerObject;
        public Vector3 SpawnPosition;

        private bool _triggered;

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (PrefabToSpawn == null || _triggered)
            {
                return;
            }

            if (other == TriggerObject.collider2D)
            {
                Instantiate(PrefabToSpawn, SpawnPosition, Quaternion.Euler(Vector3.zero));
                _triggered = true;
            }
        }
    }
}
