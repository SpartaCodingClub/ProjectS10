using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(ItemData))]
public class ItemInspector : Editor
{
    SerializedProperty id;
    SerializedProperty name;
    SerializedProperty description;
    SerializedProperty type;
    SerializedProperty canStacking;
    SerializedProperty maxStacking;
    SerializedProperty weapon;

    private void OnEnable()
    {
        id = serializedObject.FindProperty("id");
        name = serializedObject.FindProperty("name");
        description = serializedObject.FindProperty("description");
        type = serializedObject.FindProperty("type");
        canStacking = serializedObject.FindProperty("canStacking");
        maxStacking = serializedObject.FindProperty("maxStacking");
        weapon = serializedObject.FindProperty("weapon");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(id, new GUIContent("식별 번호"));
        EditorGUILayout.PropertyField(name, new GUIContent("이름"));
        EditorGUILayout.PropertyField(description, new GUIContent("설명"));

        EditorGUI.BeginChangeCheck();
        ItemType iType = (ItemType)type.enumValueIndex;
        iType = (ItemType)EditorGUILayout.EnumPopup("아이템 타입", iType);

        if (EditorGUI.EndChangeCheck())
        {
            type.enumValueIndex = (int)iType;
        }


        if(iType == ItemType.Weapon)
        {
            canStacking.boolValue = false;
            EditorGUILayout.PropertyField(weapon, new GUIContent("무기 프리팹"));
        }
        else
        {
            EditorGUILayout.PropertyField(canStacking, new GUIContent("스택 가능 여부"));
            EditorGUILayout.PropertyField(maxStacking, new GUIContent("최대 스택 수"));
        }
        serializedObject.ApplyModifiedProperties();
    }
}
#endif
