using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace PixelBoxGen
{
    [AddComponentMenu("PixelBoxGen/BoxFrame")]
    [ExecuteInEditMode]
    public class BoxFrame : MonoBehaviour
    {
        public PolygonCollider2D TheCollider;
        public int some = 1;
        [HideInInspector]
        public int select;
        private int? now_select;
        [HideInInspector]
        [SerializeField]
        public ColliderImageFrame[] ColliderImages;

        void Update()
        {
            if (TheCollider == null || ColliderImages == null || ColliderImages.Length < 1)
            {
                if (Application.isPlaying)
                {
                    enabled = false;
                }
                return;
            }
#if UNITY_EDITOR
            var need_update = false;
            if (!Application.isPlaying)
            {
                foreach (var item in ColliderImages)
                {
                    item.rootpos = transform.position;
                    if (item.LastUid != item.Uid || item.cache_points == null)
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
                            TheCollider.pathCount = 0;
                        }
                        need_update = true;
                    }
                }
            }
            if (need_update)
            {
                SetCollider(ColliderImages[select]);
            }
#endif
            if (now_select != select)
            {
                SetCollider(ColliderImages[select]);
                now_select = select;
            }
        }

        void SetCollider(ColliderImageFrame item)
        {
            Debug.Log(item);
            TheCollider.pathCount = item.cache_points.Length;
            for (int i = 0; i < item.cache_points.Length; i++)
            {
                TheCollider.SetPath(i, item.cache_points[i]);
            }
        }

#if UNITY_EDITOR
#region UNITY_EDITOR
        static ConditionalWeakTable<string, Vector2[][]> cache = new ConditionalWeakTable<string, Vector2[][]>();

        string get_ustr<T>(T v)
        {
            return string.Intern(v.ToString());
        }

        public static void Gen(ColliderImageFrame item)
        {
            if (item.points != null)
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
                item.cache_points = cache_points;
            }
        }
#endregion
#endif
    }

}
