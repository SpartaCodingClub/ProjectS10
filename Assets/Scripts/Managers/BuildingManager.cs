using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }
    public GameObject wall;

    private void Awake()
    {
        Instance = this;
    }

    // 테스트
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            PlaceWall(new Vector3(0, 0, 0));
        }
    }

    public void PlaceWall(Vector3 position)
    {
        GameObject newWall = Instantiate(wall, position, Quaternion.identity);
        BuildingWall wallComponent = newWall.GetComponent<BuildingWall>();

        if (wallComponent != null)
        {
            float buildingHeight = wallComponent.GetComponent<Renderer>().bounds.size.y;
            newWall.transform.position = new Vector3(position.x, -buildingHeight, position.z);
            wallComponent.Initialize();
        }
    }
}
