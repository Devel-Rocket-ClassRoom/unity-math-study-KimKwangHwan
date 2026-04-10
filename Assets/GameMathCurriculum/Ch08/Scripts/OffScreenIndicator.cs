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

            Vector3 local = cam.transform.InverseTransformPoint(indicators[i].transform.position);
            Vector2 dir = new Vector2(local.x, local.y).normalized;
            Vector2 center = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
            float scale = Mathf.Min(center.x / Mathf.Abs(dir.x), center.y / Mathf.Abs(dir.y));
            Vector2 pos = center + dir * scale;
            images[i].transform.position = new Vector2(Mathf.Clamp(pos.x, offset, Screen.width - offset), Mathf.Clamp(pos.y, offset, Screen.height - offset));

            if (isBehind)
            {
                idPointInScreen.x = Screen.width - idPointInScreen.x;
                idPointInScreen.y = Screen.height - idPointInScreen.y;
            }

            if (isBehind || idPointInScreen.x < 0f || idPointInScreen.x > Screen.width || idPointInScreen.y < 0f || idPointInScreen.y > Screen.height)
            {
                //images[i].transform.position = new Vector3(Mathf.Clamp(idPointInScreen.x, offset, Screen.width - offset), Mathf.Clamp(idPointInScreen.y, offset, Screen.height - offset), 0f);
                images[i].enabled = true;
            }
            else
            {
                images[i].enabled = false;
            }
        }
    }

}
