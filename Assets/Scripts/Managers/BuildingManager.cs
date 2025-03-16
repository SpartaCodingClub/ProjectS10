using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }
    public GameObject wall;
    public GameObject turret1;
    public GameObject turret2;
    public GameObject turret3;

    private List<GameObject> placedBuildings = new List<GameObject>();

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

        if (Input.GetKeyDown(KeyCode.A))
        {
            PlaceBuilding(turret1, new Vector3(2, 0, 0));
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            PlaceBuilding(turret2, new Vector3(2, 0, 0));
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            PlaceBuilding(turret3, new Vector3(2, 0, 0));
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RemoveBuilding();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            DestroyBuilding();
        }
    }

    public void PlaceBuilding(GameObject building, Vector3 position)
    {
        GameObject newBuilding = Instantiate(building, position, Quaternion.identity);
        BuildingBase buildingComponent = newBuilding.GetComponent<BuildingBase>();

        if (buildingComponent != null)
        {
            buildingComponent.Initialize();
            placedBuildings.Add(newBuilding);
        }
    }

    public void RemoveBuilding()
    {
        if (placedBuildings.Count > 0)
        {
            // 플레이어가 인식할 경우 철거로 수정
            GameObject lastBuilding = placedBuildings[placedBuildings.Count - 1];
            BuildingBase buildingComponent = lastBuilding.GetComponent<BuildingBase>();

            if (buildingComponent != null)
            {
                StartCoroutine(RemoveBuildingWithDelay(buildingComponent, lastBuilding, 1.5f));
            }
        }
    }

    // 코루틴 점검 필요
    private IEnumerator RemoveBuildingWithDelay(BuildingBase building, GameObject obj, float delay)
    {
        building.StartRemoving(delay);
        yield return new WaitForSeconds(delay);
        placedBuildings.Remove(obj);
    }

    public void DestroyBuilding()
    {
        if (placedBuildings.Count > 0)
        {
            GameObject lastBuilding = placedBuildings[placedBuildings.Count - 1];
            BuildingBase buildingComponent = lastBuilding.GetComponent<BuildingBase>();

            if (buildingComponent != null)
            {
                buildingComponent.DestroyBuilding();
                StartCoroutine(WaitAndRemove(lastBuilding, 2f));
            }
        }
    }

    private IEnumerator WaitAndRemove(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        placedBuildings.Remove(obj);
    }
}
