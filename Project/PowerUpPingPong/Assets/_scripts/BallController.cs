using UnityEngine;

//This attribute enforces a component as a dependency, like transform
[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    /* this is my preferred coding style, private variables have "_"
     * Public variables start with an Uppercase letter
     * inline variables are lowercase */
    private Rigidbody _rb;
    [SerializeField, Range(1, 1000), Tooltip("This is an attribute, helpful for understanding what a field does, or Units")]
    // 10 here, is the default value, if you hit "reset" on the Component accidentally, this value comes back.
    private float _moveSpeed = 10.0f, _affordanceAngle = 10.0f;
    [SerializeField] bool _startOnAwake = false, _lastBounceValid;
    private Vector3 _bounceDirection = Vector3.zero;
    [SerializeField] private float _reflectAngle, _offset;
    [SerializeField] private Transform _player;
    //Awake is called before start, to get components
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        ///*Far more optimized. Why? (Try Catch, Happy Case, No MemAlloc, shorter, DRY) Vs itself will suggest
        // * some changes, some of them are good. VS 2022 learns a style, more you code.
        // * Intellisense should always do the hard work, not the programmer. */
        ////Ctrl + K, pops options for VS. Ctrl + K and Ctrl + U to uncomment, same and Ctrl + C to comment whatever
        ////is selected. such as this block of code. can do a Collapse, as well, with the drop down arrow on the left.
        //if (TryGetComponent(out _rb))
        //{
        //    Debug.Log("Rb bound successfully");
        //}
    }

    /* Start is called before the first frame update, but AFTER awake.
     * this is a multiline commment, useful for formatting and
     * quickly blocking out "blocks of code. done with "/*" and the
     * opposite to close out the multiline
     */

    void Start()
    {
        //This is called as a Dependency Check, the attribute falls more under
        //Dependency Injection. Part of Programmer's Design principles.
        //S.O.L.I.D ,  the D is Dependency Injection. Still, not DRY.

        if (_startOnAwake)
        {
            if (_rb)
            {
                Debug.Log("Rigidbody bound");
            }

            _rb.AddForce(_moveSpeed * 10.0f * Vector3.down, ForceMode.Impulse);
        }
    }

    private void Update()
    {
        //find offset direction from player
        Vector3 offsetDir = new(_player.position.x + _offset, 0, _player.position.z + _offset);
        _bounceDirection = offsetDir - transform.position;
        
        //Debud lines for Direction
        Debug.DrawRay(transform.position, _bounceDirection.normalized * 10.0f, Color.magenta);

        //Check valid bounce
        if (_reflectAngle < _affordanceAngle)
        {
            _lastBounceValid = false;
        }

        else
        {
            _lastBounceValid = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        _reflectAngle = Vector3.Angle(_rb.velocity.normalized, collision.GetContact(0).normal);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!_lastBounceValid)
        {
            _rb.AddForce(_bounceDirection.normalized * _moveSpeed, ForceMode.Impulse);
        }
    }
}
