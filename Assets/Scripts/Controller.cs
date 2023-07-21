using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] float rotationSensitivity = 1f;
    [SerializeField] float maxRotation = 1f;

    float horizontalAxis, verticalAxis;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalAxis = Input.GetAxisRaw("Mouse X");
        verticalAxis = Input.GetAxisRaw("Mouse Y");
        
        var hr = Mathf.Sign(horizontalAxis) * Mathf.Min(Mathf.Abs(horizontalAxis), maxRotation);
        var vr = Mathf.Sign(verticalAxis)   * Mathf.Min(Mathf.Abs(verticalAxis),   maxRotation);

        var rotation = transform.rotation.eulerAngles;
        rotation.x += vr * rotationSensitivity;
        rotation.z += -hr * rotationSensitivity;
        transform.rotation = Quaternion.Euler(rotation);
    }
}
