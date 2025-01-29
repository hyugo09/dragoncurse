using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    // Start is called before the first frame
    private void Awake()
    {
        instance = this;
    }
    public IEnumerator Shake (float duration, float intencity)
    {
        Vector3 originalPos = this.gameObject.transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float x = Random.Range(-1f,1f) * intencity;
            float y = Random.Range(-1f, 1f) + intencity;

            this.gameObject.transform.localPosition = new Vector3(x,y, originalPos.y);

            yield return null;
        }
    }
}
