using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimHandler : MonoBehaviour
{
    [SerializeField] private float _moveX = 0f;
    private const string _xVelocity = "xVelocity";
    [SerializeField] private Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void LateUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (h != 0 || v != 0)
        {
            _moveX = Mathf.MoveTowards(_moveX, 1, Time.deltaTime);
        }

        else
        {
            _moveX = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _anim.SetFloat(_xVelocity, _moveX);
    }
}
