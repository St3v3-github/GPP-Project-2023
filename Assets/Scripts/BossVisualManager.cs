using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossVisualManager : MonoBehaviour
{
    // Variables
    MeshRenderer meshRenderer;
    Color originalColour, damageColor = Color.red;
    [SerializeField] private float flashTime = 0.35f;

    // Initialising
    void Start()
    {
        //...
        meshRenderer = GetComponent<MeshRenderer>();
        originalColour = meshRenderer.material.color;
        // ...
    }

    public void Damage()
    {
        StartCoroutine(EFlash());
    }

    // Function Creation
    public IEnumerator EFlash()
    {
        meshRenderer.material.color = damageColor;
        yield return new WaitForSeconds(flashTime);
        meshRenderer.material.color = originalColour;
    }
}
