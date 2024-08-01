using UnityEngine;
using UnityEngine.AI;

public class SeatedLocomotion : MonoBehaviour
{
    //private NavMeshSurface navMeshSurface;
    
    public float movementSpeed = 0.8f;
    public float maxMovement = 1f;
    public float threshold = 0.15f;
    public float thresholdLateral = 0.10f;
    public float backPercentage = 0.05f;
    public float thresholdRotation = 25f;

    public float movementForce = 1;

    public Transform targetObject;

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Vector3 originalPositionNorm;

    //Variables for chracter controller
    [Header("General")]
    [Tooltip("Force applied downward when in the air")]
    public float gravityDownForce = 20f;

    CharacterController m_Controller;
    [Tooltip("Physic layers checked to consider the player grounded")]
    public LayerMask groundCheckLayers = -1;
    [Tooltip("distance from the bottom of the character controller capsule to test for grounded")]
    public float groundCheckDistance = 0.05f;
    const float k_GroundCheckDistanceInAir = 0.07f;
    public bool isGrounded { get; private set; }

    public static bool forwardIdleActive = true;
    public static bool rotationIdleActive = false;
    public static bool lateralIdleActive = true;

    public bool activateRotation = true;
    public bool activateForward = true;
    public bool activateBackward = true;
    public bool activateLateral = true;

    Vector3 m_GroundNormal;
    

    private void Start()
    {
        originalPosition = targetObject.transform.localPosition;
        originalPositionNorm = originalPosition.normalized;
        originalRotation = targetObject.rotation;


        // fetch components on the same gameObject
        m_Controller = GetComponent<CharacterController>();
        //DebugUtility.HandleErrorIfNullGetComponent<CharacterController>(m_Controller, this, gameObject);

    }

