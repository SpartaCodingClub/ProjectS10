using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float atk = 0;
    [SerializeField] float speed = 5f;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] List<string> Enemytag;
    [SerializeField] ParticleSystem particle;
    Vector3 direction;
    Rigidbody rigid;
    bool CanAttack = false;

    private void OnEnable()
    {
        rigid = GetComponent<Rigidbody>();
        Invoke(nameof(DestroyObject), 7);
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
        if (CanAttack == false)
            return;
        if (enemyLayer.value == (enemyLayer.value | (1 << other.gameObject.layer)))
        {
            foreach (string t in Enemytag)
            {
                if (other.tag.Equals(t))
                {
                    //데미지 주는 함수
                    other.GetComponent<StatHandler>().Damage(atk);
                    break;
                }
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

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
