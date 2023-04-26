using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public float multiplier = 1.4f;
    public float waitTime = 2f;

    public bool aquired = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Pickup(other));
        }
    }

    IEnumerator Pickup(Collider player)
    {
        //PhysicsManager physicsManager = player.GetComponent<PhysicsManager>();

        //powerup goes here
        //playerMotion.runSpeed *= multiplier;

        aquired = true;
       
        gameObject.transform.position = new Vector3(0, -50, 0);

        yield return new WaitForSeconds(waitTime);

        //powerup ends here
        //playerMotion.runSpeed /= multiplier;

        Destroy(gameObject);
    }

}
