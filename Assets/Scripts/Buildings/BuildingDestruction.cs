using System.Collections;
using UnityEngine;

public class BuildingDestruction : MonoBehaviour
{
    public ParticleSystem destructionEffect;

    public void StartDestruction()
    {
        if (destructionEffect != null)
        {
            Debug.Log("파티클 실행됨");
            destructionEffect.Play();
        }

        StartCoroutine(ScaleDownEffect());
    }

    private IEnumerator ScaleDownEffect()
    {
        float duration = 1.0f;
        float timer = 0f;
        Vector3 originalScale = transform.localScale;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float scaleAmount = Mathf.Lerp(1f, 0f, timer / duration);
            transform.localScale = originalScale * scaleAmount;

            float shakeAmount = Mathf.Sin(timer * 30f) * 0.05f;
            transform.position += new Vector3(shakeAmount, 0, 0);

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
