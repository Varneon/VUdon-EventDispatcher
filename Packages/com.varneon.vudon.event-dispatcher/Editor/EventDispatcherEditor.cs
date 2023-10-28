using UnityEditor;
using UnityEngine;
using Varneon.VUdon.Editors.Editor;

namespace Varneon.VUdon.EventDispatcher.Editor
{
    /// <summary>
    /// Custom inspector for EventDispatcher prefab
    /// </summary>
    [CustomEditor(typeof(EventDispatcher))]
    public class EventDispatcherEditor : InspectorBase
    {
        [SerializeField]
        private Texture2D headerIcon;

        protected override InspectorHeader Header => new InspectorHeaderBuilder()
            .WithTitle("VUdon - EventDispatcher")
            .WithDescription("Dispatcher for delegating update events to UdonBehaviours")
            .WithIcon(headerIcon)
            .WithURL("GitHub", "https://github.com/Varneon/VUdon-EventDispatcher")
            .Build();

        private GUIStyle wrappedRichTextStyle;

        private bool apiMethodsExpanded;

        protected override void OnEnable()
        {
            base.OnEnable();

            wrappedRichTextStyle = new GUIStyle()
            {
                normal =
                {
                    textColor = Color.white
                },
                wordWrap = true,
                richText = true,
                padding = new RectOffset(2, 2, 1, 2)
            };
        }

        protected override void OnPreDrawFields()
        {
            if(apiMethodsExpanded = EditorGUILayout.BeginFoldoutHeaderGroup(apiMethodsExpanded, "API Methods"))
            {
                GUI.color = Color.black;

                Rect rect = EditorGUILayout.BeginVertical(EditorStyles.helpBox);

                if (EditorDarkMode) { GUI.Box(rect, string.Empty); }

                GUI.color = Color.white;

                EditorGUI.indentLevel++;

                EditorGUILayout.LabelField("<color=#dcdcaa>AddHandler</color>(<color=#b8d7a3>VRCUpdateEventType</color>, <color=#b8d7a3>IUdonEventReceiver</color>)", wrappedRichTextStyle);
                EditorGUILayout.LabelField("<color=#dcdcaa>RemoveHandler</color>(<color=#b8d7a3>VRCUpdateEventType</color>, <color=#b8d7a3>IUdonEventReceiver</color>)", wrappedRichTextStyle);
                EditorGUILayout.LabelField("<color=#dcdcaa>SetHandlerActive</color>(<color=#b8d7a3>VRCUpdateEventType</color>, <color=#b8d7a3>IUdonEventReceiver</color>, <color=#569cd6>bool</color>)", wrappedRichTextStyle);
                EditorGUILayout.LabelField("<color=#dcdcaa>AddFixedUpdateHandler</color>(<color=#b8d7a3>IUdonEventReceiver</color>)", wrappedRichTextStyle);
                EditorGUILayout.LabelField("<color=#dcdcaa>RemoveFixedUpdateHandler</color>(<color=#b8d7a3>IUdonEventReceiver</color>)", wrappedRichTextStyle);
                EditorGUILayout.LabelField("<color=#dcdcaa>AddUpdateHandler</color>(<color=#b8d7a3>IUdonEventReceiver</color>)", wrappedRichTextStyle);
                EditorGUILayout.LabelField("<color=#dcdcaa>RemoveUpdateHandler</color>(<color=#b8d7a3>IUdonEventReceiver</color>)", wrappedRichTextStyle);
                EditorGUILayout.LabelField("<color=#dcdcaa>AddLateUpdateHandler</color>(<color=#b8d7a3>IUdonEventReceiver</color>)", wrappedRichTextStyle);
                EditorGUILayout.LabelField("<color=#dcdcaa>RemoveLateUpdateHandler</color>(<color=#b8d7a3>IUdonEventReceiver</color>)", wrappedRichTextStyle);
                EditorGUILayout.LabelField("<color=#dcdcaa>AddPostLateUpdateHandler</color>(<color=#b8d7a3>IUdonEventReceiver</color>)", wrappedRichTextStyle);
                EditorGUILayout.LabelField("<color=#dcdcaa>RemovePostLateUpdateHandler</color>(<color=#b8d7a3>IUdonEventReceiver</color>)", wrappedRichTextStyle);

                EditorGUI.indentLevel--;

                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.EndFoldoutHeaderGroup();
        }
    }
}
