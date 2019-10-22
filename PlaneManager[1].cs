/*============================================================================== 
Copyright (c) 2017-2018 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other 
countries.   
==============================================================================*/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Vuforia;

namespace LeoScript.MarkerLessARModule
{
    /// <summary>
    /// Manager for ground augmentation.
    /// </summary>
    public class PlaneManager : MonoBehaviour
    {
        #region PUBLIC_MEMBERS
        public PlaneFinderBehaviour m_PlaneFinder;     

        [SerializeField] private List<GameObject> placedARContentList;
        public int PlacedARContentCount
        {
            get
            {
                return placedARContentList.Count;
            }
        }

        [SerializeField] private GameObject messagePanel;


        public Text m_OnScreenMessage;

        public CanvasGroup m_ScreenReticleGround;
        public GameObject m_TranslationIndicator;
        public GameObject m_RotationIndicator;

        public Transform Floor;       

        [SerializeField] private LayerMask floorLayerMask;


        #endregion // PUBLIC_MEMBERS


        #region PRIVATE_MEMBERS

        const string unsupportedDeviceTitle = "Unsupported Device";
        const string unsupportedDeviceBody =
            "This device has failed to start the Positional Device Tracker. " +
            "Please check the list of supported Ground Plane devices on our site: " +
            "\n\nhttps://library.vuforia.com/articles/Solution/ground-plane-supported-devices.html";

        const string EmulatorGroundPlane = "Emulator Ground Plane";   

        StateManager m_StateManager;
        SmartTerrain m_SmartTerrain;
        PositionalDeviceTracker m_PositionalDeviceTracker;
     
        int AutomaticHitTestFrameCount;

        GraphicRaycaster m_GraphicRayCaster;
        PointerEventData m_PointerEventData;
        EventSystem m_EventSystem;

        Camera mainCamera;
       
        #endregion // PRIVATE_MEMBERS


        #region MONOBEHAVIOUR_METHODS

        void Start()
        {
            VuforiaARController.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);        
            DeviceTrackerARController.Instance.RegisterTrackerStartedCallback(OnTrackerStarted);

            m_PlaneFinder.HitTestMode = HitTestMode.AUTOMATIC;
            // Enable floor collider if running on device; Disable if running in PlayMode
            Floor.gameObject.SetActive(!VuforiaRuntimeUtilities.IsPlayMode());

            mainCamera = Camera.main;

            m_GraphicRayCaster = FindObjectOfType<GraphicRaycaster>();
            m_EventSystem = FindObjectOfType<EventSystem>();

            m_PlaneFinder.gameObject.SetActive(true);
        }



        private void HandleGesture()
        {
            MarkerLessARContent content;

            if (MarkerLessARModuleView.Instance.OperatingARContent != null)
            {
                if (TouchHandler.IsSingleFingerTapped())
                {
                    if(TappingOnARContent(out content) && !IsCanvasButtonPressed())
                    {
                        if (content == MarkerLessARModuleView.Instance.OperatingARContent)
                        {
                            content.PlaceToGround();
                            MarkerLessARModuleView.Instance.OperatingARContent = null;
                        }
                        else
                        {
                            MarkerLessARModuleView.Instance.OperatingARContent.PlaceToGround();
                            content.Hover();

                            MarkerLessARModuleView.Instance.OperatingARContent = content;
                        }
                    }
                }
                else if ((VuforiaRuntimeUtilities.IsPlayMode() && (Input.GetMouseButton(0)) && TouchHandler.TouchDuration() > 0.1f) 
                         || TouchHandler.IsSingleFingerDragging() && !IsCanvasButtonPressed())
                {
                    GameObject operatingObject = MarkerLessARModuleView.Instance.OperatingARContent.gameObject;

                    AddIndicatorsToGameObject(operatingObject);

                    m_RotationIndicator.SetActive(Input.touchCount == 2);
                    m_TranslationIndicator.SetActive(true);                       

                    MoveModelToTouchPoint(operatingObject);                       

                }
            }
            // only operation can do is pick up something.
            else
            {
                if (TouchHandler.IsSingleFingerTapped() && !IsCanvasButtonPressed())
                {           
                    if (TappingOnARContent(out content))
                    {      
                        RemoveIndicators();      
                        content.Hover();   
                        MarkerLessARModuleView.Instance.OperatingARContent = content;
                        AddIndicatorsToGameObject(content.gameObject);                              
                    }                
                }
            }     

            if (Input.touchCount == 0 && !Input.GetMouseButton(0))
            {
                RemoveIndicators();
            }
        }

