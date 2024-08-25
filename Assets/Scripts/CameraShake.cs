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
        ShakeDirectional(Vector2.one, defaultStats);
    }
    public void Shake(CameraShakeStats stats)
    {
        ShakeDirectional(Vector2.one, stats);
    }
    public void ShakeDirectional(Vector2 direction, CameraShakeStats stats)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        Vector2 dir = direction + Random.insideUnitCircle / 10;
        coroutine = StartCoroutine(ShakeCoroutine(dir, stats));
    }
    IEnumerator ShakeCoroutine(Vector2 direction, CameraShakeStats stats)
    {
        originalPos = targetCamera.transform.localPosition;
        float elapsed = 0.0f;
        while (elapsed < stats._duration)
        {
            Vector2 shake = stats._magnitude * direction * Random.insideUnitCircle;

            targetCamera.transform.localPosition = new Vector3(shake.x, shake.y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        targetCamera.transform.localPosition = originalPos;
    }
}