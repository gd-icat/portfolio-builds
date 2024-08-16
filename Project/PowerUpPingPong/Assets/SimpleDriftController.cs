using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDriftController : MonoBehaviour
{
    [SerializeField] private Vector3 _moveForce;
    [SerializeField] private float _speed, _maxSpeed, _turnAngle, _traction;
    [SerializeField] private bool _drifting;
    //value less than one slowly decreases move vector
    private const float _drag = 0.98f;
    private void Update()
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
        _moveForce = Vector3.Slerp(_moveForce , transform.forward * throttle, _traction * Time.deltaTime);

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
            _traction = Mathf.MoveTowards(_traction, 5.0f, 10.0f * Time.deltaTime);
        }
    }
}
