using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public float multiplier = 2;
    public float waitTime = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Pickup(other));
        }
    }

    IEnumerator Pickup(Collider player) 
    {
       PlayerMotion playerMotion =  player.GetComponent<PlayerMotion>();
        playerMotion.runSpeed *= multiplier;
        playerMotion.sprintSpeed *= multiplier;

        gameObject.transform.position = new Vector3(0, -50, 0);

        yield return new WaitForSeconds(waitTime);

        playerMotion.runSpeed /= multiplier;
        playerMotion.sprintSpeed /= multiplier;

        Destroy(gameObject);
    }

}
