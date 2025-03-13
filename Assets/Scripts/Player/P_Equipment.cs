using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Equipment : MonoBehaviour
{
    [SerializeField] Equipment curEquipment;
    [SerializeField] Vector3 boxOffset;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] Vector3 boxSize = new Vector3(1, 1, 1);
    RaycastHit[] hits;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (curEquipment != null)
        {
            hits = Physics.BoxCastAll(transform.position + transform.forward * boxSize.z / 2 + transform.TransformDirection(boxOffset), boxSize, transform.forward, transform.rotation, 0, enemyLayer);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 boxCenter = transform.position + transform.forward * boxSize.z / 2 + transform.TransformDirection(boxOffset);
        Gizmos.matrix = Matrix4x4.TRS(boxCenter, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, boxSize);
    }
}
