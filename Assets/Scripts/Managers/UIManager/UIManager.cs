using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public class UIManager
{
    private readonly int POPUP_SORTING_ORDER_BASE = 10;

    public enum Type
    {
        Menu,
        Popup,
        Scene,
        SubItem,
        WorldSpace,
        Count
    }

    public UI_Scene CurrentSceneUI { get; private set; }

    private Transform transform;

    private readonly Transform[] children = new Transform[(int)Type.Count];
    private readonly Stack<UI_Popup> popups = new();

    private UI_SubItem popupBackground;

    public void Initialize()
    {
        transform = new GameObject(nameof(UIManager), typeof(InputSystemUIInputModule)).transform;
        transform.SetParent(Managers.Instance.transform);

        var names = Enum.GetNames(typeof(Type));
        for (int i = 0; i < children.Length; i++)
        {
            var child = new GameObject(names[i]).transform;
            child.SetParent(transform);
            children[i] = child;
        }
    }

    public T Show<T>() where T : UI_Base
    {
        Type type = GetType(typeof(T));
        if (type == Type.Count)
        {
            Debug.LogWarning($"Failed to Show<{typeof(T).Name}>()");
            return null;
        }

        T @base = Managers.Resource.Instantiate<T>(children[(int)type]);
        if (@base == null)
        {
            return null;
        }

        switch (type)
        {
            case Type.Popup:
                ShowPopup(@base as UI_Popup);
                break;
            case Type.Scene:
                CurrentSceneUI = @base as UI_Scene;
                break;
            case Type.WorldSpace:
                @base.GetComponent<Canvas>().worldCamera = Managers.Game.MainCamera;
                break;
        }

        return @base;
    }

    private void ShowPopup(UI_Popup popup)
    {
        if (popupBackground == null)
        {
            //popupBackground = Show<UI_PopupBackground>();
        }

        int backgroundSortingOrder = POPUP_SORTING_ORDER_BASE + popups.Count * 2 + 1;
        popupBackground.GetComponent<Canvas>().sortingOrder = backgroundSortingOrder;

        popup.SortingOrder = backgroundSortingOrder + 1;
        popups.Push(popup);
    }

    public void ClosePopup()
    {
        if (popups.Count == 0)
        {
            return;
        }

        if (popups.Peek().Interactable == false)
        {
            return;
        }

        UI_Popup popup = popups.Pop();
        popups.Pop().Close();

        if (popups.Count > 0)
        {
            popupBackground.GetComponent<Canvas>().sortingOrder = popup.SortingOrder - 3;
            return;
        }

        popupBackground.Close();
        popupBackground = null;
    }

    public void CloseAllPopup()
    {
        while (popups.Count > 0)
        {
            popups.Pop().Destroy();
        }

        popups.Clear();
    }

    private Type GetType(System.Type type)
    {
        return type switch
        {
            var t when t.IsSubclassOf(typeof(UI_Menu)) => Type.Menu,
            var t when t.IsSubclassOf(typeof(UI_Popup)) => Type.Popup,
            var t when t.IsSubclassOf(typeof(UI_Scene)) => Type.Scene,
            var t when t.IsSubclassOf(typeof(UI_SubItem)) => Type.SubItem,
            var t when t.IsSubclassOf(typeof(UI_WorldSpace)) => Type.WorldSpace,
            _ => Type.Count
        };
    }
}