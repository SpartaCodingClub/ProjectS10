using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_InteractionFinder : MonoBehaviour
{
    [Header("상호 작용 물체 찾기")]
    [SerializeField] float distance;
    [SerializeField] LayerMask layerMask;
    public GameObject curInteract;
    // Start is called before the first frame update
    void Start()
    {
        curInteract = null;
    }

    // Update is called once per frame
    void Update()
    {
        FindInteract();
    }

    void FindInteract()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, distance, layerMask))
        {
            curInteract = hit.collider.gameObject;
            UnityEngine.Debug.DrawRay(transform.position, transform.forward * distance, Color.red);
        }
        else
        {
            curInteract = null;
            UnityEngine.Debug.DrawRay(transform.position, transform.forward * distance, Color.green);
        }
    }
}
