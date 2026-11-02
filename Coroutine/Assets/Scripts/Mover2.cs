using System.Collections;
using UnityEngine;

public class Mover2 : MonoBehaviour
{
    [SerializeField]
    private Vector3 _start;

    [SerializeField]
    private Vector3 _end;

    [SerializeField]
    private float _speed = 2f;

    [SerializeField]
    private float _delay = 1f;

    private IEnumerator Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        Vector3 target = _end;

        while (true)
        {
            float duration = Vector3.Distance(_start, target) / _speed;
            float time = 0f;

            while (time < duration)
            {
                time += Time.fixedDeltaTime;
                rb.MovePosition(Vector3.Lerp(_start, target, time / duration));
                yield return new WaitForFixedUpdate();
            }

            yield return new WaitForSeconds(_delay);

            if (target == _end)
            {
                target = _start;
            }
        }
    }
}
