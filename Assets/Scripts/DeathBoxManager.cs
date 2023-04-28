using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBoxManager : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private float spawnX;
    [SerializeField] private float spawnY;
    [SerializeField] private float spawnZ;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            player.gameObject.transform.position = new Vector3(spawnX, spawnY, spawnZ);
        }

    }
}
