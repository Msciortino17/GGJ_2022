using UnityEngine;

public class PlayerMovementLand : MonoBehaviour
{
    public float MovementSpeed;
    private Rigidbody _rigidbody;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            _rigidbody.AddForce(MovementSpeed, 0f, 0f,ForceMode.Impulse);
        }
    }
}