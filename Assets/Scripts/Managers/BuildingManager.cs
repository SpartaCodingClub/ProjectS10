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

    private GameObject selectedBuilding; // 배치할 건물
    private GameObject previewBuilding; // 미리보기용 프리팹

    private void Awake()
    {
        Instance = this;
    }

    // 테스트 (UI랑
    private void Update()
    {
        // 마우스 위치로 미리보기 건물 이동
        Vector3 buildPosition = GetMouseWorldPosition();
        previewBuilding.transform.position = buildPosition;

        // 배치 확정
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
        {
            PlaceBuilding(buildPosition);
        }

        // 취소 
        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
        {
            CancelBuilding();
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

    public void StartBuilding(GameObject buildingPrefab)
    {
        if (selectedBuilding != null)
        {
            CancelBuilding(); // 기존 건설 취소
        }

        selectedBuilding = buildingPrefab;
        previewBuilding = Instantiate(buildingPrefab);
        previewBuilding.GetComponent<Collider>().enabled = false; 
        previewBuilding.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.5f); // 반투명 표시
    }

    public void PlaceBuilding(Vector3 position)
    {
        if (selectedBuilding != null)
        {
            GameObject newBuilding = Instantiate(selectedBuilding, position, Quaternion.identity);
            BuildingBase buildingComponent = newBuilding.GetComponent<BuildingBase>();

            if (buildingComponent != null)
            {
                buildingComponent.Initialize();
                placedBuildings.Add(newBuilding);
            }

            CancelBuilding(); // 건설 후 선택 취소
        }
    }

    public void CancelBuilding()
    {
        if (previewBuilding != null)
        {
            Destroy(previewBuilding);
        }

        selectedBuilding = null;
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
}
