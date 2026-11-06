using System.Collections;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Vector3 _start;
    [SerializeField] private Vector3 _end;
    [SerializeField] private float _speed;
    [SerializeField] private float _delay;
    private Vector3 _currentTarget;
    private Rigidbody _rigidbody;



    private IEnumerator Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.position = _start;
        _currentTarget = _end;

        while (true)
        {
            yield return StartCoroutine(MovePosition(_currentTarget));
            yield return new WaitForSeconds(_delay);
            _currentTarget = (_currentTarget == _end) ? _start : _end;
        }
    }

    private IEnumerator MovePosition(Vector3 target)
    {
        while (Vector3.SqrMagnitude(_rigidbody.position - _currentTarget) > 0.001f)
        {
            Vector3 nextPosition = Vector3.MoveTowards(_rigidbody.position, target, _speed * Time.fixedDeltaTime);
            _rigidbody.MovePosition(nextPosition);
            yield return new WaitForFixedUpdate();
        }
        _rigidbody.MovePosition(target);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_start, 1f);
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_end, 1f);
        
    }

}
