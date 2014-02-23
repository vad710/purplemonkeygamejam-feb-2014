using UnityEngine;

namespace Assets
{
    public class DialogTimer : MonoBehaviour
    {

        public Transform NextDialog;

        private const float DialogTime = 6f;
        private float _finishTime = 0f;

        // Use this for initialization
        public void Start ()
        {
            _finishTime = Time.time + DialogTime;
        }
	
        // Update is called once per frame
        public void Update () 
        {
            if (Time.time >= _finishTime)
            {
                if (NextDialog != null)
                {
                    Instantiate(NextDialog, transform.position, Quaternion.Euler(Vector3.zero));
                }

                Destroy(this.gameObject);
            }
        }
    }
}
