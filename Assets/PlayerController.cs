using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_Rigidbody2D;
    [SerializeField] private Animator m_Aminator;

    [SerializeField] private float m_MoveSpeed;
    [SerializeField] private float m_SmoothTime;

    private Vector2 m_MoveVector;

    [SerializeField] private Transform m_SwordMagnitudeClamp;
    [SerializeField] private float m_SwordMoveSens;

    [SerializeField] private float m_SwordRotationSpeed;

    private Vector2 m_SwordTargetPosition;
    [SerializeField] private Transform m_SwordTransform;

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

        m_Rigidbody2D.velocity = new Vector2(smoothDamp, m_Rigidbody2D.velocity.y);

        // m_SwordTransform.position = ConvertScreenToPlanePoint(m_SwordTargetPosition);
    }

    public Vector3 ConvertScreenToPlanePoint(Vector2 screenPoint)
    {
        Plane plane = new(Vector3.forward, Vector3.zero);

        Ray ray = Camera.main.ScreenPointToRay(screenPoint);

        if (plane.Raycast(ray, out float enter))
        {
            Vector3 intersection = ray.GetPoint(enter);
            return intersection;
        }

        return Vector3.zero;
    }

    public void OnMove(InputValue value)
    {
        m_MoveVector = value.Get<Vector2>();

        switch (m_MoveVector.x)
        {
            case 0:
                m_Aminator.SetBool("Move", false);
                m_Aminator.SetBool("MoveBack", false);
                break;
            case > 0:
                m_Aminator.SetBool("Move", true);
                m_Aminator.SetBool("MoveBack", false);
                break;
            case < 0:
                m_Aminator.SetBool("Move", false);
                m_Aminator.SetBool("MoveBack", true);
                break;
        }
    }

    public void OnSwordMovement(InputValue value)
    {
        m_SwordTargetPosition = value.Get<Vector2>() * m_SwordMoveSens;

        m_SwordTransform.position += new Vector3(m_SwordTargetPosition.x, m_SwordTargetPosition.y, 0);

        Vector3 toObject = m_SwordTransform.position - m_SwordMagnitudeClamp.position;

        const float radius = 2f;
        if (toObject.magnitude > radius)
        {
            toObject = toObject.normalized * radius; // Normalize toObject vector and scale to maxDistance
            m_SwordTransform.position = m_SwordMagnitudeClamp.position + toObject; // Update position
        }
    }

    public void OnSwordRotation(InputValue value)
    {
        float v = value.Get<float>() * m_SwordRotationSpeed;
        m_SwordTransform.Rotate(new Vector3(0, 0, v));
    }

    private void OnValidate()
    {
        if (!m_Rigidbody2D) m_Rigidbody2D = GetComponent<Rigidbody2D>();
        if (!m_Aminator) m_Aminator = GetComponent<Animator>();
    }
}