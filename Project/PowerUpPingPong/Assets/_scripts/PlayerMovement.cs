using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform _visual;
    [SerializeField] float _rotationSensitivity = 10.0f;
    [SerializeField] bool _smoothRotate = false;
    [SerializeField] private CinemachineVirtualCamera _followCam;
    [SerializeField] private float _moveSpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float mouseX = Input.GetAxis("Mouse X");
        Vector3 move = new Vector3(h, 0, v);

        if (h != 0 || v != 0)
        {
            if (_followCam)
            {
                _followCam.Priority = 17;
            }

            if (_visual)
            {
                if (_smoothRotate)
                {
                    //character rotation
                    _visual.forward = Vector3.Lerp(_visual.forward, move, _rotationSensitivity * Time.deltaTime);
                }

                else
                {
                    float angle = mouseX * 90.0f * Time.deltaTime;
                    _visual.Rotate(Vector3.up, angle);
                }
            }

            //movement
            transform.Translate(move * _moveSpeed * Time.deltaTime);
            transform.forward = move;
        }

        else
        {
            if (_followCam)
            {
                _followCam.Priority = 15;
            }
        }
    }
}
