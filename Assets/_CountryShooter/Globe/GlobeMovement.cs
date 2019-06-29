using UnityEngine;
using System.Collections;

public class GlobeMovement : MonoBehaviour
{
  public float duration = 3f;
  public Vector3 africaEulerRotation;
  public Vector3 asiaEulerRotation;
  public Vector3 australiaEulerRotation;
  public Vector3 europeEulerRotation;
  public Vector3 northAmericaEulerRotation;
  public Vector3 southAmericaEulerRotation;

  private Constants.Region lastRegion;

  void Awake()
  {
    GameController.OnSetQuestion += RotateToRegion;
  }

  void onDisable()
  {
    GameController.OnSetQuestion -= RotateToRegion;
  }

  void RotateToRegion(Constants.Region region)
  {
    // Only rotate if you're switching regions
    if (region != lastRegion)
    {
      Quaternion rotateDirection = GetRotationByRegion(region);
      StartCoroutine(RotateTo(rotateDirection));
      lastRegion = region;
    }
  }

  Quaternion GetRotationByRegion(Constants.Region region)
  {
    switch (region)
    {
      case Constants.Region.Africa:
        return Quaternion.Euler(africaEulerRotation);
      case Constants.Region.Asia:
        return Quaternion.Euler(asiaEulerRotation);
      case Constants.Region.Australia:
        return Quaternion.Euler(australiaEulerRotation);
      case Constants.Region.Europe:
        return Quaternion.Euler(europeEulerRotation);
      case Constants.Region.NorthAmerica:
        return Quaternion.Euler(northAmericaEulerRotation);
      case Constants.Region.SouthAmerica:
        return Quaternion.Euler(southAmericaEulerRotation);
      default:
        return Quaternion.Euler(0, 0, 0);
    }
  }

  IEnumerator RotateTo(Quaternion toRotation)
  {
    float halfPi = Mathf.PI / 2;
    float timeCount = 0.0f;
    Quaternion startRotation = transform.rotation;

    while (timeCount < duration)
    {
      // Use sin (between 0 and 1/2 pi) to smooth it out
      transform.rotation = Quaternion.Slerp(startRotation, toRotation, Mathf.Sin((timeCount / duration) * halfPi));
      timeCount = timeCount + Time.deltaTime;
      yield return null;
    }
  }
}
