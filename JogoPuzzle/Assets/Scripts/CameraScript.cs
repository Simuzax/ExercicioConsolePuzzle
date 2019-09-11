using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private float yaw;
    private float pitch;

    [SerializeField]
    private float mouseSensitivity = 3f;

    [SerializeField]
    private float distanceFromTargetX;

    [SerializeField]
    private float distanceFromTargetY;

    [SerializeField]
    private float pitchMin = -40;

    [SerializeField]
    private float pitchMax = 80;

    [SerializeField]
    private bool lockCursor;

    private Vector3 currentRotation;

    [SerializeField]
    private float rotationSmoothTime = 0.12f;

    [SerializeField]
    private Vector3 rotationSmoothVelocity;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private LayerMask cameraLayerMask;

    // Use this for initialization
    private void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            
            Cursor.visible = false;
        }
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        
        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);

        Vector3 targetRotation = new Vector3(pitch, yaw);

        currentRotation = Vector3.SmoothDamp(currentRotation, targetRotation, ref rotationSmoothVelocity, rotationSmoothTime);
        
        transform.eulerAngles = currentRotation;

        transform.position = target.position - transform.forward * distanceFromTargetX + transform.up * distanceFromTargetY;

        checkWall();
    }

    private void checkWall()
    {
        RaycastHit hit;
        
        Vector3 raystart = target.position;
        
        Vector3 dir = (transform.position - target.position).normalized;

        float dist = Vector3.Distance(transform.position, target.position);

        if (Physics.Raycast(raystart, dir, out hit, dist, cameraLayerMask))
        {
            float hitDistance = hit.distance;

            Vector3 castCenterHit = target.position + (dir.normalized * hitDistance);

            transform.position = castCenterHit;
        }
    }
}