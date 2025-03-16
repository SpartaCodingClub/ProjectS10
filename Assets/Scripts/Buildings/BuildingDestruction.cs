using System.Collections;
using UnityEngine;

public class BuildingDestruction : MonoBehaviour
{
    private Material[] materials;
    private float dissolveAmount = 0;
    public float dissolveSpeed = 1f;
    public ParticleSystem destructionEffect; 

    private void Start()
    {
        materials = GetComponent<Renderer>().materials;
    }

    public void StartDestruction()
    {
        if (destructionEffect != null)
        {
            Debug.Log("파티클 실행됨");
            destructionEffect.Play(); 
        }
        StartCoroutine(DissolveEffect());
    }

    private IEnumerator DissolveEffect()
    {
        while (dissolveAmount < 1)
        {
            dissolveAmount += Time.deltaTime * dissolveSpeed;

            foreach (var mat in materials)
            {
                if (mat.HasProperty("_DissolveAmount"))
                {
                    mat.SetFloat("_DissolveAmount", dissolveAmount);
                }
            }
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
