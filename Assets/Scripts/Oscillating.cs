using UnityEngine;

public class Oscillating : MonoBehaviour
{
   [SerializeField] Vector3 movementVector;
    [SerializeField] float speed;
    
    Vector3 startPosition;
    Vector3 endPosition;
    float movementFactor;

    void Start()
    {
        startPosition = transform.position; // stores the position of the object at the start
        endPosition = startPosition + movementVector; // assigns the end position based on the movement vector
    }

    void Update()
    {
        movementFactor = Mathf.PingPong(Time.time * speed, 1f); // calculates the movement factor using PingPong to oscillate between 0 and 1
        transform.position = Vector3.Lerp(startPosition, endPosition, movementFactor); // for the smooth transition between start and end position 
    }
}
