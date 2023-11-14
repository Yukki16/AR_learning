using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;
using UnityEngine.XR.ARFoundation;

public class ChangeFilter : MonoBehaviour
{
    HorizontalScrollSnap hss;
    [SerializeField] ARFaceManager faceManager;

    [System.Serializable] struct faceMasksPrefabs {
        public string ID;
        public GameObject facePrefab; 
    }

    [SerializeField] faceMasksPrefabs[] prefabs;

    Dictionary<string , GameObject> faces = new Dictionary<string , GameObject>();
    private void Start()
    {
        //faceMasksPrefabs = new Dictionary<string, GameObject>();
        for (int i = 0; i < prefabs.Length; i++)
        {
            faces.Add(prefabs[i].ID, prefabs[i].facePrefab);
        }
        hss = GetComponent<HorizontalScrollSnap>();
    }

    public void ChangeFilterByPage() 
    {
        GameObject facePrefabTemp;
        if(!faces.TryGetValue(hss.CurrentPageObject().name, out facePrefabTemp))
        {
            Debug.LogWarning("No filter with this name");
        }
        else
        {
            faceManager.facePrefab = facePrefabTemp;
        }
    }
}
