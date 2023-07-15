using System.Collections;
using System.Collections.Generic;
using Systems.Spawner;
using TMPro;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI waveCounter;

    [SerializeField] private TextMeshProUGUI enemyCounter;

    [SerializeField] SpawnManager spawnManager;

    private void Start()
    {
        if (spawnManager == null)
            return;


        spawnManager.OnEnemyDeath.AddListener(UpdateUI);
        spawnManager.OnWaveEnd.AddListener(UpdateUI);


        UpdateUI();

    }
    public void UpdateUI() {
        waveCounter.text = spawnManager.GetCurrentWave().ToString();
        enemyCounter.text = spawnManager.GetCurrentEnemyCount().ToString();

    }
}
