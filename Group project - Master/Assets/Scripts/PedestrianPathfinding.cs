using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PedestrianPathfinding : MonoBehaviour
{
    [SerializeField] NavMeshAgent nav;
    [SerializeField] float pathingRadius = 5f;
    [SerializeField] bool hitObstacle;
    [SerializeField] bool changeDestination;
    [SerializeField] LayerMask buildings;

    Vector3 spawn;
    Vector3 newPosition;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        spawn = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //hitObstacle = Physics.Raycast(transform.position, transform.position + transform.forward * 0.6f, buildings);

        if (hitObstacle)
        {
            Invoke("changeDestinationOnCollision", 1f);
        }

        if (nav.remainingDistance < 0.5f || changeDestination)
        {
            if (changeDestination) { changeDestination = false; }
            //gets a random position on the navMesh within a circle around the pedestrian
            NavMesh.SamplePosition(spawn + Random.insideUnitSphere * pathingRadius, out NavMeshHit hit, pathingRadius, NavMesh.AllAreas);

            NavMeshPath path = new NavMeshPath();
            if (hit.position == Vector3.positiveInfinity)
            {
                changeDestination = true;
            }
            else if (nav.CalculatePath(hit.position, path))
            {
                nav.SetPath(path);
            }
        }
    }

    private void changeDestinationOnCollision()
    {
        if (hitObstacle)
        {
            changeDestination = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 0.6f);
        Gizmos.DrawWireSphere(spawn, pathingRadius);
    }

}
