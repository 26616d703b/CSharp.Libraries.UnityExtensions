using System.Collections;
using UnityEngine;
using UnityExtensions.Characters;
using UnityExtensions.Motion.Controllers;
using UnityExtensions.UI.HUD.Markers.Waypoint;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.ImageEffects;
using UnityStandardAssets.Utility;

namespace UnityExtensions.Cameras.Controllers
{
    public class MultiPOVCameraController : MonoBehaviour
    {
        #region Attributes

        public static MultiPOVCameraController current { get; set; }

        [SerializeField]
        private Camera m_firstPersonCamera;

        [SerializeField]
        private Camera m_thirdPersonCamera;

        [SerializeField]
        [Range(0f, 3f)]
        private readonly float m_transitionTime = 3f;

        [SerializeField]
        private Player.Camera.POV m_pointOfView;

        public bool useFOVKick;

        [SerializeField]
        private readonly FOVKick m_fovKick = new FOVKick();

        public bool useMouseLook = true;

        [SerializeField]
        private MouseLook m_mouseLook;

        private bool m_isStaticOrMovingSlowly = true;

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        public Camera currentCamera { get; private set; }

        public Camera firstPersonCamera
        {
            get { return m_firstPersonCamera; }
        }

        public Camera thirdPersonCamera
        {
            get { return m_thirdPersonCamera; }
        }

        public float focusDistance { get; private set; }

        public Vector3 focusPoint
        {
            get { return currentCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)).GetPoint(focusDistance); }
            set { currentCamera.transform.LookAt(value); }
        }

        public Player.Camera.POV pointOfView
        {
            get { return m_pointOfView; }
        }

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        private void Awake()
        {
            m_firstPersonCamera.GetComponent<DepthOfField>().enabled =
                bool.Parse(PlayerPrefs.GetString(Player.Preferences.Display.DepthOfField, bool.FalseString));

            m_firstPersonCamera.GetComponent<CameraMotionBlur>().enabled =
                bool.Parse(PlayerPrefs.GetString(Player.Preferences.Display.MotionBlur, bool.FalseString));

            m_firstPersonCamera.GetComponent<BloomOptimized>().enabled =
                bool.Parse(PlayerPrefs.GetString(Player.Preferences.Display.Bloom, bool.FalseString));

            m_thirdPersonCamera.GetComponent<DepthOfField>().enabled =
                bool.Parse(PlayerPrefs.GetString(Player.Preferences.Display.DepthOfField, bool.FalseString));

            m_thirdPersonCamera.GetComponent<CameraMotionBlur>().enabled =
                bool.Parse(PlayerPrefs.GetString(Player.Preferences.Display.MotionBlur, bool.FalseString));

            m_thirdPersonCamera.GetComponent<BloomOptimized>().enabled =
                bool.Parse(PlayerPrefs.GetString(Player.Preferences.Display.Bloom, bool.FalseString));
        }

        private void OnEnable()
        {
            current = this;
        }

        // Use this for initialization
        private void Start()
        {
            switch (m_pointOfView)
            {
                case Player.Camera.POV.FirstPerson:

                    m_thirdPersonCamera.gameObject.SetActive(false);

                    currentCamera = m_firstPersonCamera;
                    currentCamera.gameObject.SetActive(true);

                    break;

                case Player.Camera.POV.ThirdPerson:

                    m_firstPersonCamera.gameObject.SetActive(false);

                    currentCamera = m_thirdPersonCamera;
                    currentCamera.gameObject.SetActive(true);

                    break;

                default:
                    break;
            }

            focusDistance = currentCamera.farClipPlane;

            m_fovKick.Setup(currentCamera);
            m_mouseLook.Init(transform.parent, currentCamera.transform);

            WaypointSystem.current.targetCamera = currentCamera;
        }

        private void Update()
        {
            if (CrossPlatformInputManager.GetButtonDown("Switch Camera"))
            {
                m_pointOfView = m_pointOfView == Player.Camera.POV.FirstPerson
                    ? Player.Camera.POV.ThirdPerson
                    : Player.Camera.POV.FirstPerson;

                SwitchPointOfView(m_pointOfView);
            }

            if (useFOVKick)
            {
                bool wasStaticMovingSlowly = m_isStaticOrMovingSlowly;
                m_isStaticOrMovingSlowly = PlayerMotionController.current.isIdling ||
                                           PlayerMotionController.current.isWalking;

                if (m_isStaticOrMovingSlowly != wasStaticMovingSlowly)
                {
                    StopAllCoroutines();
                    StartCoroutine(!m_isStaticOrMovingSlowly ? m_fovKick.FOVKickUp() : m_fovKick.FOVKickDown());
                }
            }

            if (useMouseLook)
            {
                // Rotate view
                m_mouseLook.LookRotation(transform.parent, currentCamera.transform);
            }
        }

        #endregion Inherited Methods

        #region Events

        //_____________________________________________________________________________ EVENTS _____________________________________________________________________________

        public delegate void MoveCameraEventHandler();

        /*public event MoveCameraEventHandler OnMoveCameraStart;
        public event MoveCameraEventHandler OnMoveCameraEnd;*/

        #endregion Events

        #region Other Methods

        //__________________________________________________________________________ OTHER METHODS _________________________________________________________________________

        public void SwitchPointOfView(Player.Camera.POV pointOfView)
        {
            switch (m_pointOfView)
            {
                case Player.Camera.POV.FirstPerson:

                    currentCamera = m_firstPersonCamera;
                    currentCamera.transform.position = m_thirdPersonCamera.transform.position;
                    currentCamera.transform.rotation = m_thirdPersonCamera.transform.rotation;
                    currentCamera.gameObject.SetActive(true);

                    m_thirdPersonCamera.gameObject.SetActive(false);

                    MoveCamera(Player.Camera.Anchor.FirstPerson, m_transitionTime);

                    break;

                case Player.Camera.POV.ThirdPerson:

                    currentCamera = m_thirdPersonCamera;
                    currentCamera.transform.position = m_firstPersonCamera.transform.position;
                    currentCamera.transform.rotation = m_firstPersonCamera.transform.rotation;
                    currentCamera.gameObject.SetActive(true);

                    m_firstPersonCamera.gameObject.SetActive(false);

                    MoveCamera(Player.Camera.Anchor.ThirdPerson, m_transitionTime);

                    break;

                default:
                    break;
            }

            m_fovKick.Setup(currentCamera);
            WaypointSystem.current.targetCamera = currentCamera;
        }

        public void MoveCamera(Vector3 destination)
        {
            StopAllCoroutines();
            StartCoroutine(MoveCamera_Coroutine(destination, m_transitionTime));
        }

        public void MoveCamera(Vector3 destination, float time)
        {
            StopAllCoroutines();
            StartCoroutine(MoveCamera_Coroutine(destination, time));
        }

        private IEnumerator MoveCamera_Coroutine(Vector3 destination, float time)
        {
            float timer = 0f;
            Vector3 from = currentCamera.transform.localPosition;

            //OnMoveCameraStart();

            while (timer <= time)
            {
                float t = 1f + Mathf.Pow(timer / time - 1f, 3f);
                currentCamera.transform.localPosition = Vector3.Lerp(from, destination, t);
                t += Time.deltaTime;

                timer += Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }

            currentCamera.transform.localPosition = destination;

            //OnMoveCameraDone();
        }

        #endregion Other Methods

        #endregion Methods
    }
}