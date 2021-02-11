using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class VRController : MonoBehaviour
{
    private Color TransparentColor = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    private Color CeilingColor = new Color(0.24f, 0.24f, 0.24f, 1.0f);
    private Color CeilingLightColor = new Color(0.85f, 0.85f, 0.85f, 1.0f);

    private GameObject[] ceilingTiles = null;
    private GameObject[] ceilingLights = null;

    public float m_Sensitivity = 0.1f;
    public float m_MaxSpeed = 1.0f;

    public SteamVR_Action_Boolean m_MovePress = null;
    public SteamVR_Action_Vector2 m_MoveValue = null;

    public SteamVR_Action_Boolean m_AButtonPress = null;

    public Camera FirstPersonCamera = null;
    public Camera OverheadCamera = null;

    private float m_Speed = 0.0f;

    private bool m_OverheadView = false;

    private CharacterController m_CharacterController = null;
    private Transform m_CameraRig = null;
    private Transform m_Head = null;

    private void Awake()
    {
        m_CharacterController = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        m_CameraRig = SteamVR_Render.Top().origin;
        m_Head = SteamVR_Render.Top().head;
        FirstPersonCamera.enabled = !m_OverheadView;
        OverheadCamera.enabled = m_OverheadView;

        ceilingTiles = GameObject.FindGameObjectsWithTag("ceiling");
        ceilingLights = GameObject.FindGameObjectsWithTag("ceiling_light");
    }

    // Update is called once per frame
    private void Update()
    {
        HandleHead();
        HandleHeight();
        CheckCameraToggle();
        CalculateMovement();

        //PrintInfo();
    }

    private void HandleHead()
    {
        // store
        Vector3 oldPosition = m_CameraRig.position;
        Quaternion oldRotation = m_CameraRig.rotation;

        // transform
        transform.eulerAngles = new Vector3(0.0f, m_Head.rotation.eulerAngles.y, 0.0f);

        // restore
        m_CameraRig.position = oldPosition;
        m_CameraRig.rotation = oldRotation;
    }

    private void CalculateMovement()
    {
        // decide movement orientation
        Vector3 orientationEuler = new Vector3(0, transform.eulerAngles.y, 0);
        Quaternion orientation = Quaternion.Euler(orientationEuler);
        Vector3 movement = Vector3.zero;

        // handle not moving
        if (m_MovePress.GetStateUp(SteamVR_Input_Sources.Any))
        {
            m_Speed = 0;
        }

        // handle pressed button
        if (m_MovePress.state)
        {
            // Add and clamp speed
            m_Speed += m_MoveValue.axis.y * m_Sensitivity;
            m_Speed = Mathf.Clamp(m_Speed, -m_MaxSpeed, m_MaxSpeed);

            // orientation
            movement += orientation * (m_Speed * Vector3.forward) * Time.deltaTime;
        }

        // apply
        m_CharacterController.Move(movement);
    }

    private void HandleHeight()
    {
        // get head in local space
        float headHeight = Mathf.Clamp(m_Head.localPosition.y, 1, 2);
        m_CharacterController.height = headHeight;

        // cut height in half
        Vector3 newCenter = Vector3.zero;
        newCenter.y = m_CharacterController.height / 2;
        newCenter.y += m_CharacterController.skinWidth;

        // move capsule in local space
        newCenter.x = m_Head.localPosition.x;
        newCenter.z = m_Head.localPosition.z;

        // rotate capsule
        newCenter = Quaternion.Euler(0, -transform.eulerAngles.y, 0) * newCenter;

        // apply
        m_CharacterController.center = newCenter;
    }

    private void PrintInfo()
    {
        Debug.Log(m_OverheadView.ToString());
    }

    private void CheckCameraToggle()
    {
        if (m_AButtonPress.stateUp)
        {
            m_OverheadView = !m_OverheadView;
            SwitchViews();
            Debug.Log("m_OverheadView toggled to " + m_OverheadView.ToString() + " at " + Time.time.ToString());
        }
    }

    private void SwitchViews()
    {
        FirstPersonCamera.enabled = !m_OverheadView;
        OverheadCamera.enabled = m_OverheadView;

        Vector3 invisible = new Vector3(0, 0, 0);
        Vector3 visible = new Vector3(1, 1, 1);

        if (m_OverheadView)
        {
            foreach (GameObject obj in ceilingTiles)
                obj.GetComponent<MeshRenderer>().enabled = false;
        } else
        {
            foreach (GameObject obj in ceilingTiles)
                obj.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
