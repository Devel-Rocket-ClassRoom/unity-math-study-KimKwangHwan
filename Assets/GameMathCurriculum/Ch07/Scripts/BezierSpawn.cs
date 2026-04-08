using UnityEngine;

public class BezierSpawn : MonoBehaviour
{
    public GameObject prefab;
    public GameObject final;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int randomValue = Random.Range(1, 11);
            for (int i = 0; i < randomValue; i++)
            {
                Vector3 mid = (transform.position + final.transform.position) / 2f;
                float radius = (transform.position - mid).magnitude;
                Vector3 p1 = (transform.position + mid) / 2f + Random.insideUnitSphere * (radius / 2f);
                Vector3 p2 = (mid + final.transform.position) / 2f + Random.insideUnitSphere * (radius / 2f);

                GameObject sphere = Instantiate(prefab, transform.position, Quaternion.identity);
                sphere.GetComponent<BezierMover>().GetPoints(transform.position, p1, p2, final.transform.position);
            }
        }
    }
    
}
