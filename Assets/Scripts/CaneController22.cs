using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CaneController22 : MonoBehaviour {

	[SerializeField] private float moveSpeed;
    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;
    [SerializeField] private bool reverseMove = false;
    [SerializeField] private Transform objectToUse;
    [SerializeField] private bool moveThisObject = false;
    [SerializeField] private Slider mainSlider;
    private float startTime;
    private float journeyLength;
    private float distCovered;
    private float fracJourney;
    void Start()
    {
        //Adds a listener to the main slider and invokes a method when the value changes.
        mainSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });

        startTime = Time.time;
        if (moveThisObject)
        {
            objectToUse = transform;
        }
        journeyLength = Vector3.Distance(pointA.transform.position, pointB.transform.position);
    }
    void Update()
    {
        distCovered = (Time.time - startTime) * moveSpeed;
        fracJourney = distCovered / journeyLength;
        if (reverseMove)
        {
            objectToUse.position = Vector3.Lerp(pointB.transform.position, pointA.transform.position, fracJourney);
        }
        else
        {
            objectToUse.position = Vector3.Lerp(pointA.transform.position, pointB.transform.position, fracJourney);
        }
        if ((Vector3.Distance(objectToUse.position, pointB.transform.position) == 0.0f || Vector3.Distance(objectToUse.position, pointA.transform.position) == 0.0f)) //Checks if the object has travelled to one of the points
        {
            if (reverseMove)
            {
                reverseMove = false;
            }
            else
            {
                reverseMove = true;
            }
            startTime = Time.time;
        }
    }

    // Invoked when the value of the slider changes.
    public void ValueChangeCheck()
    {
        Debug.Log(mainSlider.value);
    }
}
