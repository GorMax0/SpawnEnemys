using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimator))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private PatrolPointsGroup _path;
    [SerializeField] private float _speed;

    private Transform[] _patrolPoints;
    private EnemyAnimator _enemyAnimator;

    private void Awake()
    {
        _enemyAnimator = GetComponent<EnemyAnimator>();
    }

    private void Start()
    {
        _patrolPoints = CreatePatrolPoints();
        StartCoroutine(MovementToPoint());
    }

    private Transform[] CreatePatrolPoints()
    {
        Transform[] patrolPoints = new Transform[_path.transform.childCount];

        for (int i = 0; i < patrolPoints.Length; i++)
        {
            patrolPoints[i] = _path.transform.GetChild(i);
        }

        return patrolPoints;
    }

    private IEnumerator MovementToPoint()
    {
        float distanceToPoint = 0.3f;
        int indexPoint = Random.Range(0, _patrolPoints.Length);
        WaitForSeconds waitForSeconds = new WaitForSeconds(3f);
        WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

        while (true)
        {
            _enemyAnimator.Walk(true);
            transform.position = Vector3.MoveTowards(transform.position, _patrolPoints[indexPoint].position, _speed * Time.fixedDeltaTime);

            var direction = _patrolPoints[indexPoint].position - transform.position;
            transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 1, 0);

            if (Vector3.Distance(transform.position, _patrolPoints[indexPoint].position) <= distanceToPoint)
            {
                _enemyAnimator.Walk(false);
                indexPoint = Random.Range(0, _patrolPoints.Length);
                yield return waitForSeconds;
            }

            yield return waitForFixedUpdate;
        }
    }
}
