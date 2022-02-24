using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{

    private Coroutine destroyRoutine;

    public void ActivateDestroy()
    {
        destroyRoutine = StartCoroutine(DestroyAfterDelay());
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(3f);

        Destroy(gameObject);
    }

}
