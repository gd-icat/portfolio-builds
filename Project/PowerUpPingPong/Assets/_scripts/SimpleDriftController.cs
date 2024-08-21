using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControlType
{
    transform,
    physics
}
public class SimpleDriftController : MonoBehaviour
{
    [SerializeField] private Vector3 _moveForce;
    [SerializeField] private float _speed, _maxSpeed, _turnAngle, _traction;
    [SerializeField] private bool _drifting;
    [SerializeField] private ParticleSystem[] _smokeFX = new ParticleSystem[2];
    //value less than one slowly decreases move vector
    private const float _drag = 0.98f;
    [SerializeField] private ControlType _control = ControlType.transform;
    private Rigidbody _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        StopVFX();
    }
    private void FixedUpdate()
    {
        switch(_control)
        {
            case ControlType.transform:
                DriveSimple();
                break;
            case ControlType.physics:
                DrivePhysics();
                break;
        }
    }

    private void DriveSimple()
    {
        //player input
        float steering = Input.GetAxis("Horizontal");
        float throttle = Input.GetAxis("Vertical");

        //Movement using forward of transform
        _moveForce += _speed * throttle * transform.forward * Time.deltaTime;
        transform.position += _moveForce * Time.deltaTime;

        //Steering using vertical so car deosn't turn when idle
        transform.Rotate(new Vector3(0, _turnAngle * Time.deltaTime * steering * throttle, 0));

        //drag
        _moveForce *= _drag;

        //Speed Limit
        _moveForce = Vector3.ClampMagnitude(_moveForce, _maxSpeed);

        //Debug directions
        Debug.DrawRay(transform.position, _moveForce.normalized * 5.0f, Color.cyan);
        Debug.DrawRay(transform.position, transform.forward * 5.0f, Color.red);

        //Grip & drifting
        _moveForce = Vector3.Slerp(_moveForce, transform.forward * throttle, _traction * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _drifting = !_drifting;
        }

        if (_drifting)
        {
            _traction = Mathf.MoveTowards(_traction, 0.5f, 10.0f * Time.deltaTime);
        }

        else
        {
            _traction = Mathf.MoveTowards(_traction, 5.0f, Time.deltaTime);
        }

        ChangeVFX(_drifting);
    }

    private void DrivePhysics()
    {
        //player input
        float steering = Input.GetAxis("Horizontal");
        float throttle = Input.GetAxis("Vertical");

        //Movement using forward of transform
        _moveForce += _speed * throttle * transform.forward * Time.fixedDeltaTime;
        _rb.velocity += _moveForce * Time.fixedDeltaTime;

        //Steering using vertical so car deosn't turn when idle
        transform.Rotate(new Vector3(0, _turnAngle * Time.fixedDeltaTime * steering * throttle, 0));

        //drag
        _rb.drag = (1 - _drag);

        //Speed Limit
        _moveForce = Vector3.ClampMagnitude(_moveForce, _maxSpeed);

        //Debug directions
        Debug.DrawRay(transform.position, _moveForce.normalized * 5.0f, Color.cyan);
        Debug.DrawRay(transform.position, transform.forward * 5.0f, Color.red);

        //Grip & drifting
        _moveForce = Vector3.Slerp(_moveForce, transform.forward * throttle, _traction * Time.fixedDeltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _drifting = !_drifting;
        }

        if (_drifting)
        {
            _traction = Mathf.MoveTowards(_traction, 1.0f, 10.0f * Time.fixedDeltaTime);
        }

        else
        {
            _traction = Mathf.MoveTowards(_traction, 10.0f, Time.fixedDeltaTime);
        }

        ChangeVFX(_drifting);
    }

    private void ChangeVFX(bool active)
    {
        foreach (ParticleSystem p  in _smokeFX) 
        {
            if (active)
            {
                p.Play();
            }

            else
            {
                p.Pause();
            }
        }
    }

    private void StopVFX()
    {
        foreach (ParticleSystem p in _smokeFX)
        {
            p.Stop();
        }
    }
}
