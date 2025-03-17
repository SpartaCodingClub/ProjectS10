using UnityEditor;
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
    SerializedProperty building;
    SerializedProperty maxHealth;
    SerializedProperty resourceAmount;
    SerializedProperty buildTime;

    private void OnEnable()
    {
        id = serializedObject.FindProperty("ID");
        name = serializedObject.FindProperty("Name");
        description = serializedObject.FindProperty("Description");
        type = serializedObject.FindProperty("Type");
        canStacking = serializedObject.FindProperty("CanStacking");
        maxStacking = serializedObject.FindProperty("MaxStacking");
        weapon = serializedObject.FindProperty("Weapon");

        building = serializedObject.FindProperty("Building");
        maxHealth = serializedObject.FindProperty("MaxHealth");
        resourceAmount = serializedObject.FindProperty("ResourceAmount");
        buildTime = serializedObject.FindProperty("BuildTime");
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


        if (iType == ItemType.Weapon)
        {
            canStacking.boolValue = false;
            EditorGUILayout.PropertyField(weapon, new GUIContent("무기 프리팹"));
        }

        else if (iType == ItemType.Building) 
        {
            EditorGUILayout.PropertyField(building, new GUIContent("건물 프리팹"));
            EditorGUILayout.PropertyField(maxHealth, new GUIContent("최대 체력"));
            EditorGUILayout.PropertyField(resourceAmount, new GUIContent("건설 비용"));
            EditorGUILayout.PropertyField(buildTime, new GUIContent("건설 시간")); 
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