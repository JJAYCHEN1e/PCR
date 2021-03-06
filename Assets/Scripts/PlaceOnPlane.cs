using System.Collections.Generic;
using Lean.Touch;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/// <summary>
/// Listens for touch events and performs an AR raycast from the screen touch point.
/// AR raycasts will only hit detected trackables like feature points and planes.
///
/// If a raycast hits a trackable, the <see cref="placedPrefab"/> is instantiated
/// and moved to the hit position.
/// </summary>
[RequireComponent(typeof(ARRaycastManager))]
public class PlaceOnPlane : MonoBehaviour
{
    public delegate void ModelPlacedHandler(); 
    public static event ModelPlacedHandler modelConfirmedEvent;
    
    public GameObject confirmModelButton;
    private bool confirmed = false;
    
    [SerializeField]
    [Tooltip("Instantiates this prefab on a plane at the touch location.")]
    GameObject m_PlacedPrefab;

    /// <summary>
    /// The prefab to instantiate on touch.
    /// </summary>
    public GameObject placedPrefab
    {
        get { return m_PlacedPrefab; }
        set { m_PlacedPrefab = value; }
    }

    /// <summary>
    /// The object instantiated as a result of a successful raycast intersection with a plane.
    /// </summary>
    public GameObject spawnedObject { get; private set; }

    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount == 2)
        {
            touchPosition = (Input.GetTouch(0).position + Input.GetTouch(0).position) / 2;
            return true;
        }

        touchPosition = default;
        return false;
    }

    public void ConfirmModelPosition()
    {
        confirmed = true;
        GameObject.Find("Model").GetComponent<LeanTwistRotateAxis>().enabled = false;

        if (modelConfirmedEvent != null)
        {
            modelConfirmedEvent();
        }
    }

    public bool IsConformed()
    {
        return confirmed;
    }

    void Update()
    {
#if UNITY_EDITOR
        ConfirmModelPosition();
#endif
        
        if (!TryGetTouchPosition(out Vector2 touchPosition) || confirmed)
            return;

        if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
        {
            // Raycast hits are sorted by distance, so the first one
            // will be the closest hit.
            var hitPose = s_Hits[0].pose;

            if (spawnedObject == null)
            {
                spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);
                SpeechController.Speak("????????????????????????????????????????????????????????????????????????");
                confirmModelButton.SetActive(true);
            }
            else
            {
                spawnedObject.transform.position = hitPose.position;
                spawnedObject.name = m_PlacedPrefab.name;
            }
        }
    }

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    ARRaycastManager m_RaycastManager;
}
