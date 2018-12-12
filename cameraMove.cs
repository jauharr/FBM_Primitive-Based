using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class cameraMove : MonoBehaviour
{

    //public Transform target;
    public float speed = 1.5f;
    private float x = 0.0f;
    private float y = 0.0f;
    private Touch touch;
    private  Vector3 dragOrigin;
    private float maxx = 200;
    private float minx = -200;
    private float maxy = 200;
    private float miny = -200;
    public int editmode;
    public Camera sketchcam;
    public Camera extcam;
    Vector3 touchStart;
    public float zoomOutMin = 1;
    public float zoomOutMax = 20;
    float sensitivity = 10f;
    private float init;
    //private Vector3 distance;
    // Use this for initialization
    
 

    public void editingmode(int mode)
    {
        editmode = mode;
        //Debug.Log("here");
    }
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
        
    }
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
    void Update()
    {
        
        if (editmode == 1)
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                touchStart = sketchcam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, sketchcam.nearClipPlane));
                
            }
            if (Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

                float difference = currentMagnitude - prevMagnitude;

                zoom(difference * 0.01f);
            }
            else if (Input.GetMouseButton(0))
            {
                Vector3 direction = touchStart - sketchcam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, sketchcam.nearClipPlane));
                sketchcam.transform.position += direction;
                //Debug.Log(direction);
            }
            zoom(Input.GetAxis("Mouse ScrollWheel"));
            //Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
        }
        if(editmode == 2)
        {
            if (Input.GetMouseButtonDown(0) )
            {
                touchStart = sketchcam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, sketchcam.nearClipPlane));

            }
            if (Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

                float difference = currentMagnitude - prevMagnitude;

                
                extcam.transform.position = extzoom(difference * 0.001f);
            }
            else if (Input.GetMouseButton(0) && Input.touchCount == 1 && !IsPointerOverUIObject())
            {
                //Version1
                float x_ = Input.GetAxis("Mouse X");
                float y_ = Input.GetAxis("Mouse Y");
                Vector3 camrotation = extcam.transform.eulerAngles;
                camrotation.x += y_ * 2f;
                camrotation.y -= x_ * 2f;
                extcam.transform.rotation = Quaternion.Euler(camrotation.x,camrotation.y,0);

                //version2
     
            }
            Vector3 pos = extzoom(Input.GetAxis("Mouse ScrollWheel"));
            //istance -= delta * (distance / MAX_CAM_DISTANCE) * (zoomSpeed * 1000) * Time.deltaTime;
            //distance = Mathf.Clamp(distance, MIN_CAM_DISTANCE, MAX_CAM_DISTANCE);
            extcam.transform.position = pos;
            extcam.transform.position = extcam.transform.rotation * (Vector3.forward * -pos.magnitude);//Move the main camera
        }




        //y = ClampAngle(y, yMinLimit, yMaxLimit);
        //Quaternion rotation = Quaternion.Euler(y, x, 0);

        //transform.rotation = rotation;
       


    }
    void zoom(float increment)
    {
        sketchcam.orthographicSize = Mathf.Clamp(sketchcam.orthographicSize - increment, zoomOutMin, zoomOutMax);
        //sketchcam.transform.position += new Vector3(0, 0, Input.GetAxis("Mouse ScrollWheel") * sensitivity);
        //Debug.Log("here");
    }
    Vector3 extzoom(float change)
    {
        //extcam.fieldOfView = Mathf.Clamp(extcam.fieldOfView - fov, 4f, 180f);
        float R = change * 15;                                   //The radius from current camera
        float PosX = extcam.transform.eulerAngles.x + 90;              //Get up and down
        float PosY = -1 * (extcam.transform.eulerAngles.y - 90);       //Get left to right
        PosX = PosX / 180 * Mathf.PI;                                       //Convert from degrees to radians
        PosY = PosY / 180 * Mathf.PI;                                       //^
        float X = R * Mathf.Sin(PosX) * Mathf.Cos(PosY);                    //Calculate new coords
        float Z = R * Mathf.Sin(PosX) * Mathf.Sin(PosY);                    //^
        float Y = R * Mathf.Cos(PosX);                                      //^
        float CamX = extcam.transform.position.x;                      //Get current camera postition for the offset
        float CamY = extcam.transform.position.y;                      //^
        float CamZ = extcam.transform.position.z;                      //^
        Vector3 pos = new Vector3(CamX + X, CamY + Y, CamZ + Z);
        if (pos.magnitude>50f || pos.magnitude < 5f)
        {
            pos = new Vector3(CamX, CamY, CamZ);
        }

        return pos;
    }


    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

}