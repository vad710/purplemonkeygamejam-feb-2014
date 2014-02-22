using UnityEngine;

namespace Assets
{
    public class PlayerController : MonoBehaviour
    {
        private const string HorizontalAxis = "Horizontal";
        private const string DirectionAnimationParameter = "Direction";
        private const string IsMovingAnimationParameter = "IsMoving";


        private Animator _animator;
        private Transform _transform;

        private int _previousDirection = 1;

        // Use this for initialization
        public void Start ()
        {
            _animator = this.GetComponent<Animator>();
            _transform = this.GetComponent<Transform>();
        }
	
        // Update is called once per frame
        public void Update ()
        {
            //var vertical = Input.GetAxis("Vertical");
            var horizontal = Input.GetAxis(HorizontalAxis);


            int direction;
            bool isMoving;

            if (horizontal > 0)
            {
                direction = 1;
                isMoving = true;
            }
            else if (horizontal < 0)
            {
                direction = 0;
                isMoving = true;
            }
            else
            {
                isMoving = false;
                direction = _previousDirection;
            }

            _animator.SetInteger(DirectionAnimationParameter, direction);
            _animator.SetBool(IsMovingAnimationParameter, isMoving);

            if (_previousDirection != direction)
            {
                Flip();
                _previousDirection = direction;
            }

            
        }

        private void Flip()
        {
            var theScale = _transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
