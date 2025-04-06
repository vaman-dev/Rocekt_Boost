using UnityEngine;
using UnityEngine.InputSystem;

public class Movment_ : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 direction;
    public float speed = 1.0f;
    public float jumpForce = 5.0f; 

    void Awake()
    {
        // Gets Rigidbody component attached to this GameObject
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Run();
        // Jump(); 
    }

    void OnMove(InputValue value)
    {
        Vector2 inputVector = value.Get<Vector2>();
        direction = new Vector3(inputVector.x, 0.0f, inputVector.y);
    }

    void Run()
    {
        // ✅ Use rb.velocity instead of rb.linearVelocity
        rb.linearVelocity = direction * speed;
    }

    void OnThrust(InputValue value)
    {
        if(value.isPressed)
        {
            Debug.Log("Key is pressed value");
            Make_Jump();
        }
        else
        {
            Debug.Log("Key is not pressed");
        }
    }

    void Make_Jump()
    { 
        
        rb.AddRelativeForce(Vector3.up * jumpForce, ForceMode.Force);
    }

    
}
