//using UnityEditor;
//using UnityEngine;

//// SenseDrawer
//[CustomPropertyDrawer(typeof(Sense))]
//public class SenseDrawer : PropertyDrawer
//{
//    // Draw the property inside the given rect
//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//    {
//        // Using BeginProperty / EndProperty on the parent property means that
//        // prefab override logic works on the entire property.
//        EditorGUI.BeginProperty(position, label, property);

//        // Draw label
//        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

//        // Don't make child fields be indented
//        var indent = EditorGUI.indentLevel;
//        EditorGUI.indentLevel = 0;

//        // Draw fields - passs GUIContent.none to each so they are drawn without labels
//        EditorGUI.PropertyField(
//            new Rect(position.x, position.y, 90, 18),
//            property.FindPropertyRelative("filterName"),
//            GUIContent.none
//        );
//        EditorGUI.PropertyField(
//            new Rect(position.x + 92, position.y, position.width - 92, 18),
//            property.FindPropertyRelative("stimuli"),
//            GUIContent.none
//        );


//        var qq = property.FindPropertyRelative("senseChange");
//        EditorGUI.PropertyField(
//            new Rect(position.x, position.y+20, 70, 18),
//            qq,
//            GUIContent.none
//        );
//        float voffset = 0;

//        if (qq.enumValueIndex == 0)
//        {
//            // Fixed
//            voffset = 50;
//            EditorGUI.PropertyField(
//                new Rect(position.x + 72, position.y+20, 50, 18),
//                property.FindPropertyRelative("fixedValue"),
//                GUIContent.none
//            );
//        }

//        if (qq.enumValueIndex != 2)
//        {
//            EditorGUI.PropertyField(
//                new Rect(position.x + 72 + voffset, position.y + 20, 70, 18),
//                property.FindPropertyRelative("senseChangeDirection"),
//                GUIContent.none
//            );
//        }

//        if (qq.enumValueIndex == 1)
//        {
//            // Adaptive
//            EditorGUI.LabelField(
//                new Rect(position.x + 72 + voffset + 70, position.y + 20, 60, 18),
//                new GUIContent("Cooldown:", "Time spent in value before neuron adapts to this.")
//            );

//            EditorGUI.PropertyField(
//                new Rect(position.x + 72 + voffset+70+60, position.y + 20, 50, 18),
//                property.FindPropertyRelative("adaptationToValueCooldown"),
//                GUIContent.none
//            );
//        }



//            // Set indent back to what it was
//            EditorGUI.indentLevel = indent;

//        EditorGUI.EndProperty();
//    }

//    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
//    {
//        return 40;
//    }
//}
