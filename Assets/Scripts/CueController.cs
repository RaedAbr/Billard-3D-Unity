using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueController : MonoBehaviour {
    public GameObject pointA;
    public GameObject pointB;
    public float speed;
    private GameObject mainCamera;

    void Start()
    {
        speed = 30;
        mainCamera = GameObject.FindWithTag("MainCamera");
    }
        
    public void onButtonClick()
    {
        Vector3 from = pointA.transform.position;
        Vector3 to = pointB.transform.position;
        transform.position = from;
        StartCoroutine(Move_Routine(this.transform, from, to));
    }

    private IEnumerator Move_Routine(Transform transform, Vector3 from, Vector3 to)
    {
        float t = 1f;
        mainCamera.GetComponent<CameraController>().canMove = false;
        while (Vector3.Distance(transform.position, to) > 0)
        {
            t += Time.deltaTime * speed;
            //transform.position = Vector3.Lerp(from, to, Mathf.SmoothStep(0f, 1f, t));
            transform.position = Vector3.MoveTowards(from, to, t);
            yield return null;
        }
        mainCamera.GetComponent<CameraController>().canMove = true;
    }
}
