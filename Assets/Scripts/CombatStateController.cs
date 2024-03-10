using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class CombatStateController : MonoBehaviour
{
    [NonSerialized] public CombatState combatState;

    // [NonSerialized] public bool attack;
    public SwordController swordController;

    public UnityEvent OnAttackBlocked;

    public void DealDamage()
    {
        if (gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (swordController.enemyCharacter)
            {
                EnemyController enemyController = swordController.enemyCharacter.GetComponentInParent<EnemyController>();
                // if (combatStateController.attack)
                // {
                if (combatState == CombatState.HighAttack && enemyController.combatStateController.combatState != CombatState.HighDefence ||
                    combatState == CombatState.LowAttack && enemyController.combatStateController.combatState != CombatState.LowDefence)
                {
                    enemyController.hpController.Hp -= 1;
                    Debug.Log("enemy damaged");
                }
                else
                {
                    Debug.Log("enemy blocked attack");
                    OnAttackBlocked.Invoke();
                }
                // }
            }
        }
        else if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (swordController.enemyCharacter)
            {
                PlayerController playerController = swordController.enemyCharacter.GetComponentInParent<PlayerController>();
                // if (combatStateController.attack)
                // {
                if (combatState == CombatState.HighAttack && playerController.combatStateController.combatState != CombatState.HighDefence ||
                    combatState == CombatState.LowAttack && playerController.combatStateController.combatState != CombatState.LowDefence)
                {
                    playerController.hpController.Hp -= 1;
                    Debug.Log("player damaged");
                }
                else
                {
                    Debug.Log("player blocked attack");
                    OnAttackBlocked.Invoke();
                }
                // }
            }
        }
    }

    public void DestroyGameObject()
    {
        if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Destroy(gameObject);
        }
    }

    private void OnValidate()
    {
        if (!swordController) swordController = GetComponentInChildren<SwordController>();
    }
}

[Serializable]
public enum CombatState : byte
{
    Idle,
    LowAttack,
    HighAttack,
    LowDefence,
    HighDefence
}