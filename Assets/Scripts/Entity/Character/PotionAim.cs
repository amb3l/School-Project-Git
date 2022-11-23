using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionAim : MonoBehaviour
{
    [SerializeField] Transform aimTransform;
    private Vector3 mousePosition;
    private Vector3 aimDirection;

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        aimTransform.eulerAngles = new Vector3(0, 0, angle);
    }
}