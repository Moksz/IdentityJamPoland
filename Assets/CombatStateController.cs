using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStateController : MonoBehaviour
{
    [NonSerialized] public CombatState combatState;
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