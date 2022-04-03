using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class PointSpawn : MonoBehaviour
{
    [SerializeField] private int _maxCountSpawn;
    [SerializeField] private Enemy _enemy;

    private int _currentCountSpawn = 1;
    private ParticleSystem _effect;

    private void Awake()
    {
        _effect = GetComponent<ParticleSystem>();
    }

    public bool CanInstantiateEnemy()
    {
        bool isLastSpawn = _currentCountSpawn == _maxCountSpawn;

        if (_currentCountSpawn <= _maxCountSpawn)
        {
            Instantiate(_enemy, transform.position, transform.rotation);
            _currentCountSpawn++;

            if (isLastSpawn)
            {
                _effect.Stop();
            }

            return true;
        }
        else
        {
            return false;
        }
    }
}
