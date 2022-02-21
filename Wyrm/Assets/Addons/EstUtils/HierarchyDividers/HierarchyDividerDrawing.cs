#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class HierarchyDividerDrawing
{
	static HierarchyDividerDrawing()
	{
		EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
	}

	private static Vector2 offset = new Vector2(0, 2);

	private static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
	{
		GameObject obj = (GameObject)EditorUtility.InstanceIDToObject(instanceID);

		if (obj == null)
			return;

		HierarchyDivider found = obj.GetComponent<HierarchyDivider>();
		if (found == null)
			return;

		Color bgColor = new Color(found.bgColor.r, found.bgColor.g, found.bgColor.b, 1f);

		Color standardBg = EditorGUIUtility.isProSkin ?
			new Color(0.22f, 0.22f, 0.22f)
			: new Color(.76f, .76f, .76f);

		Color _selectionColor = EditorGUIUtility.isProSkin ? new Color(0.24f, 0.37f, 0.59f) : new Color(0.24f, 0.48f, 0.90f);
		bool selected = Selection.instanceIDs.Contains(instanceID);
		if (selected)
		{
			bgColor = _selectionColor;
		}
		Rect offsetRect = new Rect(selectionRect.position + offset, selectionRect.size);

#if UNITY_2019_2
		EditorGUI.DrawRect(new Rect(selectionRect.x, selectionRect.y, offsetRect.width + 2, offsetRect.height), bgColor);
#else
		EditorGUI.DrawRect(new Rect(selectionRect.x, selectionRect.y, offsetRect.width, offsetRect.height), bgColor);
#endif


		GUIStyle _style = new GUIStyle();
		_style.fontStyle = FontStyle.Bold;
		_style.normal = new GUIStyleState();
		_style.alignment = TextAnchor.UpperCenter;
		EditorGUI.LabelField(new Rect(offsetRect.x, offsetRect.y, offsetRect.width, offsetRect.height), obj.name, _style);

#if UNITY_2019_2
						EditorGUI.DrawRect(new Rect(16f, offsetRect.y - 2, 45, EditorGUIUtility.singleLineHeight), selected? _selectionColor : standardBg);
#elif UNITY_2019_1_OR_NEWER
		EditorGUI.DrawRect(new Rect(25f, offsetRect.y - 2, 30, EditorGUIUtility.singleLineHeight), selected ? _selectionColor : standardBg);
#else
						EditorGUI.DrawRect(new Rect(0f, offsetRect.y - 2, 30, EditorGUIUtility.singleLineHeight), selected? _selectionColor : standardBg);
#endif
	}
}
#endif
