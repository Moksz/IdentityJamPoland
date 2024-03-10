using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HpController : MonoBehaviour
{
    [SerializeField] private int m_MaxHp;
    private int m_Hp;

    public int Hp
    {
        get => m_Hp;
        set
        {
            if (value < m_Hp)
            {
                m_OnDamage.Invoke();
            }
            
            if (value <= 0 && m_Hp > 0)
            {
                m_OnDeath.Invoke();
            }
            
            m_Hp = value;
            if (m_HpBar)
            {
                m_HpBar.fillAmount = (float)m_Hp / m_MaxHp;
            }
        }
    }

    [SerializeField] private Image m_HpBar;

    [SerializeField] private UnityEvent m_OnDeath;
    [SerializeField] private UnityEvent m_OnDamage;

    // Start is called before the first frame update
    void Start()
    {
        m_Hp = m_MaxHp;
    }
}