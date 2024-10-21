using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Camera cam;

    private Vector3 _movement = Vector3.zero;
    private Vector3 _mousePosition = Vector3.zero;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.z = Input.GetAxisRaw("Vertical");

        _mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        _mousePosition.y = 0;
        Debug.Log($"mouse position: {_mousePosition}"); // let's assume for n ow that we are getting the correct mouse position.
    }

    private void FixedUpdate()
    {
        rb.MovePosition(gameObject.transform.position + _movement * (moveSpeed * Time.deltaTime));
        
        //calculate the direction to target
        Vector3 targetDirection = gameObject.transform.position - _mousePosition; // this gets the current position and the mouse position, so it gives us a total distance we need to move
        Debug.Log(targetDirection);
        
        //normalize direction
        Vector3 normalizedDirection = targetDirection.normalized;
        
        //compute the necessary rotation based on the direction
        Quaternion rotation = Quaternion.LookRotation(normalizedDirection, Vector3.up);
        
        //apply the rotation to the object
        transform.rotation = rotation;
    }
}
