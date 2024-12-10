using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;
using UnityEngine.XR.ARFoundation;

public class ChangeFilter : MonoBehaviour
{
    /// <summary>
    /// The hss is accessed for the on value change function that is implemented with it
    /// </summary>
    HorizontalScrollSnap hss;

    //the group with all the masks for the app
    [SerializeField] GameObject faceMaskGroupObject;
    List<Transform> filters;

    [SerializeField] ARFaceManager faceManager;

    //A dictionary to make it easier to find the filters to activate
    Dictionary<string , Transform> faces = new Dictionary<string , Transform>();

    private void Awake()
    {
        faceMaskGroupObject = faceManager.facePrefab.gameObject;
        filters = new List<Transform>();

        //Adds all the mesh renderers, not necessary that i now look at it, used to create the dictionary
        for (int i = 0; i < faceMaskGroupObject.transform.childCount; i++)
        {
            filters.Add(faceMaskGroupObject.transform.GetChild(i).GetComponent<Transform>());
        }

        for (int i = 0; i < filters.Count; i++)
        {
            faces.Add(filters[i].name, filters[i]);
        }
        DeactivateFilters();

        hss = GetComponent<HorizontalScrollSnap>();
    }

    public void ChangeFilterByPage() 
    {
        //finds the new filter that needs to be active, in case of "Default" it deactivates all
        Transform facePrefabTemp;
        if(!faces.TryGetValue(hss.CurrentPageObject().name, out facePrefabTemp))
        {
            Debug.LogWarning("No filter with this name");
            DeactivateFilters();
        }
        else
        {
            DeactivateFilters();

            if (facePrefabTemp.name != "Default")
            {
                Transform outValue;
                faces.TryGetValue(facePrefabTemp.name, out outValue);

                if (outValue != null)
                {
                    outValue.transform.gameObject.SetActive(true);
                }
                else
                {
                    Debug.LogWarning("Please check spelling in the HSS object, this error is because of the spelling of a kid");
                }
            }
        }
    }

    void DeactivateFilters()
    {
        foreach (Transform filter in filters)
        {
            filter.transform.gameObject.SetActive(false);
        }
    }
    
    void EnableMeshForChildren(Transform theFilter)
    {
        for (int i = 0; i < theFilter.transform.childCount; i++) 
        {
            
        }
    }
}
