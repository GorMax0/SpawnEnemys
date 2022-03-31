using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private PatrolPointsGroup _path;
    [SerializeField] private float _speed;

    private Transform[] _patrolPoints;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _patrolPoints = CreatePatrolPoints();
        StartCoroutine(MovementToPoint());
    }

    private void FixedUpdate()
    {

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
        int indexPoint = Random.Range(0, _patrolPoints.Length);
        WaitForSeconds waitForSeconds = new WaitForSeconds(2f);

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _patrolPoints[indexPoint].position, _speed * Time.fixedDeltaTime);

            if (transform.position == _patrolPoints[indexPoint].position)
            {
                indexPoint = Random.Range(0, _patrolPoints.Length);
                yield return waitForSeconds;
            }

            yield return null;
        }
    }
}
