using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Vector3 _rotate;

    IEnumerator Start()
    {
        var rigidbody = GetComponent<Rigidbody>();
        while (true)
        {
            yield return new WaitForFixedUpdate();
            Vector3 rotationStep = _rotate * Time.fixedDeltaTime;
            Quaternion deltaRotation = Quaternion.Euler(rotationStep);
            Quaternion nextRotation = rigidbody.rotation * deltaRotation;
            rigidbody.MoveRotation(nextRotation);
            yield return null;            
        }
    }

   
}
