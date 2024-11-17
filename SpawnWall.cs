using UnityEngine;

public class SpawnWall : MonoBehaviour
{
    public GameObject wallPrefab;
    public Camera cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = cam.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            Instantiate(wallPrefab, position, Quaternion.identity);
        }
    }
}
