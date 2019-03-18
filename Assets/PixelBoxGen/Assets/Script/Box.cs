using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace PixelBoxGen
{
    [AddComponentMenu("PixelBoxGen/Box")]
    [ExecuteInEditMode]
    public class Box : MonoBehaviour
    {

#if UNITY_EDITOR
        #region UNITY_EDITOR
        static ConditionalWeakTable<string, Vector2[][]> cache = new ConditionalWeakTable<string, Vector2[][]>();

        string get_ustr<T>(T v)
        {
            return string.Intern(v.ToString());
        }

        [HideInInspector]
        [SerializeField]
        public ColliderImage[] ColliderImages;

        void Update()
        {
            if (Application.isPlaying)
            {
                if (Application.isEditor)
                {
                    enabled = false;
                }
                else
                {
                    Destroy(this);
                }
                return;
            }
            if (ColliderImages == null) return;
            foreach (var item in ColliderImages)
            {
                item.rootpos = transform.position;
                if (item.NeedNewCollider)
                {
                    GameObject obj;
                    if (item.img == null) obj = new GameObject();
                    else obj = new GameObject(item.img.name);
                    item.Collider = obj.AddComponent<PolygonCollider2D>();
                    obj.transform.parent = transform;
                    obj.transform.position = transform.position;
                    obj.transform.rotation = transform.rotation;
                    obj.transform.localScale = transform.localScale;
                    Undo.RegisterCreatedObjectUndo(obj, "Create New Collider");
                    item.NeedNewCollider = false;
                }
                if (item.Collider == null)
                {
                    item.LastUid = null;
                    continue;
                }
                if (item.LastUid != item.Uid)
                {
                    item.LastUid = item.Uid;
                    item.StrUid = get_ustr(item.Uid);
                    cache.Remove(get_ustr(item.StrUid));
                    if (item.points != null)
                    {
                        Gen(item);
                    }
                    else
                    {
                        item.Collider.pathCount = 0;
                    }
                }
            }
        }

        void Gen(ColliderImage item)
        {
            if (item.points != null && item.Collider != null)
            {

                if (!cache.TryGetValue(item.StrUid, out var cache_points))
                {
                    var raw_points = new List<List<Vector2>>();
                    foreach (var point in item.points)
                    {
                        while (point.x >= raw_points.Count)
                        {
                            raw_points.Add(new List<Vector2>());
                        }
                        var pl = raw_points[(int)point.x];
                        pl.Add(new Vector2(point.y, point.z));
                    }
                    cache_points = raw_points.Select(a => a.ToArray()).ToArray();
                    cache.Add(item.StrUid, cache_points);
                }
                item.Collider.pathCount = cache_points.Length;
                for (int i = 0; i < cache_points.Length; i++)
                {
                    item.Collider.SetPath(i, cache_points[i].ToArray());
                }
            }
        }
        #endregion
#else
        private void Update()
        {
            Destroy(this);
        }
#endif
    }
}
