using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscllator : MonoBehaviour
{

    [SerializeField] Vector3 movmentVector;
    [Range(0, 1)] [SerializeField] float movementFactor;

    Vector3 startingPos;
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position; // the position of the current object the scrip is on
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = movementFactor * movmentVector;
        transform.position = startingPos + offset;
    }
}
