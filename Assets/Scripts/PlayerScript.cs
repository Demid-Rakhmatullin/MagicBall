using System.Linq;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody rb;
    public float force;
    public float airForce;
    public float maxSpeed;
    public float jumpForce;

    //double jump
    private byte maxJumpCount = 2;
    private byte jumpCounter;
    private byte jumpInputCounter;
    private float distanceToGround;

    public bool Empowered { get; private set; }
    private float remaingEmpowerTime;
    [SerializeField] private Material empowerMaterial;
    [SerializeField] private Material normalMaterial;

    private Transform playerCamera;

    private bool moveForward, moveBack, moveLeft, moveRight;
    //private bool grounded = true;

    void Start()
    {
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera")
            .GetComponent<Transform>();
        distanceToGround = GetComponent<Collider>().bounds.extents.y;
    }

    void FixedUpdate()
    {
        bool? grounded = null;

        if (HasMoveInputs() && GetHorizontalSpeed(out float currentSpeed) < maxSpeed)
        {
            grounded = CalcGrounded();
            var currentForce = grounded.Value ? force : airForce;
            var relSpeed = currentSpeed > 0 ? currentSpeed / maxSpeed : 0;
            currentForce *= relSpeed < 0.3f ? 0.5f : (relSpeed < 0.5f ? 0.75f : 1f);
            var cameraDir = getCameraLookDirection();

            if (moveForward)
                rb.AddForce(cameraDir * currentForce);
            if (moveBack)
                rb.AddForce(Quaternion.Euler(0, 180f, 0) * cameraDir * currentForce);
            if (moveRight)
                rb.AddForce(Quaternion.Euler(0, 90f, 0) * cameraDir * currentForce);
            if (moveLeft)
                rb.AddForce(Quaternion.Euler(0, 270f, 0) * cameraDir * currentForce);
        }

        if (jumpCounter > 0 && (grounded ?? CalcGrounded()))         
                jumpCounter = 0;

        if (!Empowered)
            while (jumpInputCounter > 0 && jumpCounter < maxJumpCount)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                jumpCounter++;
                jumpInputCounter--;
            }

        jumpInputCounter = 0;
    }

    void Update()
    {
        CheckMoveInputs();
        CheckEmpowerTime();

        if (Input.GetKeyDown(KeyCode.Space) && jumpInputCounter < maxJumpCount)
            jumpInputCounter++;

        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
    }

    private void CheckMoveInputs()
    {
        moveForward = Input.GetKey(KeyCode.W);
        moveBack = Input.GetKey(KeyCode.S);
        moveRight = Input.GetKey(KeyCode.D);
        moveLeft = Input.GetKey(KeyCode.A);
    }

    private bool HasMoveInputs()
        => moveForward || moveBack || moveLeft || moveRight;

    private bool CalcGrounded()
    {
        var result = Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 0.1f)
            || Physics.OverlapSphere(transform.position, distanceToGround + 0.1f, 1 << LayerMask.NameToLayer("Ground")).Any();
        return result;
    }

    private Vector3 getCameraLookDirection()
    {
        var cameraFwd = playerCamera.forward;
        return new Vector3(cameraFwd.x, 0, cameraFwd.z);
    }

    private Vector2 skipHeight(Vector3 vector)
        => new Vector2(vector.x, vector.z);

    public void Empower(float empowerTime)
    {
        if (remaingEmpowerTime <= 0)
        {
            transform.localScale = transform.localScale * 1.5f;
            var renderer = GetComponent<Renderer>();
            renderer.material = empowerMaterial;
            Empowered = true;
        }
        remaingEmpowerTime = empowerTime;
    }

    private float GetHorizontalSpeed(out float currentSpeed)
    {
        currentSpeed = new Vector2(rb.velocity.x, rb.velocity.z).magnitude;
        return currentSpeed;
    }

    private void CheckEmpowerTime()
    {
        if (remaingEmpowerTime > 0)
        {
            remaingEmpowerTime -= Time.deltaTime;
            if (remaingEmpowerTime <= 0)
            {
                transform.localScale = transform.localScale / 1.5f;
                var renderer = GetComponent<Renderer>();
                renderer.material = normalMaterial;
                Empowered = false;
                remaingEmpowerTime = 0;
            }
        }
    }
}
