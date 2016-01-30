using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    public Transform objectToFollow;
    public Vector3 offset = new Vector3(0,15,0);
	// Use this for initialization
	void Start () {
        transform.position = objectToFollow.position + offset;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(transform.position,objectToFollow.position + offset, Time.deltaTime*2);
	}
}
