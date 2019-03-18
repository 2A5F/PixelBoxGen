#if UNITY_EDITOR
using MeowType.Collections.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace PixelBoxGen
{
    #region BoxGenEditor
    [CustomEditor(typeof(Box))]
    class BoxGenEditor : Editor
    {
        public ReorderableList reorderableList;

        void OnEnable()
        {
            #region new ReorderableList
            var prop = serializedObject.FindProperty("ColliderImages");
            reorderableList = new ReorderableList(serializedObject, prop)
            {
                elementHeight = 110,
                drawElementCallback = (rect, index, isActive, isFocused) =>
                {
                    var element = prop.GetArrayElementAtIndex(index);
                    EditorGUI.PropertyField(rect, element);
                },
                drawHeaderCallback = (rect) =>
                {
                    EditorGUI.LabelField(rect, "ColliderSprites");
                }
            };
            #endregion

            #region onAddCallback
            reorderableList.onAddCallback += (list) =>
            {
                prop.arraySize++;
                list.index = prop.arraySize - 1;

                #region propertys
                var property = prop.GetArrayElementAtIndex(list.index);
                var rootpos_property = property.FindPropertyRelative("rootpos");
                var img_property = property.FindPropertyRelative("img");
                var Collider_property = property.FindPropertyRelative("Collider");
                var points_property = property.FindPropertyRelative("points");
                var Uid_property = property.FindPropertyRelative("Uid");
                var NeedNewCollider_property = property.FindPropertyRelative("NeedNewCollider");
                var Color_property = property.FindPropertyRelative("Color");
                var ColorRangeTo_property = property.FindPropertyRelative("ColorRangeTo");
                var ReverseColor_property = property.FindPropertyRelative("ReverseColor");
                var ColorR_property = property.FindPropertyRelative("ColorR");
                var ColorG_property = property.FindPropertyRelative("ColorG");
                var ColorB_property = property.FindPropertyRelative("ColorB");
                var ColorA_property = property.FindPropertyRelative("ColorA");
                var ColorRange_property = property.FindPropertyRelative("ColorRange");
                #endregion

                #region DefaultValues
                rootpos_property.vector3Value = ((MonoBehaviour)serializedObject.targetObject).transform.position;
                img_property.objectReferenceValue = null;
                Collider_property.objectReferenceValue = null;
                points_property.arraySize = 0;
                Uid_property.stringValue = "0";
                NeedNewCollider_property.boolValue = false;
                Color_property.colorValue = new Color(0, 0, 0, 0);
                ColorRangeTo_property.colorValue = new Color(0, 0, 0, 0);
                ReverseColor_property.boolValue = true;
                ColorR_property.boolValue = false;
                ColorG_property.boolValue = false;
                ColorB_property.boolValue = false;
                ColorA_property.boolValue = true;
                ColorRange_property.boolValue = false;
                #endregion
            };
            #endregion
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            reorderableList.DoLayoutList();

            serializedObject.ApplyModifiedProperties();
        }
    }
    #endregion

    #region BoxGenDrawer
    [CustomPropertyDrawer(typeof(ColliderImage))]
    public class BoxGenDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            using (new EditorGUI.PropertyScope(position, label, property))
            {
                #region rect define
                position.height = EditorGUIUtility.singleLineHeight;
                var img_rect = new Rect(position)
                {
                    y = position.y + 43,
                    width = 64,
                    height = 64
                };
                var Collider_rect = new Rect(position)
                {
                    y = position.y + 43,
                    width = position.width - 70,
                    x = position.x + 70
                };
                var Null_Collider_rect = new Rect(Collider_rect)
                {
                    width = Collider_rect.width - 40,
                    x = Collider_rect.x + 40,
                };
                var state_rect = new Rect(Collider_rect)
                {
                    y = EditorGUIUtility.singleLineHeight + Collider_rect.y + 5,
                    width = Collider_rect.width - 40 - 55
                };
                var gen_buttom_rect = new Rect(Collider_rect)
                {
                    y = 110 - EditorGUIUtility.singleLineHeight * 1.5f + position.y - 3,
                    height = EditorGUIUtility.singleLineHeight * 1.5f
                };
                var clear_buttom_rect = new Rect(Collider_rect)
                {
                    y = EditorGUIUtility.singleLineHeight + Collider_rect.y + 3,
                    x = position.width - 65,
                    width = 40,
                    height = EditorGUIUtility.singleLineHeight * 1.2f
                };
                var debug_buttom_rect = new Rect(Collider_rect)
                {
                    y = EditorGUIUtility.singleLineHeight + Collider_rect.y + 5,
                    x = position.width - 20,
                    width = 55,
                    height = EditorGUIUtility.singleLineHeight * 1.2f
                };
                var new_collider_buttom_rect = new Rect(position)
                {
                    y = position.y + 43,
                    width = 35,
                    x = position.x + 70,
                    height = EditorGUIUtility.singleLineHeight + 1
                };
                var label_rect = new Rect(position)
                {
                    y = position.y + 3,
                    width = 50,
                };
                var Color_label_rect = new Rect(position)
                {
                    y = position.y + EditorGUIUtility.singleLineHeight * 1.5f,
                    width = 50,
                };
                var Color_rect = new Rect(position)
                {
                    y = position.y + 3,
                    x = position.x + label_rect.width,
                    width = position.width - label_rect.width
                };
                var Color_reangeA_rect = new Rect(position)
                {
                    y = position.y + 3,
                    x = position.x + label_rect.width,
                    width = (position.width - label_rect.width) / 2
                };
                var Color_reangeB_rect = new Rect(Color_reangeA_rect)
                {
                    x = Color_reangeA_rect.x + Color_reangeA_rect.width,
                };
                var ColorRe_rect = new Rect(Color_label_rect)
                {
                    x = Color_label_rect.x + Color_label_rect.width,
                    width = 45,
                };
                var ColorR_rect = new Rect(ColorRe_rect)
                {
                    x = ColorRe_rect.x + ColorRe_rect.width,
                    width = (position.width - 150) / 4,
                };
                var ColorG_rect = new Rect(ColorR_rect)
                {
                    x = ColorR_rect.x + ColorR_rect.width,
                };
                var ColorB_rect = new Rect(ColorG_rect)
                {
                    x = ColorG_rect.x + ColorG_rect.width,
                };
                var ColorA_rect = new Rect(ColorB_rect)
                {
                    x = ColorB_rect.x + ColorB_rect.width,
                };
                var ColorRange_rect = new Rect(ColorA_rect)
                {
                    x = ColorA_rect.x + ColorA_rect.width,
                    width = 55,
                };
                #endregion

                #region propertys
                var rootpos_property = property.FindPropertyRelative("rootpos");
                var img_property = property.FindPropertyRelative("img");
                var Collider_property = property.FindPropertyRelative("Collider");
                var points_property = property.FindPropertyRelative("points");
                var Uid_property = property.FindPropertyRelative("Uid");
                var NeedNewCollider_property = property.FindPropertyRelative("NeedNewCollider");
                var Color_property = property.FindPropertyRelative("Color");
                var ColorRangeTo_property = property.FindPropertyRelative("ColorRangeTo");
                var ReverseColor_property = property.FindPropertyRelative("ReverseColor");
                var ColorR_property = property.FindPropertyRelative("ColorR");
                var ColorG_property = property.FindPropertyRelative("ColorG");
                var ColorB_property = property.FindPropertyRelative("ColorB");
                var ColorA_property = property.FindPropertyRelative("ColorA");
                var ColorRange_property = property.FindPropertyRelative("ColorRange");
                var debug_property = property.FindPropertyRelative("debug");
                #endregion

                #region Color
                EditorGUI.LabelField(label_rect, new GUIContent("Color:", "Target color"));
                if (ColorRange_property.boolValue)
                {
                    Color_property.colorValue = EditorGUI.ColorField(Color_reangeA_rect, Color_property.colorValue);
                    ColorRangeTo_property.colorValue = EditorGUI.ColorField(Color_reangeB_rect, ColorRangeTo_property.colorValue);
                }
                else
                {
                    Color_property.colorValue = EditorGUI.ColorField(Color_rect, Color_property.colorValue);
                }
                #endregion

                #region Color Options
                EditorGUI.LabelField(Color_label_rect, new GUIContent("Option:", "Color options"));
                ReverseColor_property.boolValue = EditorGUI.ToggleLeft(ColorRe_rect, new GUIContent("Inv", "Inverse selection"), ReverseColor_property.boolValue);
                ColorR_property.boolValue = EditorGUI.ToggleLeft(ColorR_rect, new GUIContent("R", "Use R channel"), ColorR_property.boolValue);
                ColorG_property.boolValue = EditorGUI.ToggleLeft(ColorG_rect, new GUIContent("G", "Use G channel"), ColorG_property.boolValue);
                ColorB_property.boolValue = EditorGUI.ToggleLeft(ColorB_rect, new GUIContent("B", "Use B channel"), ColorB_property.boolValue);
                ColorA_property.boolValue = EditorGUI.ToggleLeft(ColorA_rect, new GUIContent("A", "Use A channel"), ColorA_property.boolValue);
                ColorRange_property.boolValue = EditorGUI.ToggleLeft(ColorRange_rect, new GUIContent("Range", "Use range color"), ColorRange_property.boolValue);
                #endregion

                #region Sprite
                img_property.objectReferenceValue = EditorGUI.ObjectField(img_rect,
                       img_property.objectReferenceValue, typeof(Sprite), false);
                #endregion

                #region Collider
                Collider_property.objectReferenceValue =
                    EditorGUI.ObjectField(Collider_property.objectReferenceValue != null ? Collider_rect : Null_Collider_rect,
                        Collider_property.objectReferenceValue, typeof(PolygonCollider2D), true);
                if (Collider_property.objectReferenceValue == null)
                {
                    if (GUI.Button(new_collider_buttom_rect, new GUIContent("New", "Game objects with colliders will be created at the child level")))
                    {
                        NeedNewCollider_property.boolValue = true;
                    }
                }
                else
                {
                    NeedNewCollider_property.boolValue = false;
                }
                #endregion

                #region did img is empty
                if (img_property.objectReferenceValue != null)
                {
                    #region debug
                    debug_property.boolValue =
                        EditorGUI.ToggleLeft(debug_buttom_rect, "Debug", debug_property.boolValue);
                    #endregion

                    var sprite = (Sprite)img_property.objectReferenceValue;

                    #region did texture is Readable
                    if (sprite.texture.isReadable)
                    {
                        #region did it generated
                        EditorGUI.LabelField(state_rect, Uid_property.stringValue == "0" ? "Not generated" : "Already generated");
                        if (Uid_property.stringValue != "0")
                        {
                            if (GUI.Button(clear_buttom_rect, "Clear"))
                            {
                                points_property.ClearArray();
                                Uid_property.stringValue = "0";
                            }
                        }
                        if (GUI.Button(gen_buttom_rect, Uid_property.stringValue == "0" ? "Generate" : "Regenerate"))
                        {
                            #region do generate

                            #region GenCheckCB
                            Func<Color, bool> GenCheckCB()
                            {
                                var Color = Color_property.colorValue;
                                var ReverseColor = ReverseColor_property.boolValue;
                                var ColorR = ColorR_property.boolValue;
                                var ColorG = ColorG_property.boolValue;
                                var ColorB = ColorB_property.boolValue;
                                var ColorA = ColorA_property.boolValue;

                                if (ColorRange_property.boolValue)
                                {
                                    var ColorRangeTo = ColorRangeTo_property.colorValue;
                                    bool CB(Color color)
                                    {
                                        var check_vals = new List<bool>();
                                        if (ColorR)
                                        {
                                            var max = Mathf.Max(Color.r, ColorRangeTo.r);
                                            var min = Mathf.Max(Color.r, ColorRangeTo.r);
                                            check_vals.Add(color.r >= min && color.r <= max);
                                        }
                                        if (ColorG)
                                        {
                                            var max = Mathf.Max(Color.g, ColorRangeTo.g);
                                            var min = Mathf.Max(Color.g, ColorRangeTo.g);
                                            check_vals.Add(color.g >= min && color.g <= max);
                                        }
                                        if (ColorB)
                                        {
                                            var max = Mathf.Max(Color.b, ColorRangeTo.b);
                                            var min = Mathf.Max(Color.b, ColorRangeTo.b);
                                            check_vals.Add(color.b >= min && color.b <= max);
                                        }
                                        if (ColorA)
                                        {
                                            var max = Mathf.Max(Color.a, ColorRangeTo.a);
                                            var min = Mathf.Max(Color.a, ColorRangeTo.a);
                                            check_vals.Add(color.a >= min && color.a <= max);
                                        }
                                        if (ReverseColor)
                                        {
                                            check_vals = check_vals.Select(v => !v).ToList();
                                        }
                                        foreach (var item in check_vals)
                                        {
                                            if (!item) return false;
                                        }
                                        return true;
                                    }
                                    return CB;
                                }
                                else
                                {
                                    bool CB(Color color)
                                    {
                                        var check_vals = new List<bool>();
                                        if (ColorR)
                                        {
                                            check_vals.Add(color.r == Color.r);
                                        }
                                        if (ColorG)
                                        {
                                            check_vals.Add(color.g == Color.g);
                                        }
                                        if (ColorB)
                                        {
                                            check_vals.Add(color.b == Color.b);
                                        }
                                        if (ColorA)
                                        {
                                            check_vals.Add(color.a == Color.a);
                                        }
                                        if (ReverseColor)
                                        {
                                            check_vals = check_vals.Select(v => !v).ToList();
                                        }
                                        foreach (var item in check_vals)
                                        {
                                            if (!item) return false;
                                        }
                                        return true;
                                    }
                                    return CB;
                                }
                            }
                            #endregion

                            #region Gen and show log
                            Vector2[][] points = null;
                            try
                            {
                                points = new BoxGen(sprite, rootpos_property.vector3Value, GenCheckCB(), debug_property.boolValue).Gen();
                            }
                            catch (BoxGenException e)
                            {
                                Debug.LogError(e.Message);
                                return;
                            }
                            #endregion

                            #region Flat
                            var new_poitns = new List<Vector3>();
                            points_property.ClearArray();
                            for (int i = 0; i < points.Length; i++)
                            {
                                foreach (var v in points[i])
                                {
                                    new_poitns.Add(new Vector3(i, v.x, v.y));
                                }
                            }
                            points_property.arraySize = new_poitns.Count;
                            for (int i = 0; i < new_poitns.Count; i++)
                            {
                                var p = points_property.GetArrayElementAtIndex(i);
                                p.vector3Value = new_poitns[i];
                            }
                            #endregion

                            #region Generate guid
                            Uid_property.stringValue = Guid.NewGuid().ToString("N");
                            #endregion

                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        EditorGUI.LabelField(state_rect, new GUIContent("The sprite is not readable, please go to the sprite advanced option settings", "The sprite is not readable, please go to the sprite advanced option settings"));
                    }
                    #endregion
                }
                else
                {
                    EditorGUI.LabelField(state_rect, "No sprite selected");
                }
                #endregion
            }
        }
    }
    #endregion

    #region BoxGen
    class BoxGen
    {
        readonly Sprite sprite;
        readonly Vector2 rootpos;
        readonly Texture2D texture;
        readonly float pixels_per_unit;
        readonly Vector2 move_pivot;
        readonly Func<Color, bool> checkCb;
        readonly bool debug;

        public BoxGen(Sprite sprite, Vector2 rootpos, Func<Color, bool> checkCb, bool debug)
        {
            this.sprite = sprite;
            this.rootpos = rootpos;
            this.checkCb = checkCb;
            this.debug = debug;
            texture = sprite.texture;
            pixels_per_unit = 1 / sprite.pixelsPerUnit;
            move_pivot = sprite.pivot;
        }

        #region DebugNormal
        void DebugNormal(Vector2 point, Di di, Color c)
        {
            point = (point - move_pivot) * pixels_per_unit + rootpos;
            var m = 0.5f * pixels_per_unit;
            var h = new Vector2(m, m);
            Vector2 d;
            if (di == Di.left) d = new Vector2(-m, 0);
            else if (di == Di.right) d = new Vector2(m, 0);
            else if (di == Di.up) d = new Vector2(0, -m);
            else d = new Vector2(0, m);
            Debug.DrawLine(point + d + h, point + h, c);
        }
        #endregion

        #region DebugArrow
        void DebugArrow(Vector2 from, Vector2 to, Color c)
        {
            from = (from - move_pivot) * pixels_per_unit + rootpos;
            to = (to - move_pivot) * pixels_per_unit + rootpos;
            var m = 0.3f * pixels_per_unit;
            Debug.DrawLine(from, to, c);
            var di = GetDi(from, to);
            switch (di)
            {
                case Di.down:
                    Debug.DrawLine(to, to + new Vector2(-m, -m), c);
                    Debug.DrawLine(to, to + new Vector2(m, -m), c);
                    break;
                case Di.up:
                    Debug.DrawLine(to, to + new Vector2(-m, m), c);
                    Debug.DrawLine(to, to + new Vector2(m, m), c);
                    break;
                case Di.left:
                    Debug.DrawLine(to, to + new Vector2(m, -m), c);
                    Debug.DrawLine(to, to + new Vector2(m, m), c);
                    break;
                case Di.right:
                    Debug.DrawLine(to, to + new Vector2(-m, -m), c);
                    Debug.DrawLine(to, to + new Vector2(-m, m), c);
                    break;
            }
        }
        #endregion

        #region DebugPoint
        void DebugPoint(Vector2 point, Color c)
        {
            point = (point - move_pivot) * pixels_per_unit + rootpos;
            var m = 0.1f * pixels_per_unit;
            var h = new Vector2(0.5f * pixels_per_unit, 0.5f * pixels_per_unit);
            Debug.DrawLine(point + new Vector2(-m, -m) + h, point + new Vector2(m, m) + h, c);
            Debug.DrawLine(point + new Vector2(m, -m) + h, point + new Vector2(-m, m) + h, c);
        }
        #endregion

        #region Di direction
        /// <summary>
        /// direction enum
        /// </summary>
        enum Di
        {
            /// <summary>
            /// null
            /// </summary>
            none = 0,
            /// <summary>
            /// ↑
            /// </summary>
            up = 1,
            /// <summary>
            /// ↓
            /// </summary>
            down = 2,
            /// <summary>
            /// ←
            /// </summary>
            left = 3,
            /// <summary>
            /// →
            /// </summary>
            right = 6,
        }
        #endregion

        #region GetDi
        Di GetDi(Vector2 a, Vector2 b)
        {
            if (a.x == b.x)
            {
                if (a.y == b.y)
                {
                    return Di.none;
                }
                else if (a.y > b.y)
                {
                    return Di.up;
                }
                else
                {
                    return Di.down;
                }
            }
            else if (a.y == b.y)
            {
                if (a.x > b.x)
                {
                    return Di.left;
                }
                else
                {
                    return Di.right;
                }
            }
            else if (a.x > b.x)
            {
                if (a.y > b.y)
                {
                    return Di.up | Di.left;
                }
                else
                {
                    return Di.down | Di.left;
                }
            }
            else
            {
                if (a.y > b.y)
                {
                    return Di.up | Di.right;
                }
                else
                {
                    return Di.down | Di.right;
                }
            }
        }
        #endregion

        MutualValueGraph<Vector2, Vector2> mugraph = new MutualValueGraph<Vector2, Vector2>();
        List<List<Vector2>> points = new List<List<Vector2>>();

        #region find edge and normal
        void FindEdge()
        {
            for (int y = 0; y < texture.height; y++)
            {
                for (int x = 0; x < texture.width; x++)
                {
                    var pixel = texture.GetPixel(x, y);
                    if (checkCb(pixel))
                    {
                        DebugPoint(new Vector2(x, y), Color.gray * Color.gray);
                        if (x == 0 || !checkCb(texture.GetPixel(x - 1, y)))
                        {
                            mugraph.Set(new Vector2(x, y), new Vector2(x, y + 1), new Vector2(x, y));
                            DebugNormal(new Vector2(x, y), Di.left, Color.cyan * Color.gray);
                        }
                        if (x == texture.width - 1 || !checkCb(texture.GetPixel(x + 1, y)))
                        {
                            mugraph.Set(new Vector2(x + 1, y), new Vector2(x + 1, y + 1), new Vector2(x, y));
                            DebugNormal(new Vector2(x, y), Di.right, Color.cyan * Color.gray);
                        }
                        if (y == 0 || !checkCb(texture.GetPixel(x, y - 1)))
                        {
                            mugraph.Set(new Vector2(x, y), new Vector2(x + 1, y), new Vector2(x, y));
                            DebugNormal(new Vector2(x, y), Di.up, Color.cyan * Color.gray);
                        }
                        if (y == texture.height - 1 || !checkCb(texture.GetPixel(x, y + 1)))
                        {
                            mugraph.Set(new Vector2(x, y + 1), new Vector2(x + 1, y + 1), new Vector2(x, y));
                            DebugNormal(new Vector2(x, y), Di.down, Color.cyan * Color.gray);
                        }
                    }
                }
            }
            if (mugraph.Count == 0) throw new BoxGenException("No edges found");
        }
        #endregion

        #region gen points
        void GenPoints()
        {
            var remove_points = new List<Vector2> { };
            var remove_4_points = new List<(Vector2, Vector2, Vector2)> { };

            var now_points = new List<Vector2> { };

            var first = mugraph.First();
            var last = first;
            var now = last;

            var last_di = Di.none;

            var last_p = mugraph[last, mugraph[last].First()].First();

            #region ToRemove
            void ToRemove()
            {
                foreach (var item in remove_points)
                {
                    mugraph.Remove(item);
                }
                foreach (var (l, n, nn) in remove_4_points)
                {
                    mugraph.UnSet(l, n);
                    mugraph.UnSet(n, nn);
                    if (mugraph[n] != null && mugraph[n].Count() == 0)
                    {
                        mugraph.Remove(n);
                    }
                }
            }
            #endregion

            var len = mugraph.Count;
            for (int i = 0; i < len; i++)
            {
                var to = mugraph[now].ToArray();

                #region GetNow
                Vector2 GetNow()
                {
                    if (to.Length == 0)
                    {
                        return default;
                    }
                    else if (to.Length == 1)
                    {
                        DebugArrow(now, to[0], Color.red);
                        DebugPoint(mugraph[now, to[0]].First(), Color.red);
                        return to[0];
                    }
                    else if (to.Length == 2)
                    {
                        Vector2 nn;
                        if (to[0] == last) nn = to[1];
                        else nn = to[0];
                        remove_points.Add(now);
                        return nn;
                    }
                    else //length == 4
                    {
                        var nl = to.Where(p => p != last && GetDi(now, p) != last_di).ToArray();
                        bool check(Vector2 N)
                        {
                            var p = mugraph[now, N].First();
                            if (p == last_p)
                            {
                                DebugPoint(p, Color.yellow);
                            }
                            return p == last_p;
                        }
                        var nn = nl.Where(N => check(N)).First();
                        i--;
                        DebugArrow(now, nn, Color.yellow);
                        remove_4_points.Add((last, now, nn));
                        return nn;
                    }
                }
                #endregion

                var n = GetNow();

                #region check error and debug
                if (to.Length < 2) throw new BoxGenException("Accidents");
                if (to.Length == 2)
                {
                    if (now == first) DebugArrow(now, n, Color.gray);
                    else DebugArrow(now, n, Color.green);
                }
                #endregion

                if (n == first)
                {
                    last_p = mugraph[now, n].First();

                    #region debug
                    DebugArrow(now, first, Color.blue);
                    DebugPoint(last_p, Color.blue);
                    #endregion

                    now_points.Add(now);
                    points.Add(now_points);

                    ToRemove();

                    if (mugraph.Count == 0) return;

                    #region reset
                    i = 0;
                    first = mugraph.First();
                    remove_points = new List<Vector2> { };
                    remove_4_points.Clear();
                    now_points = new List<Vector2> { };
                    last = first;
                    now = first;
                    last_di = Di.none;
                    last_p = mugraph[last, mugraph[last].First()].First();
                    #endregion

                    continue;
                }
                else
                {
                    var di = GetDi(now, n);
                    last_p = mugraph[now, n].First();

                    #region debug
                    if (to.Length == 2)
                    {
                        if (now == first)
                        {
                            DebugPoint(last_p, Color.gray);
                        }
                        else
                        {
                            DebugPoint(last_p, Color.green);
                        }
                    }
                    #endregion

                    last = now;
                    now = n;

                    if (last_di != di)
                    {
                        now_points.Add(last);
                    }
                    last_di = di;
                }

            }

            throw new BoxGenException("No closure found");
        }
        #endregion

        #region Gen Function
        /// <summary>
        /// Gen Function
        /// </summary>
        /// <param name="sprite">target sprite</param>
        /// <param name="rootpos">root position of target object</param>
        /// <param name="checkCb">A callback that determines whether a pixel matches</param>
        /// <param name="debug">is it debug mode</param>
        /// <returns></returns>
        public Vector2[][] Gen()
        {
            FindEdge();

            GenPoints();

            if (debug) throw new BoxGenException("Debug mode");

            return points.Select(a => a.Select(v => (v - move_pivot) * pixels_per_unit).ToArray()).ToArray();
        }
        #endregion
    }
    #endregion

    #region BoxGenException
    class BoxGenException : Exception
    {
        public BoxGenException(string msg) : base("Generate failed: " + msg) { }
    }
    #endregion

}
#endif