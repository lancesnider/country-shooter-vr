using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  private Transform thisTransform;
  private Rigidbody thisRigidbody;

  void Start()
  {
    thisTransform = transform;
    thisRigidbody = GetComponent<Rigidbody>();
  }

  void Update()
  {
    thisTransform.rotation = Quaternion.LookRotation(thisRigidbody.velocity);
    // Stop tracking the bullet after it gets far away
    if (thisTransform.position.y < -10)
    {
      gameObject.SetActive(false);
    }
  }
}
