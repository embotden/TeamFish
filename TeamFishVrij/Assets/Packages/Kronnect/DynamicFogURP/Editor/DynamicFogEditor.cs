using UnityEngine;
using UnityEditor;

namespace DynamicFogAndMist2 {

    [CustomEditor(typeof(DynamicFog))]
    public partial class DynamicFogEditor : Editor {

        DynamicFogProfile cachedProfile;
        Editor cachedProfileEditor;
        SerializedProperty profile;
        SerializedProperty maskEditorEnabled, maskBrushMode, maskBrushColor, maskBrushWidth, maskBrushFuzziness, maskBrushOpacity;
        SerializedProperty enableFade, fadeDistance, fadeController, enableSubVolumes, subVolumes, showBoundary, allowRotation;
        SerializedProperty enableFogOfWar, fogOfWarCenter, fogOfWarSize, fogOfWarTextureSize, fogOfWarRestoreDelay, fogOfWarRestoreDuration, fogOfWarSmoothness, fogOfWarBlur;

        static GUIStyle boxStyle;
        DynamicFog fog;

        void OnEnable() {
            profile = serializedObject.FindProperty("profile");

            enableFade = serializedObject.FindProperty("enableFade");
            fadeDistance = serializedObject.FindProperty("fadeDistance");
            fadeController = serializedObject.FindProperty("fadeController");
            enableSubVolumes = serializedObject.FindProperty("enableSubVolumes");
            subVolumes = serializedObject.FindProperty("subVolumes");
            showBoundary = serializedObject.FindProperty("showBoundary");
            allowRotation = serializedObject.FindProperty("allowRotation");

            enableFogOfWar = serializedObject.FindProperty("enableFogOfWar");
            fogOfWarCenter = serializedObject.FindProperty("fogOfWarCenter");
            fogOfWarSize = serializedObject.FindProperty("fogOfWarSize");
            fogOfWarTextureSize = serializedObject.FindProperty("fogOfWarTextureSize");
            fogOfWarRestoreDelay = serializedObject.FindProperty("fogOfWarRestoreDelay");
            fogOfWarRestoreDuration = serializedObject.FindProperty("fogOfWarRestoreDuration");
            fogOfWarSmoothness = serializedObject.FindProperty("fogOfWarSmoothness");
            fogOfWarBlur = serializedObject.FindProperty("fogOfWarBlur");

            maskEditorEnabled = serializedObject.FindProperty("maskEditorEnabled");
            maskBrushColor = serializedObject.FindProperty("maskBrushColor");
            maskBrushMode = serializedObject.FindProperty("maskBrushMode");
            maskBrushWidth = serializedObject.FindProperty("maskBrushWidth");
            maskBrushFuzziness = serializedObject.FindProperty("maskBrushFuzziness");
            maskBrushOpacity = serializedObject.FindProperty("maskBrushOpacity");
            fog = (DynamicFog)target;
        }


