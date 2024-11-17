using System;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * Pressing an arrow key is equivalent to choosing a direction on the unit circle:
 * --- up arrow ---> (0, 1)
 * --- down arrow ---> (0, -1)
 * --- left arrow ---> (-1, 0)
 * --- right arrow ---> (1, 0)
 * 
 * Pressing two arrows at the same also gives us a direction, for example:
 * --- down + left arrow ---> (-sqrt(2)/2, -sqrt(2)/2)
 * 
 * We can extract these values in the event handler of the appropriate event (OnMove in this case)
 * and use them as a velocity of our ball 
 * but since the values are normalized they actually allow us to specify the direction of movement.
 * 
 * The default values, i.e. when nothing is pressed, are zeros.
 * 
 * As soon as we stop pressing, the values are reset to the default ones, i.e. to zeros (which is logical).
 */

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private Vector3 _velocity;

    [SerializeField]
    private float _speed;

    private int _collectedPickUps;
    public event EventHandler<OnCollectPickUpArgs> OnCollectPickUp;

    private int _totalPickUpsToCollect;
    public event Action OnAllPickUpsCollected;

    public event Action OnTouchEnemy;

    public event Action OnFalling;

    [SerializeField]
    private float _jumpForce;

    [SerializeField]
    private float _detectionSphereRadius;

    [SerializeField]
    private Transform _jumpableSurfaceChecker;

    [SerializeField]
    private LayerMask _jumpable;

    private bool _isGrounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _velocity = new Vector3();
        _speed = 10.0f;
        _collectedPickUps = 0;
        _totalPickUpsToCollect = GameObject.FindGameObjectsWithTag("PickUp").Length;
        _jumpForce = 4.0f;
        _detectionSphereRadius = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        _jumpableSurfaceChecker.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);

        if (transform.position.y < -5.0f)
        {
            OnFalling?.Invoke();
        }
    }

    void FixedUpdate()
    {
        _rb.AddForce(_velocity * _speed);
    }

    void OnMove(InputValue movementDirection)
    {
        Vector2 movementDirectionVector2 = movementDirection.Get<Vector2>();
        _velocity.Set(movementDirectionVector2.x, 0.0f, movementDirectionVector2.y);
    }

    void OnJump()
    {
        _isGrounded = Physics.CheckSphere(_jumpableSurfaceChecker.position, _detectionSphereRadius, _jumpable);

        if (_isGrounded )
        {
            _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, 0.0f, _rb.linearVelocity.z);
            _rb.AddForce(_jumpForce * Vector3.up, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("PickUp"))
        {
            _collectedPickUps++;
            OnCollectPickUp?.Invoke(this, new OnCollectPickUpArgs(_collectedPickUps));

            if (_collectedPickUps ==  _totalPickUpsToCollect)
            {
                OnAllPickUpsCollected?.Invoke();
                _rb.constraints = RigidbodyConstraints.FreezeAll;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            OnTouchEnemy?.Invoke();
            gameObject.SetActive(false);
        }
    }
}

public class OnCollectPickUpArgs : EventArgs
{
    public int collectedPickUps;

    public OnCollectPickUpArgs(int collectedPickUps)
    {
        this.collectedPickUps = collectedPickUps;
    }
}