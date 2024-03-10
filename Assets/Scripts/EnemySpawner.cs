using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int m_NumberOfEnemies;
    [SerializeField] private EnemyController m_EnemyPrefab;

    [SerializeField] private TextMeshProUGUI m_CanvasNumber;
    
    [SerializeField] private Animation m_WinScreen;

    private void Start()
    {
        SpawnNextEnemy();
    }

    private void SpawnNextEnemy()
    {
        m_NumberOfEnemies--;
        m_CanvasNumber.text = m_NumberOfEnemies.ToString();

        if (m_NumberOfEnemies > 0)
        {
            EnemyController enemy = Instantiate(m_EnemyPrefab, transform.position, Quaternion.identity);
            enemy.hpController.OnDeath.AddListener(SpawnNextEnemy);
        }

        if (m_NumberOfEnemies == 0)
        {
            Debug.Log("No more enemies");
            m_WinScreen.Play();
        }
    }
}