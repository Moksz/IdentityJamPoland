using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour, MainPlayerInput.IPlayerActions
{
    [SerializeField] private Rigidbody2D m_Rigidbody2D;
    [SerializeField] private Animator m_Animator;
    public HpController hpController;
    public CombatStateController combatStateController;

    [SerializeField] private float m_MoveSpeed;
    [SerializeField] private float m_SmoothTime;

    private Vector2 m_MoveVector;


    // [SerializeField] private Transform m_SwordMagnitudeClamp;
    // [SerializeField] private float m_SwordMoveSens;
    //
    // [SerializeField] private float m_SwordRotationSpeed;
    //
    // private Vector2 m_SwordTargetPosition;
    // [SerializeField] private Transform m_SwordTransform;

    // private Vector2 m_SmoothMovement;
    private float m_SmoothMovementCurrentVelocity;

    // Start is called before the first frame update
    void Start()
    {
        MainPlayerInput input = new();
        input.Player.AddCallbacks(this);
        input.Player.Enable();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (hpController.Hp <= 0)
        {
            m_Rigidbody2D.velocity = Vector2.zero;
        }
        else
        {
            float smoothDamp = Mathf.SmoothDamp(m_Rigidbody2D.velocity.x, m_MoveVector.x * m_MoveSpeed, ref m_SmoothMovementCurrentVelocity, m_SmoothTime);
            m_Rigidbody2D.velocity = new Vector2(smoothDamp, m_Rigidbody2D.velocity.y);
        }

        // float x = m_MoveVector.x * m_MoveSpeed * Time.deltaTime;
        // m_SwordTransform.position = ConvertScreenToPlanePoint(m_SwordTargetPosition);
    }

    // public Vector3 ConvertScreenToPlanePoint(Vector2 screenPoint)
    // {
    //     Plane plane = new(Vector3.forward, Vector3.zero);
    //
    //     Ray ray = Camera.main.ScreenPointToRay(screenPoint);
    //
    //     if (plane.Raycast(ray, out float enter))
    //     {
    //         Vector3 intersection = ray.GetPoint(enter);
    //         return intersection;
    //     }
    //
    //     return Vector3.zero;
    // }

    public void OnAttackHigh(InputValue value)
    {
        // Debug.Log(value.);
    }

    // public void OnSwordMovement(InputValue value)
    // {
    //     m_SwordTargetPosition = value.Get<Vector2>() * m_SwordMoveSens;
    //
    //     m_SwordTransform.position += new Vector3(m_SwordTargetPosition.x, m_SwordTargetPosition.y, 0);
    //
    //     Vector3 toObject = m_SwordTransform.position - m_SwordMagnitudeClamp.position;
    //
    //     const float radius = 2f;
    //     if (toObject.magnitude > radius)
    //     {
    //         toObject = toObject.normalized * radius; // Normalize toObject vector and scale to maxDistance
    //         m_SwordTransform.position = m_SwordMagnitudeClamp.position + toObject; // Update position
    //     }
    // }
    //
    // public void OnSwordRotation(InputValue value)
    // {
    //     float v = value.Get<float>() * m_SwordRotationSpeed;
    //     m_SwordTransform.Rotate(new Vector3(0, 0, v));
    // }

    private void OnValidate()
    {
        if (!m_Rigidbody2D) m_Rigidbody2D = GetComponent<Rigidbody2D>();
        if (!m_Animator) m_Animator = GetComponent<Animator>();
        if (!combatStateController) combatStateController = GetComponent<CombatStateController>();
        if (!hpController) hpController = GetComponent<HpController>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        m_MoveVector = context.ReadValue<Vector2>();

        switch (m_MoveVector.x)
        {
            case 0:
                m_Animator.SetBool("Move", false);
                m_Animator.SetBool("MoveBack", false);
                break;
            case > 0:
                m_Animator.SetBool("Move", true);
                m_Animator.SetBool("MoveBack", false);
                break;
            case < 0:
                m_Animator.SetBool("Move", false);
                m_Animator.SetBool("MoveBack", true);
                break;
        }
    }

    public void OnAttackHigh(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            m_Animator.SetBool("HighAttack", true);
        }
        else if (context.canceled)
        {
            m_Animator.SetBool("HighAttack", false);
        }
    }

    public void OnAttackLow(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            m_Animator.SetBool("LowAttack", true);
        }
        else if (context.canceled)
        {
            m_Animator.SetBool("LowAttack", false);
        }
    }

    public void OnDefendHigh(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            m_Animator.SetBool("HighDefence", true);
        }
        else if (context.canceled)
        {
            m_Animator.SetBool("HighDefence", false);
        }
    }

    public void OnDefendLow(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            m_Animator.SetBool("LowDefence", true);
        }
        else if (context.canceled)
        {
            m_Animator.SetBool("LowDefence", false);
        }
    }
}