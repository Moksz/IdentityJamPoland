using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HpController : MonoBehaviour
{
    [SerializeField] private int m_MaxHp;
    private int m_Hp;

    [SerializeField] private Animation m_LoseScreen;


    public int Hp
    {
        get => m_Hp;
        set
        {
            if (value < m_Hp)
            {
                OnDamage.Invoke();
            }

            if (value <= 0 && m_Hp > 0)
            {
                OnDeath.Invoke();
                if (m_LoseScreen)
                {
                    m_LoseScreen.Play();
                }
            }

            m_Hp = value;
            if (m_HpBar)
            {
                m_HpBar.fillAmount = (float)m_Hp / m_MaxHp;
            }
        }
    }

    [SerializeField] private Image m_HpBar;

    [FormerlySerializedAs("m_OnDeath")] public UnityEvent OnDeath;
    [FormerlySerializedAs("m_OnDamage")] public UnityEvent OnDamage;

    // Start is called before the first frame update
    void Start()
    {
        m_Hp = m_MaxHp;
    }
}