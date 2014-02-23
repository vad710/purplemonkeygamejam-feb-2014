using UnityEngine;

namespace Assets
{
    public class Shoot : MonoBehaviour
    {

        private PlayerController _playerController;


        // Use this for initialization
        void Start () 
        {
            _playerController = transform.root.GetComponent<PlayerController>();
        }
	
        // Update is called once per frame
        void Update () 
        {
	        
        }
    }
}
