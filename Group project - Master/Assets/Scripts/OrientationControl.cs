using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationControl : MonoBehaviour
{
    [SerializeField] Transform orientation;
    [SerializeField] LayerMask ground;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, ground))
        {
            GameObject groundObject = hit.collider.gameObject;
            Debug.Log("Hit Ground");
            orientation.eulerAngles = new Vector3(groundObject.transform.eulerAngles.x, 0, groundObject.transform.eulerAngles.z);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down);
    }
}
