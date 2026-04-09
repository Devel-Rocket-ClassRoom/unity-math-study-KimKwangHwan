using System.Drawing;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public Terrain terrain;
    private Camera cam;
    private bool isDraging;
    private GameObject selectedObject;
    private Vector3 originPosition;
    private Vector3 pointInterval;
    private bool isInHome = false;
    private Transform targetHome;
    [SerializeField] private GameObject[] homes;
    [SerializeField] private float threshold = 5f;

    private void Start()
    {
        cam = Camera.main;
    }
    private void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Target"))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    selectedObject = hit.collider.gameObject;
                    isDraging = true;
                    originPosition = selectedObject.transform.position;
                    Vector3 size = selectedObject.GetComponent<Renderer>().bounds.size;
                    pointInterval = selectedObject.transform.position - new Vector3(hit.point.x, terrain.SampleHeight(hit.point) + size.y / 2f, hit.point.z);
                    isInHome = false;
                }
            }
            if (isDraging && selectedObject != null)
            {
                Vector3 size = selectedObject.GetComponent<Renderer>().bounds.size;
                selectedObject.transform.position = new Vector3(hit.point.x, terrain.SampleHeight(hit.point) + size.y / 2f, hit.point.z) + pointInterval;
                
                Vector3 objectBottom = selectedObject.transform.position - new Vector3(0f, size.y / 2f, 0f);
                foreach (GameObject obj in homes)
                {
                    if (Vector3.Distance(objectBottom, obj.transform.position) < threshold)
                    {
                        isInHome = true;
                        targetHome = obj.transform;
                        break;
                    }
                    else
                    {
                        targetHome = null;
                        isInHome = false;
                    }
                }
            }
        }
        //Debug.Log(isInHome);
        if (isDraging && selectedObject != null && Input.GetMouseButtonUp(0))
        {
            if (!isInHome)
            {
                selectedObject.GetComponent<ControllableObject>()?.GoOriginPosition(originPosition);
            }
            else
            {
                Vector3 homeSize = targetHome.GetComponent<Renderer>().bounds.size;
                Vector3 cubeSize = selectedObject.GetComponent<Renderer>().bounds.size;
                selectedObject.transform.position = new Vector3(targetHome.position.x, targetHome.position.y + homeSize.y / 2f + cubeSize.y / 2f, targetHome.position.z);
            }
            selectedObject = null;
            isDraging = false;
            isInHome = false;
        }
    }
}
