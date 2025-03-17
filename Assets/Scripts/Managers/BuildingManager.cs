using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }

    public ItemData wallData;
    public ItemData turret1Data;
    public ItemData turret2Data;
    public ItemData tentData;
    public ItemData wellData;

    private List<GameObject> placedBuildings = new List<GameObject>();

    private GameObject previewBuilding;
    private ItemData selectedItemData;

    private void Awake()
    {
        Instance = this;
    }

    // 테스트
    private void Update()
    {
        if (selectedItemData != null)
        {
            Vector3 buildPosition = GetMouseWorldPosition();
            previewBuilding.transform.position = buildPosition;

            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(PlaceBuilding(buildPosition, selectedItemData));
            }

            if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
            {
                CancelBuilding();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RemoveBuilding();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            DestroyBuilding();
        }
    }

    public void StartBuilding(ItemData itemData)
    {
        if (selectedItemData != null)
        {
            CancelBuilding();
        }

        selectedItemData = itemData;

        // ItemManager에서 보유한 아이템이 충분한지 검사하고 있습니다!
        //if (!HasEnoughResources(selectedItemData.ResourceAmount))
        //{
        //    Debug.Log("자원이 부족합니다");
        //    selectedItemData = null;
        //    return;
        //}

        previewBuilding = Instantiate(selectedItemData.Building);
        previewBuilding.GetComponent<Collider>().enabled = false;
        previewBuilding.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.5f);
    }

    private IEnumerator PlaceBuilding(Vector3 position, ItemData data)
    {
        yield return new WaitForSeconds(data.BuildTime);

        GameObject newBuilding = Instantiate(data.Building, position, Quaternion.identity);
        BuildingBase buildingComponent = newBuilding.GetComponent<BuildingBase>();

        if (buildingComponent != null)
        {
            buildingComponent.Initialize();
        }
    }

    public void CancelBuilding()
    {
        if (previewBuilding != null)
        {
            Destroy(previewBuilding);
        }

        selectedItemData = null;
        previewBuilding = null;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            return hit.point;
        }
        return Vector3.zero;
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

    private bool HasEnoughResources(int cost)
    {
        int playerResources = 100; 
        return playerResources >= cost;
    }
}
