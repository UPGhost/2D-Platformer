using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    // Camera Edge of Screen

    [SerializeField]
    private float xMin;

    [SerializeField]
    private float yMin;

    [SerializeField]
    private float xMax;

    [SerializeField]
    private float yMax;

    private Transform target;

	// Use this for initialization
	void Start ()
    {
        //Target Player

        target = GameObject.Find("Player_Character").transform;
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        if (target.position.y > 18 || target.position.y < -2)
        {
            transform.position = new Vector3(Mathf.Clamp(target.position.x, xMin, xMax), Mathf.Clamp(target.position.y, yMin, yMax), target.position.z - 1);
        }
        else
        {
            transform.position = new Vector3(Mathf.Clamp(target.position.x, xMin, xMax), Mathf.Clamp(6.8f, yMin, yMax), target.position.z - 1);
        }
	}
}
