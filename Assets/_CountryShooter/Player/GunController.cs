using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
  /*
    Bullets last at most 3 seconds. With a fire rate of 0.3, we
    need a pool of 10 bullets.
  */
  private GameObject[] bulletPool;
  public int bulletCount = 10;
  public float fireRate = 0.3f;
  private float nextFire = 0.0f;
  public int bulletForce = 200;
  public GameObject bullet;
  public Transform muzzle;
  private int currentBulletIndex = 0;

  void Awake()
  {
    bulletPool = new GameObject[bulletCount];

    for (int i = 0; i < bulletCount; i++)
    {
      GameObject bulletClone = Instantiate(bullet, muzzle.position, muzzle.rotation) as GameObject;
      bulletPool[i] = bulletClone;
    }
  }

  void Fire()
  {
    GameObject currentBullet = bulletPool[currentBulletIndex % bulletPool.Length];
    currentBullet.transform.position = muzzle.position;
    currentBullet.transform.rotation = muzzle.rotation;

    currentBullet.GetComponent<Collider>().enabled = true;
    Rigidbody currentBulletRB = currentBullet.GetComponent<Rigidbody>();

    currentBulletRB.velocity = Vector3.zero;
    currentBulletRB.angularVelocity = Vector3.zero;
    currentBullet.SetActive(true);

    currentBulletRB.AddForce(muzzle.forward * bulletForce);

    currentBulletIndex++;
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
