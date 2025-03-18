using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager
{
    public ItemData wallData;
    public ItemData turret1Data;
    public ItemData turret2Data;
    public ItemData tentData;
    public ItemData wellData;

    private List<GameObject> placedBuildings = new List<GameObject>();

    private GameObject previewBuilding;
    private ItemData selectedItemData;

    // 테스트
    public void Update()
    {
        if (selectedItemData != null)
        {
            Vector3 buildPosition = GetMouseWorldPosition();
            if (buildPosition != Vector3.zero)
            {
                previewBuilding.transform.position = buildPosition;
            }

            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
            {
                Managers.Instance.StartCoroutine(PlaceBuilding(buildPosition, selectedItemData));
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
    }

    public void StartBuilding(ItemData itemData)
    {
        if (selectedItemData != null)
        {
            CancelBuilding();
        }

        selectedItemData = itemData;
        previewBuilding = Managers.Resource.Instantiate(selectedItemData.Building);

        previewBuilding.GetComponent<Collider>().enabled = false;
        previewBuilding.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.5f);
    }

    private IEnumerator PlaceBuilding(Vector3 position, ItemData data)
    {
        foreach (GameObject placed in placedBuildings)
        {
            if (Vector3.Distance(placed.transform.position, position) < 0.1f)
            {
                Debug.Log("이 위치에는 이미 건물이 존재합니다!");
                yield break;
            }
        }

        CancelBuilding();

        //yield return new WaitForSeconds(data.BuildTime);

        GameObject newBuilding = Managers.Resource.Instantiate(data.Building);
        newBuilding.transform.position = position;

        BuildingBase buildingComponent = newBuilding.GetComponent<BuildingBase>();

        if (buildingComponent != null)
        {
            Managers.Game.Player.PlayerAction.AddAction(buildingComponent);
            yield return new WaitForSeconds(buildingComponent.BuildTime);
            placedBuildings.Add(newBuilding); // 설치된 건물 리스트에 추가
        }
    }

    public void CancelBuilding()
    {
        if (previewBuilding != null)
        {
            Managers.Resource.Destroy(previewBuilding);
            previewBuilding = null;
        }

        selectedItemData = null;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            return hit.point;
        }

        return Vector3.zero;
    }

    public void RemoveBuilding()
    {
        if (placedBuildings.Count > 0)
        {
            GameObject lastBuilding = placedBuildings[placedBuildings.Count - 1];
            BuildingBase buildingComponent = lastBuilding.GetComponent<BuildingBase>();

            if (buildingComponent != null)
            {
                Managers.Instance.StartCoroutine(RemoveBuildingWithDelay(buildingComponent, lastBuilding, 1.5f));
            }
        }
    }

    // 코루틴 점검 필요
    private IEnumerator RemoveBuildingWithDelay(BuildingBase building, GameObject obj, float delay)
    {
        // 건물 철거 애니메이션 실행
        building.StartRemoving(delay);

        yield return new WaitForSeconds(delay);

        // 리스트에서 제거
        placedBuildings.Remove(obj);
    }

    //public void DestroyBuilding()
    //{
    //    if (placedBuildings.Count > 0)
    //    {
    //        GameObject lastBuilding = placedBuildings[placedBuildings.Count - 1];
    //        BuildingBase buildingComponent = lastBuilding.GetComponent<BuildingBase>();

    //        if (buildingComponent != null)
    //        {
    //            buildingComponent.DestroyBuilding();
    //            Managers.Instance.StartCoroutine(WaitAndRemove(lastBuilding, 2f));
    //        }
    //    }
    //}

    private IEnumerator WaitAndRemove(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        placedBuildings.Remove(obj);
    }
}