using NaughtyAttributes;
using System.Collections;
using UnityEngine;

[System.Serializable]
public struct CameraShakeStats
{
    public float _magnitude;
    public float _duration;
}

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    [SerializeField] Camera targetCamera;
    [SerializeField] CameraShakeStats defaultStats;

    Vector3 originalPos;
    Coroutine coroutine;

    private void Start()
    {
        if(targetCamera == null)
            targetCamera = Camera.main;
    }

    [Button]
    public void Shake()
    {
        Shake(defaultStats._magnitude, defaultStats._duration);
    }
    public void Shake(float magnitude, float duration)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        coroutine = StartCoroutine(ShakeCoroutine(magnitude, duration));
    }
    IEnumerator ShakeCoroutine(float magnitude, float duration)
    {
        originalPos = targetCamera.transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            targetCamera.transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        targetCamera.transform.localPosition = originalPos;
    }
}