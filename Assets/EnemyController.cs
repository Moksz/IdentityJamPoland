using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_Rigidbody2D;
    [SerializeField] private Animator m_Animator;

    [SerializeField] private float m_MoveSpeed;
    [SerializeField] private float m_StoppingDistance;

    private PlayerController m_PlayerController;

    [SerializeField] private float attackIntervalLow;
    [SerializeField] private float attackIntervalHigh;

    private float m_SmoothMovementCurrentVelocity;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerController = FindFirstObjectByType<PlayerController>();

        StartCoroutine(ActionCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        float moveVector;
        if (Vector3.Distance(m_PlayerController.transform.position, transform.position) > m_StoppingDistance)
        {
            m_Animator.SetBool("Move", true);
            moveVector = -1;
        }
        else
        {
            m_Animator.SetBool("Move", false);
            moveVector = 0;
        }

        float smoothDamp = Mathf.SmoothDamp(m_Rigidbody2D.velocity.x, moveVector * m_MoveSpeed, ref m_SmoothMovementCurrentVelocity, 0.1f);

        m_Rigidbody2D.velocity = new Vector2(smoothDamp, m_Rigidbody2D.velocity.y);
    }

    private IEnumerator ActionCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(attackIntervalLow, attackIntervalHigh));
        while (true)
        {
            yield return new WaitUntil(() => Vector3.Distance(m_PlayerController.transform.position, transform.position) < m_StoppingDistance + 2);

            string attackType;
            if (Random.value > 0.5f)
            {
                attackType = "HighAttack";
            }
            else
            {
                attackType = "LowAttack";
            }

            m_Animator.SetBool(attackType, true);
            yield return new WaitForSeconds(Random.Range(0f, 1f));
            if (Vector3.Distance(m_PlayerController.transform.position, transform.position) < m_StoppingDistance)
            {
                m_Animator.SetBool(attackType, false);
            }

            yield return new WaitForSeconds(Random.Range(attackIntervalLow, attackIntervalHigh));
        }
    }

    private void OnValidate()
    {
        if (!m_Rigidbody2D) m_Rigidbody2D = GetComponent<Rigidbody2D>();
        if (!m_Animator) m_Animator = GetComponent<Animator>();
    }
}