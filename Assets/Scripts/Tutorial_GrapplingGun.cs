using UnityEngine;
using UnityEngine.InputSystem;

public class Tutorial_GrapplingGun : MonoBehaviour
{
    public bool isEnabled = true;
    
    private enum CharType
	{
        Blue,
        Red
	}

    [SerializeField]
    private CharType charType;

    [Header("Scripts Ref:")]
    public Tutorial_GrapplingRope grappleRope;

    [Header("Layers Settings:")]
    [SerializeField] private bool grappleToAll = false;
    [SerializeField] private int grappableLayerNumber = 9;
    [SerializeField] private LayerMask layerMask;

    [Header("Main Camera:")]
    public Camera m_camera;

    [Header("Transform Ref:")]
    public Transform gunHolder;
    public Transform gunPivot;
    public Transform firePoint;

    [Header("Physics Ref:")]
    public SpringJoint2D m_springJoint2D;
    public Rigidbody2D m_rigidbody;

    [Header("Rotation:")]
    [SerializeField] private bool rotateOverTime = true;
    [Range(0, 60)] [SerializeField] private float rotationSpeed = 4;

    [Header("Distance:")]
    [SerializeField] private bool hasMaxDistance = false;
    [SerializeField] private float maxDistnace = 20;

    private enum LaunchType
    {
        Transform_Launch,
        Physics_Launch
    }

    [Header("Launching:")]
    [SerializeField] private bool launchToPoint = true;
    [SerializeField] private LaunchType launchType = LaunchType.Physics_Launch;
    [SerializeField] private float launchSpeed = 1;

    [Header("No Launch To Point")]
    [SerializeField] private bool autoConfigureDistance = false;
    [SerializeField] private float targetDistance = 3;
    [SerializeField] private float targetFrequncy = 1;

    [HideInInspector] public Vector2 grapplePoint;
    [HideInInspector] public Vector2 grappleDistanceVector;

    [SerializeField]
    private LayerMask playerMask;

    private GrappleManager _gm;

    private void Start()
    {
        grappleRope.enabled = false;
        m_springJoint2D.enabled = false;
        _gm = transform.parent.GetComponentInParent<GrappleManager>();

        playerMask = ~(1 << gameObject.layer) & ~(1 << 10);
        // playerMask = ~(1 << gameObject.layer);

        // Physics2D.IgnoreLayerCollision(gameObject.layer, 10);
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            ReleaseGrapplingHook();
        }

        if (!isEnabled)
            return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        // if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SetGrapplePoint();
        }
        else if(Mouse.current.leftButton.isPressed)
        //else if (Input.GetKey(KeyCode.Mouse0))
        {
            if (grappleRope.enabled)
            {
                RotateGun(grapplePoint, false);
            }
            else
            {
                // Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos = m_camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                RotateGun(mousePos, true);
            }

            if (launchToPoint && grappleRope.isGrappling)
            {
                if (launchType == LaunchType.Transform_Launch)
                {
                    Vector2 firePointDistnace = firePoint.position - gunHolder.localPosition;
                    Vector2 targetPos = grapplePoint - firePointDistnace;
                    gunHolder.position = Vector2.Lerp(gunHolder.position, targetPos, Time.deltaTime * launchSpeed);
                }
            }
        }
        /*else if (Mouse.current.leftButton.wasReleasedThisFrame)
        //else if (Input.GetKeyUp(KeyCode.Mouse0))
        {

            if (charType == CharType.Blue)
            {
                _gm.OnBlueGrapple();
            }
            else
            {
                _gm.OnRedGrapple();
            }
        }*/
        else
        {
            // Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos = m_camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            RotateGun(mousePos, true);
        }
    }

    public void ReleaseGrapplingHook()
	{
        grappleRope.enabled = false;
        m_springJoint2D.enabled = false;
        m_rigidbody.gravityScale = 1;
    }

    void RotateGun(Vector3 lookPoint, bool allowRotationOverTime)
    {
        Vector3 distanceVector = lookPoint - gunPivot.position;

        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        if (rotateOverTime && allowRotationOverTime)
        {
            gunPivot.rotation = Quaternion.Lerp(gunPivot.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
        }
        else
        {
            gunPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void SetGrapplePoint()
    {
        // Vector2 distanceVector = m_camera.ScreenToWorldPoint(Input.mousePosition) - gunPivot.position;
        Vector2 distanceVector = m_camera.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - gunPivot.position;
        RaycastHit2D _hit = Physics2D.Raycast(firePoint.position, distanceVector.normalized, maxDistnace, playerMask);
        // if (Physics2D.Raycast(firePoint.position, distanceVector.normalized, maxDistnace, playerMask))
        if(_hit)
        {
            // RaycastHit2D _hit = Physics2D.Raycast(firePoint.position, distanceVector.normalized, maxDistnace, playerMask);
            if((layerMask.value & (1 << _hit.transform.gameObject.layer)) > 0)
            // if (_hit.transform.gameObject.layer == grappableLayerNumber || grappleToAll)
            {
                if (Vector2.Distance(_hit.point, firePoint.position) <= maxDistnace || !hasMaxDistance)
                {
                    grapplePoint = _hit.point;
                    grappleDistanceVector = grapplePoint - (Vector2)gunPivot.position;
                    grappleRope.enabled = true;
                }
            }
        }
    }

    public void Grapple()
    {
        if (charType == CharType.Blue)
        {
            _gm.OnBlueGrappleStart();
        }
        else
        {
            _gm.OnRedGrappleStart();
        }
        m_springJoint2D.autoConfigureDistance = false;
        if (!launchToPoint && !autoConfigureDistance)
        {
            m_springJoint2D.distance = targetDistance;
            m_springJoint2D.frequency = targetFrequncy;
        }
        if (!launchToPoint)
        {
            if (autoConfigureDistance)
            {
                m_springJoint2D.autoConfigureDistance = true;
                m_springJoint2D.frequency = 0;
            }

            m_springJoint2D.connectedAnchor = grapplePoint;
            m_springJoint2D.enabled = true;
        }
        else
        {
            switch (launchType)
            {
                case LaunchType.Physics_Launch:
                    m_springJoint2D.connectedAnchor = grapplePoint;

                    Vector2 distanceVector = firePoint.position - gunHolder.position;

                    m_springJoint2D.distance = distanceVector.magnitude;
                    m_springJoint2D.frequency = launchSpeed;
                    m_springJoint2D.enabled = true;
                    break;
                case LaunchType.Transform_Launch:
                    m_rigidbody.gravityScale = 0;
                    m_rigidbody.velocity = Vector2.zero;
                    break;
            }
        }

        if (charType == CharType.Blue)
        {
            _gm.OnBlueGrapple();
        }
        else
        {
            _gm.OnRedGrapple();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (firePoint != null && hasMaxDistance)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(firePoint.position, maxDistnace);
        }
    }

}
