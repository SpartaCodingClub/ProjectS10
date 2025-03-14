using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float atk = 0;
    [SerializeField] float speed = 5f;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] string tag;
    ParticleSystem particle;
    bool CanAttack = false;

    private void OnEnable()
    {
        particle = GetComponentInChildren<ParticleSystem>();
    }
    public void Init(int atkinput = 0)
    {
        atk += atkinput;
        CanAttack = true;
    }

    private void Update()
    {
        ProjectileAction();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enemyLayer.value == (enemyLayer.value | (1 << other.gameObject.layer)))
        {
            if (other.tag.Equals(tag))
            {
                //데미지 주는 함수
            }
            StartCoroutine(destroyWithParticle());
        }
    }

    IEnumerator destroyWithParticle()
    {
        MeshRenderer mesh = GetComponentInChildren<MeshRenderer>();
        mesh.enabled = false;
        CanAttack = false;
        //파티클 시스템이 있으면 사용됨.
        if (particle != null)
        {
            particle.Play();
            while (particle.isPlaying)
            {
                yield return null;
            }
        }
        Destroy(gameObject);
        yield return null;
    }

    public virtual void ProjectileAction()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime);
    }
}