    void Update()
    {

        //bool wasGrounded = isGrounded;
        //GroundCheck();

        if (targetObject != null)
        {
            // Read the position and orientation of the target object
            //Vector3 position = targetObject.transform.forward;
            Vector3 targetPosition = targetObject.transform.localPosition;
            Quaternion targetRotation = targetObject.transform.localRotation;

            //Rigidbody rb = GetComponent<Rigidbody>(); // Assuming the script is attached to a GameObject with a Rigidbody component


            Debug.Log("Camera Position: " + targetPosition);
            //IsWithinNavMesh(transform.position);

            if (targetPosition.z >= threshold && activateForward)
            {
                Vector3 forwardDirection = transform.forward;
                forwardDirection.y = 0;
                forwardDirection.Normalize();
                //transform.position += forwardDirection * movementSpeed * Time.deltaTime;
                m_Controller.Move(Vector3.Lerp(Vector3.zero, forwardDirection, (targetPosition.z - threshold) * 10 * movementSpeed * Time.deltaTime));
                //m_Controller.Move(Vector3.Lerp(Vector3.zero, forwardDirection, movementSpeed * Time.deltaTime));
                Debug.Log("threshold is being reached and passed move forward");
                forwardIdleActive = false;
            }
            else if (targetPosition.z < threshold && targetPosition.z > -threshold * backPercentage && activateForward)
            {
                forwardIdleActive = true;
            }
            
            if (targetPosition.z <= -threshold*backPercentage && activateBackward)
            {
                Vector3 forwardDirection = transform.forward;
                forwardDirection.y = 0;
                forwardDirection.Normalize();
                //transform.position += -forwardDirection * movementSpeed * Time.deltaTime;
                m_Controller.Move(Vector3.Lerp(Vector3.zero, -forwardDirection, movementSpeed * Time.deltaTime));
                Debug.Log("threshold is being reached and passed move backward");
                forwardIdleActive = false;
            }
            
            if (targetRotation.eulerAngles.y >=  thresholdRotation && activateRotation)
            {
                Quaternion rotateDirection = Quaternion.Euler(0f, targetObject.eulerAngles.y, 0f);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotateDirection, Time.deltaTime*movementSpeed);
                rotationIdleActive = false;
            }
            else if (targetRotation.eulerAngles.y < thresholdRotation && activateRotation)
            {
                rotationIdleActive = true;
            }

            //This part of the code is added for lateral movements
            if (Mathf.Abs(targetPosition.x) >= thresholdLateral && activateLateral)
            {
                Vector3 lateralDirection = new Vector3();
                if (targetPosition.x >= 0)
                {
                    lateralDirection = transform.right;
                }
                else if (targetPosition.x < 0)
                {
                    lateralDirection = -transform.right;
                }

                lateralDirection.y = 0;
                lateralDirection.Normalize();
                //transform.position += forwardDirection * movementSpeed * Time.deltaTime;
                m_Controller.Move(Vector3.Lerp(Vector3.zero, lateralDirection, movementSpeed * Time.deltaTime));
                Debug.Log("threshold is being reached and passed move forward");
                lateralIdleActive = false;
            }
            else if (Mathf.Abs(targetPosition.x) < thresholdLateral && activateLateral)
            {
                lateralIdleActive = true;
            }


            //Debug.Log("Camera Move: " + threshold + " " + -threshold * backPercentage + " " + targetPosition.z);

        }
        else
        {
            // If the target object is not assigned, log a warning message
            Debug.LogWarning("Target object is not assigned.");
        }

        

    }

    public bool IsWithinNavMesh(Vector3 position)
    {
        // Sample the NavMesh to check if the given position is within the NavMesh bounds
        NavMeshHit hit;
        Debug.Log("IsWithinNavMesh: " + NavMesh.SamplePosition(position, out hit, 0.1f, NavMesh.AllAreas));
        Debug.Log("IsWithinNavMesh: given position " + position);
        Debug.Log("IsWithinNavMesh: hit position " + hit); 
        return NavMesh.SamplePosition(position, out hit, 0.5f, NavMesh.AllAreas);
    }

    void GroundCheck()
    {
        // Make sure that the ground check distance while already in air is very small, to prevent suddenly snapping to ground
        float chosenGroundCheckDistance = isGrounded ? (m_Controller.skinWidth + groundCheckDistance) : k_GroundCheckDistanceInAir;

        // reset values before the ground check
        isGrounded = false;
        m_GroundNormal = Vector3.up;

        // if we're grounded, collect info about the ground normal with a downward capsule cast representing our character capsule
        if (Physics.CapsuleCast(GetCapsuleBottomHemisphere(), GetCapsuleTopHemisphere(m_Controller.height), m_Controller.radius, Vector3.down, out RaycastHit hit, chosenGroundCheckDistance, groundCheckLayers, QueryTriggerInteraction.Ignore))
        {
            // storing the upward direction for the surface found
            m_GroundNormal = hit.normal;

            // Only consider this a valid ground hit if the ground normal goes in the same direction as the character up
            // and if the slope angle is lower than the character controller's limit
            if (Vector3.Dot(hit.normal, transform.up) > 0f &&
                IsNormalUnderSlopeLimit(m_GroundNormal))
            {
                isGrounded = true;

                // handle snapping to the ground
                if (hit.distance > m_Controller.skinWidth)
                {
                    m_Controller.Move(Vector3.down * hit.distance);
                    Debug.Log("Character Controller was moved");
                }
            }
        }
        Debug.Log("isGrounded is set to: " + isGrounded);
    }

    Vector3 GetCapsuleBottomHemisphere()
    {
        return transform.position + (transform.up * m_Controller.radius);
    }

    // Gets the center point of the top hemisphere of the character controller capsule    
    Vector3 GetCapsuleTopHemisphere(float atHeight)
    {
        return transform.position + (transform.up * (atHeight - m_Controller.radius));
    }

    // Returns true if the slope angle represented by the given normal is under the slope angle limit of the character controller
    bool IsNormalUnderSlopeLimit(Vector3 normal)
    {
        return Vector3.Angle(transform.up, normal) <= m_Controller.slopeLimit;
    }

}

