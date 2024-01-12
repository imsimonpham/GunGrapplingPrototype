using System.Net;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;


public class LefthandGun : MonoBehaviour
{
    [SerializeField] private float _range;
    [SerializeField] private GameObject _firepoint;
    [SerializeField] private GameObject _muzzleFlashPrefab;

    private LineRenderer _lineRenderer;
    private RaycastHit hit;
    private PlayerInputActions _input;

    private void Start()
    {
        //Line renderer
        _lineRenderer = gameObject.GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 2;
        _lineRenderer.startWidth = 0.002f;
        _lineRenderer.endWidth = 0.002f;

        //Input action map
        _input = new PlayerInputActions();
        _input.Lefthand.Enable();
        _input.Lefthand.Shoot.performed += Shoot;
    }
    private void Update()
    {
        //update tracer fire
        if (Physics.Raycast(_firepoint.transform.position, _firepoint.transform.forward, out hit, _range))
        {
            Vector3 endPoint = hit.point;
            RenderLine(endPoint);
        }
        else
        {
            Vector3 endPoint = _firepoint.transform.position + _firepoint.transform.forward * _range;
            RenderLine(endPoint);
        }
    }

    private void RenderLine(Vector3 endpoint)
    {
        _lineRenderer.SetPosition(0, _firepoint.transform.position);
        _lineRenderer.SetPosition(1, endpoint);
        _lineRenderer.startColor = Color.red;
        _lineRenderer.endColor = Color.red;
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        GameObject muzzleFlashGO = Instantiate(_muzzleFlashPrefab, _firepoint.transform.position, _firepoint.transform.rotation);
        Destroy(muzzleFlashGO, 0.5f);
        if (Physics.Raycast(_firepoint.transform.position, _firepoint.transform.forward, out hit, _range))
        {
            Debug.Log(hit.transform.name);
        }
        else
        {
            Debug.Log("Hitting Nothing...");
        }
    }
}