        public override void OnInspectorGUI() {

            if (boxStyle == null) {
                boxStyle = new GUIStyle(GUI.skin.box);
                boxStyle.padding = new RectOffset(15, 10, 5, 5);
            }

            serializedObject.Update();

            EditorGUILayout.PropertyField(profile);

            if (profile.objectReferenceValue != null) {
                if (cachedProfile != profile.objectReferenceValue) {
                    cachedProfile = null;
                }
                if (cachedProfile == null) {
                    cachedProfile = (DynamicFogProfile)profile.objectReferenceValue;
                    cachedProfileEditor = CreateEditor(profile.objectReferenceValue);
                }

                // Drawing the profile editor
                EditorGUILayout.BeginVertical(boxStyle);
                cachedProfileEditor.OnInspectorGUI();
                EditorGUILayout.EndVertical();
            } else {
                EditorGUILayout.HelpBox("Create or assign a fog profile.", MessageType.Info);
                if (GUILayout.Button("New Fog Profile")) {
                    CreateFogProfile();
                }
            }

            EditorGUILayout.PropertyField(showBoundary);
            EditorGUILayout.PropertyField(allowRotation);
            EditorGUILayout.PropertyField(enableFade);
            if (enableFade.boolValue) {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(fadeDistance);
                EditorGUILayout.PropertyField(fadeController, new GUIContent("Character Controller"));
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.PropertyField(enableSubVolumes);
            if (enableSubVolumes.boolValue) {
                EditorGUI.indentLevel++;
                EditorGUILayout.HelpBox("If no sub-volume is specified below, any sub-volume in the scene will be used.", MessageType.Info);
                EditorGUILayout.PropertyField(fadeController, new GUIContent("Character Controller"));
                EditorGUILayout.PropertyField(subVolumes);
                EditorGUI.indentLevel--;
            }

            bool requiresFogOfWarTextureReload = false;
            EditorGUILayout.PropertyField(enableFogOfWar);
            if (enableFogOfWar.boolValue) {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(fogOfWarCenter, new GUIContent("World Center"));
                EditorGUILayout.PropertyField(fogOfWarSize, new GUIContent("World Coverage"));
                EditorGUILayout.PropertyField(fogOfWarTextureSize, new GUIContent("Texture Size"));
                EditorGUILayout.PropertyField(fogOfWarRestoreDelay, new GUIContent("Restore Delay"));
                EditorGUILayout.PropertyField(fogOfWarRestoreDuration, new GUIContent("Restore Duration"));
                EditorGUILayout.PropertyField(fogOfWarSmoothness, new GUIContent("Border Smoothness"));
                EditorGUILayout.PropertyField(fogOfWarBlur, new GUIContent("Blur"));

                EditorGUILayout.Separator();
                EditorGUILayout.PropertyField(maskEditorEnabled, new GUIContent("Fog Of War Editor", "Activates terrain brush to paint/remove fog of war at custom locations."));

                if (maskEditorEnabled.boolValue) {
                    EditorGUILayout.HelpBox("While Fog Of War Editor is enabled, fog will be rendered with transparency to allow you see the terrain.", MessageType.Info);
                    if (GUILayout.Button("Create New Mask Texture")) {
                        if (EditorUtility.DisplayDialog("Create Mask Texture", "A texture asset will be created with the size specified in current profile (" + fog.fogOfWarTextureSize + "x" + fog.fogOfWarTextureSize + ").\n\nContinue?", "Ok", "Cancel")) {
                            CreateNewMaskTexture();
                            GUIUtility.ExitGUI();
                        }
                    }
                    EditorGUI.BeginChangeCheck();
                    fog.fogOfWarTexture = (Texture2D)EditorGUILayout.ObjectField(new GUIContent("Coverage Texture", "Fog of war coverage mask. A value of alpha of zero means no fog."), fog.fogOfWarTexture, typeof(Texture2D), false);
                    if (EditorGUI.EndChangeCheck()) {
                        requiresFogOfWarTextureReload = true;
                    }
                    Texture2D tex = fog.fogOfWarTexture;
                    if (tex != null) {
                        EditorGUILayout.LabelField("   Texture Size", tex.width.ToString());
                        string path = AssetDatabase.GetAssetPath(tex);
                        if (string.IsNullOrEmpty(path)) {
                            path = "(Temporary texture)";
                        }
                        EditorGUILayout.LabelField("   Texture Path", path);
                    }

                    if (tex != null) {
                        EditorGUILayout.Separator();
                        EditorGUILayout.BeginVertical(GUI.skin.box);
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.PropertyField(maskBrushMode, new GUIContent("Brush Mode", "Select brush operation mode."));
                        if (GUILayout.Button("Toggle", GUILayout.Width(70))) {
                            maskBrushMode.intValue = maskBrushMode.intValue == 0 ? 1 : 0;
                        }
                        EditorGUILayout.EndHorizontal();
                        if (maskBrushMode.intValue == (int)MASK_TEXTURE_BRUSH_MODE.ColorFog) {
                            EditorGUILayout.PropertyField(maskBrushColor, new GUIContent("   Color", "Brush color."));
                        }
                        EditorGUILayout.PropertyField(maskBrushWidth, new GUIContent("   Width", "Width of the snow editor brush."));
                        EditorGUILayout.PropertyField(maskBrushFuzziness, new GUIContent("   Fuzziness", "Solid vs spray brush."));
                        EditorGUILayout.PropertyField(maskBrushOpacity, new GUIContent("   Opacity", "Stroke opacity."));
                        EditorGUILayout.BeginHorizontal();
                        if (tex == null) GUI.enabled = false;
                        if (GUILayout.Button("Fill Mask")) {
                            fog.ResetFogOfWar(255);
                            maskBrushMode.intValue = (int)MASK_TEXTURE_BRUSH_MODE.RemoveFog;
                        }
                        if (GUILayout.Button("Clear Mask")) {
                            fog.ResetFogOfWar(0);
                            maskBrushMode.intValue = (int)MASK_TEXTURE_BRUSH_MODE.AddFog;
                        }

                        GUI.enabled = true;
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.EndVertical();
                    }
                }
                EditorGUI.indentLevel--;
            }


            serializedObject.ApplyModifiedProperties();
            if (requiresFogOfWarTextureReload) {
                fog.ReloadFogOfWarTexture();
            }

        }

        void CreateFogProfile() {
            DynamicFogProfile fp = CreateInstance<DynamicFogProfile>();
            fp.name = "New Dynamic Fog Profile";
            AssetDatabase.CreateAsset(fp, "Assets/" + fp.name + ".asset");
            AssetDatabase.SaveAssets();
            profile.objectReferenceValue = fp;
            EditorGUIUtility.PingObject(fp);
        }
    }

}