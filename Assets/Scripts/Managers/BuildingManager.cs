using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }
    public GameObject wall;
    public GameObject turret;

    private void Awake()
    {
        Instance = this;
    }

    // 테스트
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) 
        {
            PlaceBuilding(wall, new Vector3(0, 0, 0));
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            PlaceBuilding(turret, new Vector3(2, 0, 0));
        }
    }

    public void PlaceBuilding(GameObject building, Vector3 position)
    {
        GameObject newBuilding = Instantiate(building, position, Quaternion.identity);
        BuildingBase buildingComponent = newBuilding.GetComponent<BuildingBase>();

        if (buildingComponent != null)
        {
            float buildingHeight = buildingComponent.GetComponent<Renderer>().bounds.size.y;
            newBuilding.transform.position = new Vector3(position.x, -buildingHeight, position.z);
            buildingComponent.Initialize();
        }
    }
}
