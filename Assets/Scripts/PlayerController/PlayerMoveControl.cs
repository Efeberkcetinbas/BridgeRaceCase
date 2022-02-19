using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveControl : MonoBehaviour,IAnimationController
{
    public Animator animator { get; set; }

    [SerializeField] private float _speed;

    private Rigidbody _rb;

    protected Joystick joystick;

    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Joy_Move();
    }

    void Joy_Move()
    {
        _rb.velocity = new Vector3(joystick.Horizontal * _speed, _rb.velocity.y, joystick.Vertical * _speed);

        if(joystick.Horizontal!=0 || joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(_rb.velocity);
            Running_Anim();
        }
        else
        {
            Idle_Anim();
        }
    }


   
    public void Idle_Anim()
    {
        if (animator.GetBool("running"))
        {
            animator.SetBool("running", false);
        }
    }

    public void Running_Anim()
    {
        if (!animator.GetBool("running"))
        {
            animator.SetBool("running", true);
        }
    }
}
