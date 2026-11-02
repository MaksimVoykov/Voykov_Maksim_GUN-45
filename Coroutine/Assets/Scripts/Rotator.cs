using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    private Vector3 _rotate;

    private IEnumerator Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        while (true)
        {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(_rotate * Time.deltaTime));
            yield return null;
        }
    }
}