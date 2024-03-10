using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int m_NumberOfEnemies;
    [SerializeField] private EnemyController m_EnemyPrefab;

    [SerializeField] private TextMeshProUGUI m_CanvasNumber;

    private void Start()
    {
        SpawnNextEnemy();
    }

    private void SpawnNextEnemy()
    {
        m_CanvasNumber.text = m_NumberOfEnemies.ToString();

        if (m_NumberOfEnemies > 0)
        {
            EnemyController enemy = Instantiate(m_EnemyPrefab, transform.position, Quaternion.identity);
            enemy.hpController.OnDeath.AddListener(SpawnNextEnemy);
            m_NumberOfEnemies--;
        }

        if (m_NumberOfEnemies == 0)
        {
            Debug.Log("No more enemies");
        }
    }
}