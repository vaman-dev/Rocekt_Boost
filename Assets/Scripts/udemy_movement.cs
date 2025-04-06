using UnityEngine;
using UnityEngine.InputSystem;

public class UdemyMovement : MonoBehaviour
{
    #region Input and Movement Settings

    [Header("Input Actions")]
    [SerializeField] private InputAction thrust;
    [SerializeField] private InputAction rotation;

    [Header("Movement Settings")]
    [SerializeField] private float forceStrength = 10f;
    [SerializeField] private float rotationStrength = 0f;

    #endregion

    #region Particle Effects

    [Header("Particle Effects")]
    [SerializeField] private ParticleSystem thrustParticle;
    [SerializeField] private ParticleSystem leftParticle;
    [SerializeField] private ParticleSystem rightParticle;

    #endregion

    #region Component Caching

    private Rigidbody rb;
    private float cachedZPosition; // Cached Z position to lock

    #endregion

    #region Unity Lifecycle Methods

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing on this GameObject!");
        }

        // Cache the Z position to lock it later
        cachedZPosition = transform.position.z;
    }

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    private void OnDisable()
    {
        thrust.Disable();
        rotation.Disable();
    }

    private void FixedUpdate()
    {
        HandleThrust();
        HandleRotation();
        RestrictMovement(); // Lock Z position each physics frame
    }

    #endregion

    #region Movement Methods

    private void HandleThrust()
    {
        float thrustInput = thrust.ReadValue<float>();

        if (thrustInput > 0)
        {
            PlayThrustParticles();
            rb.AddRelativeForce(Vector3.up * forceStrength);
        }
        else
        {
            StopThrustParticles();
        }
    }

    private void HandleRotation()
    {
        float rotationInput = rotation.ReadValue<float>();

        if (rotationInput > 0)
        {
            PlayLeftRotationParticles();
            Rotate(rotationStrength);
        }
        else if (rotationInput < 0)
        {
            PlayRightRotationParticles();
            Rotate(-rotationStrength);
        }
        else
        {
            StopRotationParticles();
        }
    }

    private void Rotate(float rotationValue)
    {
        rb.freezeRotation = true; // Temporarily disable physics rotation
        transform.Rotate(Vector3.forward * rotationValue * Time.fixedDeltaTime);
        // rb.freezeRotation = false; // Optional: Enable if you want physics rotation again
    }

    private void RestrictMovement()
    {
        Vector3 lockedPosition = rb.position;
        lockedPosition.z = cachedZPosition; // Keep Z axis locked
        rb.position = lockedPosition;
    }

    private void PlayThrustParticles()
    {
        if (!thrustParticle.isPlaying)
        {
            thrustParticle.Play();
        }
        StopRotationParticles();
    }

    private void StopThrustParticles()
    {
        if (thrustParticle.isPlaying)
        {
            thrustParticle.Stop();
        }
    }

    private void PlayLeftRotationParticles()
    {
        if (!leftParticle.isPlaying)
        {
            leftParticle.Play();
        }
        if (rightParticle.isPlaying)
        {
            rightParticle.Stop();
        }
        StopThrustParticles();
    }

    private void PlayRightRotationParticles()
    {
        if (!rightParticle.isPlaying)
        {
            rightParticle.Play();
        }
        if (leftParticle.isPlaying)
        {
            leftParticle.Stop();
        }
        StopThrustParticles();
    }

    private void StopRotationParticles()
    {
        if (leftParticle.isPlaying)
        {
            leftParticle.Stop();
        }
        if (rightParticle.isPlaying)
        {
            rightParticle.Stop();
        }
    }

    #endregion
}
