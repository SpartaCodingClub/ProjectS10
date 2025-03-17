using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_BuildItem : UI_Base, IPointerClickHandler, IPointerEnterHandler
{
    #region Inspector
    [SerializeField]
    private ItemData buildingData;
    #endregion

    private enum Children
    {
        Text_Value_BlueStone,
        Text_Value_PurpleStone,
        Text_Value_RedStone
    }

    private Vector3Int resources;

    protected override void Initialize()
    {
        base.Initialize();
        BindChildren(typeof(Children));

        UpdateUI(buildingData.ResourceAmount.x, buildingData.ResourceAmount.y, buildingData.ResourceAmount.z);
    }

    public void UpdateUI(int blueStone, int purpleStone, int redStone = 0)
    {
        if (redStone > 0)
        {
            Get<TMP_Text>((int)Children.Text_Value_RedStone).text = redStone.ToString();
        }

        Get<TMP_Text>((int)Children.Text_Value_BlueStone).text = blueStone.ToString();
        Get<TMP_Text>((int)Children.Text_Value_PurpleStone).text = purpleStone.ToString();

        resources.x = blueStone;
        resources.y = purpleStone;
        resources.z = redStone;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 인벤토리에 아이템 충분한지 확인
        if (resources.x > 0)
        {
            if (Managers.Item.ContainsItem((int)ItemID.BlueStone, resources.x) == false)
            {
                return;
            }
        }

        if (resources.y > 0)
        {
            if (Managers.Item.ContainsItem((int)ItemID.PurpleStone, resources.y) == false)
            {
                return;
            }
        }

        if (resources.z > 0)
        {
            if (Managers.Item.ContainsItem((int)ItemID.RedStone, resources.z) == false)
            {
                return;
            }
        }

        // 충분하다면 아이템 삭제
        Managers.Item.RemoveItem((int)ItemID.BlueStone, resources.x);
        Managers.Item.RemoveItem((int)ItemID.PurpleStone, resources.y);
        Managers.Item.RemoveItem((int)ItemID.RedStone, resources.z);

        // 건물 생성
        Debug.Log("재료 충분");

        (Managers.UI.CurrentSceneUI as UI_Build).Close();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Managers.Item.ItemPopup == null)
        {
            Managers.Item.ItemPopup = Managers.UI.Show<UI_ItemPopup>();
        }

        Managers.Item.ItemPopup.UpdateUI(buildingData);
    }
}