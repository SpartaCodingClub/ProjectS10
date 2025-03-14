using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float atk = 0;
    [SerializeField] float speed = 5f;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] string tag;
    Vector3 direction;
    Rigidbody rigid;
    ParticleSystem particle;
    bool CanAttack = false;

    private void OnEnable()
    {
        rigid = GetComponent<Rigidbody>();
        particle = GetComponentInChildren<ParticleSystem>();
    }
    public void Init()
    {
        CanAttack = true;
    }

    private void FixedUpdate()
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

    public void ProjectileAction()
    {
        rigid.velocity = transform.forward * speed;
    }
}
