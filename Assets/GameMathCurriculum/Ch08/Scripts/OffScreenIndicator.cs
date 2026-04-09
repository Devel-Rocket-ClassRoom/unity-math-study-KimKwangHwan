using UnityEngine;
using UnityEngine.UI;

public class OffScreenIndicator : MonoBehaviour
{
    [SerializeField] private float offset;
    public Transform target;
    public GameObject[] indicators;
    public Image[] images;
    public Color[] colors;
    public Camera cam;
    private void Start()
    {
        cam = Camera.main;
        for (int i = 0; i < indicators.Length; i++)
        {
            indicators[i].GetComponent<Renderer>().material.color = colors[i];
            images[i].color = new Color(colors[i].r, colors[i].g, colors[i].b, 255f);
            images[i].enabled = false;
        }
    }

    private void LateUpdate()
    {
        for (int i = 0; i < indicators.Length; i++)
        {
            Vector3 idPointInScreen = cam.WorldToScreenPoint(indicators[i].transform.position);

            bool isBehind = idPointInScreen.z < 0;

            if (isBehind)
            {
                idPointInScreen.x = Screen.width - idPointInScreen.x;
                idPointInScreen.y = Screen.height - idPointInScreen.y;
            }

            if (isBehind || idPointInScreen.x < 0f || idPointInScreen.x > Screen.width || idPointInScreen.y < 0f || idPointInScreen.y > Screen.height)
            {
                //Debug.Log($"{indicators[i].name}: {images[i].transform.position}");
                images[i].transform.position = new Vector3(Mathf.Clamp(idPointInScreen.x, offset, Screen.width - offset), Mathf.Clamp(idPointInScreen.y, offset, Screen.height - offset), 0f);
                images[i].enabled = true;
            }
            else
            {
                images[i].enabled = false;
            }
        }
    }

}
