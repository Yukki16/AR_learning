using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI.Extensions;
using UnityEngine.XR.ARFoundation;

public class ChangeFilter : MonoBehaviour
{
    /// <summary>
    /// The hss is accessed for the on value change function that is implemented with it
    /// </summary>
    HorizontalScrollSnap hss;

    //the group with all the /    //[SerializeField] GameObject faceMaskGroupObject;

    [SerializeField] ARFaceManager faceManager;

    [SerializeField] GameObject filtersGroup;

    //A dictionary to make it easier to find the filters to activate
    // Dictionary<string , Transform> faces = new Dictionary<string , Transform>();


    [SerializeField] List<string> filterName = new List<string>();
    [SerializeField] List<GameObject> filtersPrefabs = new List<GameObject>();

    Dictionary<string, GameObject> filters = new Dictionary<string, GameObject>();

    GameObject activeFilter = null;

    void Start()
    {
        // Subscribe to face added and updated events
        //faceManager.facesChanged += OnFacesChanged;
        //Debug.Log("Subscribed");
        if (filterName.Count == filtersPrefabs.Count)
        {
            for (int i = 0; i < filterName.Count; i++)
            {
                filters.Add(filterName[i], filtersPrefabs[i]);
            }
        }
        else
        {
            Debug.LogError("The lists filterName and filtersPrefabs must be of equal length. Please check the names/prefabs of the filters");
        }


    }
    private void OnDestroy()
    {
    }


    /*void ResetFaces()
    {
        foreach (var face in faceManager.trackables)
        {
            ARFace f;
            face.TryGetComponent<ARFace>(out f);

            if(f != null)
                f = null;
        }
        Debug.Log("All faces cleared.");
    }*/
    public void OnFilterChange()
    {
        /*if (activeFilter != null)
        {
            Destroy(activeFilter);
        }

        try
        {
            ClearFaceMeshes();
            activeFilter = Instantiate(filters[hss.CurrentPageObject().name], filtersGroup.transform);
        }
        catch
        {
            activeFilter = null;
            Debug.LogWarning("NO FILTER MATCHES HSS PAGE NAME");
        }*/

        try
        {
            //faceManager.enabled = false;
            //faceManager.enabled = true;
            TurnOffRenderers();
            GameObject[] facesWithTag = GameObject.FindGameObjectsWithTag(hss.CurrentPageObject().name);
            Debug.Log(facesWithTag.Length);
            ARFaceMeshVisualizer arFace = null;

            foreach (var face in facesWithTag)
            {
                Renderer tempRend;
                face.TryGetComponent(out tempRend);
                if (tempRend != null)
                {
                    tempRend.enabled = true;
                    Debug.Log($"{tempRend.gameObject.name} renderer disabled bool: {tempRend.enabled}");
                }

                face.TryGetComponent(out arFace);
                if (arFace != null)
                {
                    arFace.enabled = true;
                }

            }
            //StartCoroutine(RefreshGroupFilters());
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            TurnOffRenderers();
        }

    }

    void ClearFaceMeshes()
    {
        foreach (var face in faceManager.trackables)
        {
            // Get the MeshFilter of the tracked face
            MeshFilter meshFilter = face.GetComponent<MeshFilter>();
            Debug.Log(meshFilter);
            if (meshFilter != null)
            {
                // Clear the mesh to make it invisible
                meshFilter.mesh = null;
            }

            // Get the ARFaceMeshVisualizer, if used, and disable it
            /*ARFaceMeshVisualizer meshVisualizer = face.GetComponent<ARFaceMeshVisualizer>();
            if (meshVisualizer != null)
            {
                meshVisualizer.
            }*/

            Debug.Log($"Cleared mesh for face: {face.trackableId}");
        }
    }

    void TurnOffRenderers()
    {
        ARFaceMeshVisualizer arFace = null;
        foreach (Renderer rend in filtersGroup.GetComponentsInChildren<Renderer>())
        {
            rend.enabled = false;
            //rend.gameObject.SetActive(false);
            //rend.gameObject.SetActive(true);
            rend.TryGetComponent(out arFace);
            if (arFace != null)
            {
                arFace.enabled = false;
            }
        }

        /*foreach (var face in faceManager.trackables)
        {
            //Destroy(face);
            *//*face.trackingState = UnityEngine.XR.ARSubsystems.TrackingState.None;
            MeshFilter mesh = face.GetComponent<MeshFilter>();
            if (mesh != null)
            {
                mesh = null;
                Debug.Log("Mesh removed for " + face.gameObject.name);
            }*//*
        }*/
        //yield return StartCoroutine(RefreshGroupFilters());
    }

    IEnumerator RefreshGroupFilters()
    {
        /*filtersGroup.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        filtersGroup.SetActive(true);*/
        faceManager.facePrefab = null;
        yield return new WaitForSeconds(0.5f);
        faceManager.facePrefab = filtersGroup;
        Debug.Log("refreshed");
    }

    private void Awake()
    {
        hss = GetComponent<HorizontalScrollSnap>();
    }


}
