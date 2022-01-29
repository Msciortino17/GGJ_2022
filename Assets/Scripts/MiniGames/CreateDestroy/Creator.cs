using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : MonoBehaviour
{
    public float Speed = 500.0f;

    // Start is called before the first frame update
    void Start() {
        mRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        var dt = Time.deltaTime;
        HandleInput();
    }

    void FixedUpdate() {
        var fdt = Time.fixedDeltaTime;
        FixedHandleMovement(fdt);
    }

    void HandleInput() {

        Vector3 movementDir = new Vector3();

        if (Input.GetKey("w")) movementDir += new Vector3(0,1,0);
        if (Input.GetKey("s")) movementDir += new Vector3(0,-1,0);

        if (Input.GetKey("a")) movementDir += new Vector3(-1,0,0);
        if (Input.GetKey("d")) movementDir += new Vector3(1,0,0);

        mInputDir = movementDir.normalized;
    }

    void FixedHandleMovement(float fdt) {
        if (mInputDir.magnitude > 0) {
            float speed = Speed * fdt;
            mRigidbody.AddForce(mInputDir * speed, ForceMode.VelocityChange);
        }
    }

    private Rigidbody mRigidbody;
    private Vector3 mInputDir;
}
