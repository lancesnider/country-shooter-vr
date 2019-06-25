using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{

  public float fireRate = 0.5f;
  private float nextFire = 0.0f;
  public int bulletForce = 200;
  public GameObject bullet;
  public Transform muzzle;
  // Start is called before the first frame update
  void Fire()
  {
    GameObject bulletClone = Instantiate(bullet, muzzle.position, muzzle.rotation) as GameObject;
    bulletClone.GetComponent<Rigidbody>().AddForce(muzzle.forward * bulletForce);
  }

  void Update()
  {
    if (Input.GetButton("Fire1") && Time.time > nextFire)
    {
      nextFire = Time.time + fireRate;
      Fire();
    }
  }
}
