using UnityEngine;
using System.Collections;

public class CursorScript : MonoBehaviour
{
    [SerializeField] private bool m_CursorVisible;
    
    // Use this for initialization
    void Start()
    {
        Cursor.visible = m_CursorVisible;
    }
}