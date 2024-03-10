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

    private float m_MoveVector;


    private float m_SmoothMovementCurrentVelocity;

    // Start is called before the first frame update
    void Start()
    {
        MainPlayerInput input = new();
        input.Player.AddCallbacks(this);
        input.Player.Enable();
    }

    private void FixedUpdate()
    {
        if (hpController.Hp <= 0)
        {
            m_Rigidbody2D.velocity = Vector2.zero;
            m_Animator.SetBool("Move", false);
            m_Animator.SetBool("MoveBack", false);
        }
        else
        {
            float smoothDamp = Mathf.SmoothDamp(m_Rigidbody2D.velocity.x, m_MoveVector * m_MoveSpeed, ref m_SmoothMovementCurrentVelocity, m_SmoothTime);
            m_Rigidbody2D.velocity = new Vector2(smoothDamp, m_Rigidbody2D.velocity.y);
        }
    }


    public void PlayDeathAnimation()
    {
        m_Animator.SetTrigger("Death");
    }

    private void OnValidate()
    {
        if (!m_Rigidbody2D) m_Rigidbody2D = GetComponent<Rigidbody2D>();
        if (!m_Animator) m_Animator = GetComponent<Animator>();
        if (!combatStateController) combatStateController = GetComponent<CombatStateController>();
        if (!hpController) hpController = GetComponent<HpController>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (hpController.Hp > 0)
        {
            m_MoveVector = context.ReadValue<float>();

            switch (m_MoveVector)
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