using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(Weapon))]
public class EquipInspector : Editor
{
    SerializedProperty type;
    SerializedProperty atk;
    SerializedProperty projectilenum;
    SerializedProperty atkDelay;
    SerializedProperty range;
    SerializedProperty projectileObject;

    private void OnEnable()
    {
        type = serializedObject.FindProperty("type");
        atk = serializedObject.FindProperty("atk");
        projectilenum = serializedObject.FindProperty("projectilenum");
        atkDelay = serializedObject.FindProperty("atkDelay");
        range = serializedObject.FindProperty("range");
        projectileObject = serializedObject.FindProperty("projectileObject");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUI.BeginChangeCheck();
        WeaponType selectedType = (WeaponType)type.enumValueIndex;
        selectedType = (WeaponType)EditorGUILayout.EnumPopup("무기 타입", selectedType);

        if(EditorGUI.EndChangeCheck())
        {
            type.enumValueIndex = (int)selectedType;
        }

        if (selectedType == WeaponType.Melee)
        {
            EditorGUILayout.PropertyField(atk, new GUIContent("공격력"));
            EditorGUILayout.PropertyField(atkDelay, new GUIContent("공격 간의 지연시간"));
            EditorGUILayout.PropertyField(range, new GUIContent("공격범위"));
        }

        else if(selectedType == WeaponType.Projectile)
        {
            EditorGUILayout.PropertyField(atk, new GUIContent("공격력"));
            EditorGUILayout.PropertyField(projectilenum, new GUIContent("투사체 개수"));
            EditorGUILayout.PropertyField(atkDelay, new GUIContent("공격 간의 지연시간"));
            EditorGUILayout.PropertyField(projectileObject, new GUIContent("투사체 오브젝트"));
        }
        serializedObject.ApplyModifiedProperties();
    }
}
#endif