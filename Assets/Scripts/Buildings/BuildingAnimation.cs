using System.Collections;
using UnityEngine;

public class BuildingAnimation : MonoBehaviour
{
    private float buildTime;
    private Vector3 startPos;
    private Vector3 finalPos;

    public void PlayAnimation(float constructionTime)
    {
        buildTime = constructionTime;
        float buildingHeight = GetComponent<Renderer>().bounds.size.y;

        // 이거 뭔가 잘 안 먹는데 문제가 뭐지
        startPos = transform.position;
        finalPos = new Vector3(transform.position.x, 0, transform.position.z);

        StartCoroutine(BuildingRise());
    }

    private IEnumerator BuildingRise()
    {
        float timer = 0f;

        while (timer < buildTime)
        {
            timer += Time.deltaTime;
            float shakeAmount = Mathf.Sin(timer * 30f) * 0.1f;
            float riseProgress = Mathf.SmoothStep(0, 1f, timer / buildTime);

            transform.position = Vector3.Lerp(startPos, finalPos, riseProgress);
            transform.position += new Vector3(shakeAmount, 0, 0);

            yield return null;
        }
        transform.position = finalPos;
    }
}
