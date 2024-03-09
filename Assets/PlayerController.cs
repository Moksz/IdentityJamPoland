using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_Rigidbody2D;
    [SerializeField] private Animator m_Aminator;

    [SerializeField] private float m_MoveSpeed = 1;
    [SerializeField] private float m_SmoothTime = 0.1f;

    private Vector2 m_MoveVector;

    // private Vector2 m_SmoothMovement;
    private float m_SmoothMovementCurrentVelocity;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        // float x = m_MoveVector.x * m_MoveSpeed * Time.deltaTime;
        float smoothDamp = Mathf.SmoothDamp(m_Rigidbody2D.velocity.x, m_MoveVector.x * m_MoveSpeed, ref m_SmoothMovementCurrentVelocity, m_SmoothTime);
        m_Aminator.SetFloat("Move", smoothDamp);
        m_Rigidbody2D.velocity = new Vector2(smoothDamp, m_Rigidbody2D.velocity.y);
    }

    public void OnMove(InputValue value)
    {
        m_MoveVector = value.Get<Vector2>();
    }

    private void OnValidate()
    {
        if (!m_Rigidbody2D) m_Rigidbody2D = GetComponent<Rigidbody2D>();
        if (!m_Aminator) m_Aminator = GetComponent<Animator>();
    }
}