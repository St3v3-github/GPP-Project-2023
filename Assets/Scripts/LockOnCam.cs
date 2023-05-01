using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnCam : MonoBehaviour
{
    public Transform playerTransform;
    public string enemyTag;
    public float maxDistance = 10f;
    public float smoothTime = 0.3f;
    private Vector3 originalPosition;
    private bool isLockedOn;
    private Vector3 targetDirection;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        originalPosition = transform.position;
    }
    void LateUpdate()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(playerTransform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null && closestDistance <= maxDistance)
        {
            targetDirection = closestEnemy.transform.position - playerTransform.position;
            isLockedOn = true;
        }
        else
        {
            isLockedOn = false;
            targetDirection = playerTransform.forward;
        }
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, smoothTime * Time.deltaTime);

        if (isLockedOn)
        {
            transform.position = playerTransform.position - playerTransform.forward * 5f + Vector3.up * 2f;
            transform.LookAt(playerTransform.position + playerTransform.forward * 5f);
        }
        else
        {
            transform.position = originalPosition;
            transform.LookAt(playerTransform.position);
        }
    }
}
