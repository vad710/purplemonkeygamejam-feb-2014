using UnityEngine;

namespace Assets
{
    public class PlayerController : MonoBehaviour
    {
        private const string HorizontalAxis = "Horizontal";
        private const string DirectionAnimationParameter = "Direction";
        private const string IsMovingAnimationParameter = "IsMoving";


        private Animator _animator;

        // Use this for initialization
        public void Start ()
        {
            _animator = this.GetComponent<Animator>();
        }
	
        // Update is called once per frame
        public void Update ()
        {
            //var vertical = Input.GetAxis("Vertical");
            var horizontal = Input.GetAxis(HorizontalAxis);

            Debug.Log(horizontal);

            if (horizontal > 0)
            {
                _animator.SetInteger(DirectionAnimationParameter, 1);
                _animator.SetBool(IsMovingAnimationParameter, true);
            }
            else if (horizontal < 0)
            {
                _animator.SetInteger(DirectionAnimationParameter, 0);
                _animator.SetBool(IsMovingAnimationParameter, true);
            }
            else
            {
                _animator.SetBool(IsMovingAnimationParameter, false);
            }


        }
    }
}
