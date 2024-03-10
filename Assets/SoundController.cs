using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource m_Footstep;
    [SerializeField] private AudioSource m_Defence;
    [SerializeField] private AudioSource m_Attack;
    [SerializeField] private AudioSource m_Hit;
    [SerializeField] private AudioSource m_Death;

    public void PlayFootstep()
    {
        m_Footstep.Play();
    }
    
    public void PlayDefence()
    {
        m_Defence.Play();
    }
    
    public void PlayAttack()
    {
        m_Attack.Play();
    }
    
    public void PlayHit()
    {
        m_Hit.Play();
    }
    
    public void PlayDeath()
    {
        m_Death.Play();
    }
}
