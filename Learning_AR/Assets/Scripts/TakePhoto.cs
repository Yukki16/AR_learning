using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class TakePhoto : MonoBehaviour
{
    int fileCounter = 0;
    Camera Camera
    {
        get
        {
            if (!_camera)
            {
                _camera = Camera.main;
            }
            return _camera;
        }
    }
    [SerializeField]  Camera _camera;
    [SerializeField] RenderTexture _texture;


    public void Capture()
    {
        //_texture = RenderTexture.active;
        //RenderTexture.active = _camera.targetTexture;
        _camera.targetTexture = _texture;

        _camera.Render();

        Texture2D image = new Texture2D(_camera.targetTexture.width, _camera.targetTexture.height);
        image.ReadPixels(new Rect(0, 0, _camera.targetTexture.width, _camera.targetTexture.height), 0, 0);
        image.Apply();
        //RenderTexture.active = activeRenderTexture;

        //byte[] bytes = image.EncodeToPNG();
        //Destroy(image);

        NativeGallery.SaveImageToGallery(image, "AR", "AR_filter" + fileCounter);
        fileCounter++;

        _camera.targetTexture = null;
    }
}
