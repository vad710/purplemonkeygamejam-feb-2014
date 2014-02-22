using UnityEngine;

namespace Assets
{
    public class PlayerController : MonoBehaviour
    {
        private const string HorizontalAxis = "Horizontal";
        private const string IsMovingAnimationParameter = "IsMoving";

        private const int MovingForward = 1;
        private const int MovingBackward = 0;

        private Animator _animator;
        private Transform _transform;

        private int _previousDirection = 1;
        private bool _runningAtFullSpeed;


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
            
            
            int direction;
            bool isMoving;

            var horizontal = GetHorizontalMovement();

            //Update Animation
            if (horizontal > 0f)
            {
                direction = MovingForward;
                isMoving = true;
            }
            else if (horizontal < 0f)
            {
                direction = MovingBackward;
                isMoving = true;
            }
            else
            {
                isMoving = false;
                direction = _previousDirection;
            }

            _animator.SetBool(IsMovingAnimationParameter, isMoving);

            if (_previousDirection != direction)
            {
                Flip();
                _previousDirection = direction;
            }

            //Update movement
            var position = _transform.position;
            position.x += (horizontal * 0.1f);
            _transform.position = position;

        }

        private float GetHorizontalMovement()
        {
            var horizontal = Input.GetAxis(HorizontalAxis);
            if (!_runningAtFullSpeed)
            {
                if (horizontal >= 1f || horizontal <= -1f)
                {
                    _runningAtFullSpeed = true;
                }
            }
            else
            {
                if (horizontal == 0f)
                {
                    //User reached a full stop, turn off flag
                    _runningAtFullSpeed = false;
                }
                else if ((horizontal >= 0 && horizontal < 1f) || (horizontal <= 0 && horizontal > -1f))
                {
                    //Already running at full speed, but slowing down - just force horizontal to be 0 for a full stop
                    horizontal = 0f;
                }
            }
            return horizontal;
        }

        private void Flip()
        {
            var theScale = _transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
