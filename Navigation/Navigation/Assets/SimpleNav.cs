using UnityEngine;
using System.Collections;

public class SimpleNav : MonoBehaviour {


    public Transform target;

    NavMeshAgent _agent;

	// Use this for initialization
	void Start () {
        _agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        _agent.SetDestination(target.position);
	}
}
