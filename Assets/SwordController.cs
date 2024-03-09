using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SwordController : MonoBehaviour
{
    public CombatStateController combatState;

    private void OnValidate()
    {
        if (!combatState) combatState = GetComponentInParent<CombatStateController>();
    }
}
