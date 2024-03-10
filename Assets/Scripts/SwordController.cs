using System;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    // public CombatStateController combatStateController;


    public GameObject enemyCharacter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.layer == LayerMask.NameToLayer("PlayerSword"))
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                enemyCharacter = other.gameObject;
            }
        }
        else if (gameObject.layer == LayerMask.NameToLayer("EnemySword"))
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                enemyCharacter = other.gameObject;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (gameObject.layer == LayerMask.NameToLayer("PlayerSword"))
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                enemyCharacter = null;
            }
        }
        else if (gameObject.layer == LayerMask.NameToLayer("EnemySword"))
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                enemyCharacter = null;
            }
        }
    }

    // private void OnValidate()
    // {
    //     if (!combatStateController) combatStateController = GetComponentInParent<CombatStateController>();
    // }
}