        private void MoveModelToTouchPoint(GameObject model )
        {
            Ray ray;
            RaycastHit hit;

            ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 10000.0f, floorLayerMask))
            {
                if (hit.collider.gameObject.name == Floor.name)
                {                          
                    model.PositionAt(hit.point);                                            
                }
            }
        }

        private bool FoundGround
        {
            get
            {
                return m_ScreenReticleGround.alpha == 0.0f;
            }
        }

        void LateUpdate()
        {
            if (AutomaticHitTestFrameCount == Time.frameCount)
            {


                if(PlacedARContentCount > 0)
                {
                    m_ScreenReticleGround.alpha = 0;
                    SetSurfaceIndicatorVisible(false);
                    HideMessage();
                }
                else if (PlacedARContentCount == 0)
                {
                    // We got an automatic hit test this frame

                    // Hide the onscreen reticle when we get a hit test

                    m_ScreenReticleGround.alpha = 0;

                    // Set visibility of the surface indicator
                    SetSurfaceIndicatorVisible(true);

                    HideMessage();
                }
            }
            else
            {                
                m_ScreenReticleGround.alpha = 1;

                SetSurfaceIndicatorVisible(false); 

                ShowMessage("Point device towards ground");
            }

            HandleGesture();
        }

        public void ShowMessage(string msg)
        {       
            messagePanel.SetActive(true);
            m_OnScreenMessage.enabled = true;
            m_OnScreenMessage.text = msg; 
        }

        private void HideMessage()
        {
            m_OnScreenMessage.enabled = false;
            messagePanel.SetActive(false);
        }

        void OnDestroy()
        {
            VuforiaARController.Instance.UnregisterVuforiaStartedCallback(OnVuforiaStarted);
     
            DeviceTrackerARController.Instance.UnregisterTrackerStartedCallback(OnTrackerStarted);                
        }

        #endregion // MONOBEHAVIOUR_METHODS

        #region GROUNDPLANE_CALLBACKS

        private HitTestResult autoHitResult;

        public void HandleAutomaticHitTest(HitTestResult result)
        {
            AutomaticHitTestFrameCount = Time.frameCount;

            if (FoundGround)
            {
                autoHitResult = result;         
            }
            else
            {
                autoHitResult = null;
            }
        }  

        public void HandleInteractiveHitTest(HitTestResult result)
        {
       
        }

        private void RemoveIndicators()
        {
            m_RotationIndicator.SetActive(false);
            m_TranslationIndicator.SetActive(false);

            m_RotationIndicator.transform.parent = null;
            m_TranslationIndicator.transform.parent = null;

        }

        private void AddIndicatorsToGameObject(GameObject go)
        {
            m_RotationIndicator.transform.parent = go.transform;
            m_TranslationIndicator.transform.parent = go.transform;

            m_RotationIndicator.transform.localPosition = Vector3.zero;
            m_TranslationIndicator.transform.localPosition = Vector3.zero;   

        }

        private bool TappingOnARContent(out MarkerLessARContent content)
        {               
            RaycastHit hit;

            Ray cameraToPlaneRay = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(cameraToPlaneRay, out hit))           
            {
                if (hit.collider.gameObject.GetComponentInParent<MarkerLessARContent>() != null)
                {  
                    content = hit.collider.gameObject.GetComponentInParent<MarkerLessARContent>();

                    return true;
                }             
            }    

            content = null;

            return false;
        }

        #endregion // GROUNDPLANE_CALLBACKS


        #region PUBLIC_BUTTON_METHODS                 
     
        public void ResetTrackers()
        {
            Debug.Log("ResetTrackers() called.");

            m_SmartTerrain = TrackerManager.Instance.GetTracker<SmartTerrain>();
            m_PositionalDeviceTracker = TrackerManager.Instance.GetTracker<PositionalDeviceTracker>();

            // Stop and restart trackers                
            m_SmartTerrain.Stop(); // stop SmartTerrain tracker before PositionalDeviceTracker
            m_PositionalDeviceTracker.Stop();
            m_PositionalDeviceTracker.Start();
            m_SmartTerrain.Start(); // start SmartTerrain tracker after PositionalDeviceTracker
        }

        #endregion // PUBLIC_BUTTON_METHODS

        #region PRIVATE_METHODS       

        void SetSurfaceIndicatorVisible(bool isVisible)
        {
            Renderer[] renderers = m_PlaneFinder.PlaneIndicator.GetComponentsInChildren<Renderer>(true);
            Canvas[] canvas = m_PlaneFinder.PlaneIndicator.GetComponentsInChildren<Canvas>(true);

            foreach (Canvas c in canvas)
                c.enabled = isVisible;

            foreach (Renderer r in renderers)
                r.enabled = isVisible;
        }             

        bool IsCanvasButtonPressed()
        {
            List<RaycastResult> results = GetCanvasButtonResult();

            bool resultIsButton = false;
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.GetComponentInParent<Toggle>() ||
                    result.gameObject.GetComponent<Button>())
                {
                    resultIsButton = true;
                    break;
                }
            }
            return resultIsButton;
        }

        private List<RaycastResult> GetCanvasButtonResult()
        {
            m_PointerEventData = new PointerEventData(m_EventSystem)
                {
                    position = Input.mousePosition
                };
            List<RaycastResult> results = new List<RaycastResult>();
            m_GraphicRayCaster.Raycast(m_PointerEventData, results); 

            return results;
        }

        

        #endregion // PRIVATE_METHODS


        #region VUFORIA_CALLBACKS

        void OnVuforiaStarted()
        {
            Debug.Log("OnVuforiaStarted() called.");

            m_StateManager = TrackerManager.Instance.GetStateManager();

            // Check trackers to see if started and start if necessary
            m_PositionalDeviceTracker = TrackerManager.Instance.GetTracker<PositionalDeviceTracker>();
            m_SmartTerrain = TrackerManager.Instance.GetTracker<SmartTerrain>();

            if (m_PositionalDeviceTracker != null && m_SmartTerrain != null)
            {
                if (!m_PositionalDeviceTracker.IsActive)
                    m_PositionalDeviceTracker.Start();
                if (m_PositionalDeviceTracker.IsActive && !m_SmartTerrain.IsActive)
                    m_SmartTerrain.Start();
            }
            else
            {
                if (m_PositionalDeviceTracker == null)
                    Debug.Log("PositionalDeviceTracker returned null. GroundPlane not supported on this device.");
                if (m_SmartTerrain == null)
                    Debug.Log("SmartTerrain returned null. GroundPlane not supported on this device.");

                MessageBox.DisplayMessageBox(unsupportedDeviceTitle, unsupportedDeviceBody, false, null);
            }
        }

        #endregion // VUFORIA_CALLBACKS


        #region DEVICE_TRACKER_CALLBACKS

        void OnTrackerStarted()
        {
            Debug.Log("OnTrackerStarted() called.");

            m_PositionalDeviceTracker = TrackerManager.Instance.GetTracker<PositionalDeviceTracker>();
            m_SmartTerrain = TrackerManager.Instance.GetTracker<SmartTerrain>();

            if (m_PositionalDeviceTracker != null)
            {
                if (!m_PositionalDeviceTracker.IsActive)
                    m_PositionalDeviceTracker.Start();

                Debug.Log("PositionalDeviceTracker is Active?: " + m_PositionalDeviceTracker.IsActive +
                    "\nSmartTerrain Tracker is Active?: " + m_SmartTerrain.IsActive);
            }
        }


        #endregion // DEVICE_TRACKER_CALLBACK_METHODS    



        public void SelectPendingModel(GameObject model)
        {   
            
            #if !UNITY_EDITOR && ( UNITY_IOS || UNITY_ANDROID )

            if (FoundGround)
                
            #endif
            {
                if (placedARContentList.Contains(model))
                {
                  //  RemoveModelFromScreen(model);

                    model.transform.position  = autoHitResult.Position;
                    model.transform.rotation = Quaternion.identity;
                    model.transform.localScale = Vector3.one;

                    model.GetComponent<MarkerLessARContent>().ComeOut();

                    return;
                }

                if (MarkerLessARModuleView.Instance.OperatingARContent != null)
                {
                    RemoveModelFromScreen(MarkerLessARModuleView.Instance.OperatingARContent.gameObject);
                }


                if (placedARContentList.Count < 2)
                {
                    MarkerLessARModuleView.Instance.OperatingARContent =  model.GetComponent<MarkerLessARContent>();            

                    model.SetActive(true);

                    model.transform.position = autoHitResult.Position;

                    Floor.gameObject.SetActive(true);

                    Floor.position = autoHitResult.Position;

                    placedARContentList.Add(model);

                    MarkerLessARModuleView.Instance.OperatingARContent.ComeOut();

                }
            }
        }      

        public void ResetModel()
        {        
            GameObject operatingObject = MarkerLessARModuleView.Instance.OperatingARContent.gameObject;

            operatingObject.transform.position  = autoHitResult.Position;
            operatingObject.transform.rotation = Quaternion.identity;
            operatingObject.transform.localScale = Vector3.one;
        }

        public void RemoveModelFromScreen(GameObject go)
        {
            go.transform.parent = null;
            
            placedARContentList.Remove(go);           

            go.SetActive(false);

            if (placedARContentList.Count == 1)
            {
                MarkerLessARModuleView.Instance.OperatingARContent = placedARContentList[0].GetComponent<MarkerLessARContent>();
            }
            else
            {
                MarkerLessARModuleView.Instance.OperatingARContent = null;
            }
        }
    }
}