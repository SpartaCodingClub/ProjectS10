using System.Collections;
using UnityEngine;

public class BuildingAnimation : MonoBehaviour
{
    private float animationTime;
    private Vector3 startPos;
    private Vector3 finalPos;

    public void PlayAnimation(float constructionTime, float buildingHeight)
    {
        animationTime = constructionTime;

        startPos = new Vector3(transform.position.x, -buildingHeight, transform.position.z);
        finalPos = new Vector3(transform.position.x, 0, transform.position.z);

        StartCoroutine(BuildingRise());
    }

    private IEnumerator BuildingRise()
    {
        float timer = 0f;

        while (timer < animationTime)
        {
            timer += Time.deltaTime;
            float shakeAmount = Mathf.Sin(timer * 30f) * 0.1f;
            float riseProgress = Mathf.SmoothStep(0, 1f, timer / animationTime);

            transform.position = Vector3.Lerp(startPos, finalPos, riseProgress);
            transform.position += new Vector3(shakeAmount, 0, 0);

            yield return null;
        }
        transform.position = finalPos;
    }

    public void RemoveAnimation(float removeTime, float buildingHeight)
    {
        StartCoroutine(BuildingRemove(removeTime, buildingHeight));
    }

    private IEnumerator BuildingRemove(float removeTime, float buildingHeight)
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(startPos.x, -buildingHeight, startPos.z); 

        float timer = 0f;
        while (timer < removeTime)
        {
            timer += Time.deltaTime;
            float shakeAmount = Mathf.Sin(timer * 30f) * 0.1f;
            float progress = Mathf.SmoothStep(0, 1f, timer / removeTime);

            transform.position = Vector3.Lerp(startPos, endPos, progress);
            transform.position += new Vector3(shakeAmount, 0, 0);

            yield return null;
        }

        transform.position = endPos;
    }
}
