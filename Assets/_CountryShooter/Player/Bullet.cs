using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  private Transform thisTransform;

  void Start()
  {
    thisTransform = transform;
  }

  void Update()
  {
    // Stop tracking the bullet after it gets far away
    if (thisTransform.position.y < -10)
    {
      gameObject.SetActive(false);
    }
  }
}
