using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    private List<SpawnPoint> _spawnPoints = new List<SpawnPoint>();

    private void Start()
    {
        _spawnPoints.AddRange(GetComponentsInChildren<SpawnPoint>());
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(2f);

        yield return waitForSeconds;

        while (_spawnPoints.Count != 0)
        {
            for (int i = 0; i < _spawnPoints.Count; i++)
            {
                if (_spawnPoints[i].CanInstantiateEnemy() == false)
                {
                    _spawnPoints.Remove(_spawnPoints[i]);
                    i--;

                    yield return null;
                }
                else
                {
                    yield return waitForSeconds;
                }
            }
        }

        yield return null;
    }
}
