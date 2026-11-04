using System.Collections;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField]
    private Vector3 _start;

    [SerializeField]
    private Vector3 _end;

    [SerializeField]
    private float _speed = 1f;

    [SerializeField]
    private float _delay = 0f;

    private IEnumerator Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        Vector3 target = _end;

        while (true)
        {
            yield return Move(rb, _start, _end);
            yield return new WaitForSeconds(_delay);

            yield return Move(rb, _end, _start);
            yield return new WaitForSeconds(_delay);
        }
    }

    private IEnumerator Move(Rigidbody rb, Vector3 from, Vector3 to)
    {
        float duration = Vector3.Distance(from, to) / _speed;
        float time = 0f;

        while (time < duration)
        {
            time += Time.fixedDeltaTime;
            rb.MovePosition(Vector3.Lerp(from, to, time / duration));
             yield return new WaitForFixedUpdate();
        }
    }
}
