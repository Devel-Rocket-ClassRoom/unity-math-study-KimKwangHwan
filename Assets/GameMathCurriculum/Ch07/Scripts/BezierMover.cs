using UnityEngine;

public class BezierMover : MonoBehaviour
{
    private Vector3 p0, p1, p2, p3;
    private float elapsedTime = 0f;
    private float speed;
    private float globalT = 0f;
    public void GetPoints(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        this.p0 = p0;
        this.p1 = p1; 
        this.p2 = p2; 
        this.p3 = p3;
    }

    private Vector3 CubicBezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        // TODO
        Vector3 a = Vector3.Lerp(p0, p1, t);
        Vector3 b = Vector3.Lerp(p1, p2, t);
        Vector3 c = Vector3.Lerp(p2, p3, t);

        Vector3 d = Vector3.Lerp(a, b, t);
        Vector3 e = Vector3.Lerp(b, c, t);

        return Vector3.Lerp(d, e, t);
    }
    void Start()
    {
        speed = Random.Range(0.3f, 0.8f);
        Color c = new Color(Random.value, Random.value, Random.value);

        GetComponent<Renderer>().material = new Material(Shader.Find("Sprites/Default"));
        GetComponent<Renderer>().material.color = c;

        TrailRenderer trail = GetComponent<TrailRenderer>();
        trail.material = new Material(Shader.Find("Sprites/Default"));
        trail.startColor = c;
        trail.endColor = new Color(c.r, c.g, c.b, 0);
        trail.startWidth = 0.2f;
        trail.endWidth = 0f;
        trail.time = 0.4f;
    }
    void Update()
    {
        transform.position = CubicBezier(p0, p1, p2, p3, globalT);

        elapsedTime += Time.deltaTime * speed;
        globalT = Mathf.Clamp01(elapsedTime);

        if (globalT >= 1f)
        {
            Destroy(gameObject);
        }
    }
}
