#if UNITY_EDITOR
using System;
using UnityEngine;

namespace PixelBoxGen
{
    #region ColliderImage
    [Serializable]
    public class ColliderImage
    {
        #region Uid
        [NonSerialized]
        public string LastUid = null;
        [NonSerialized]
        public string StrUid = null;
        [SerializeField]
        public string Uid = "0";
        #endregion

        #region Color
        [SerializeField]
        public bool ReverseColor = true;
        [SerializeField]
        public bool ColorRange = false;
        [SerializeField]
        public bool ColorR = false;
        [SerializeField]
        public bool ColorG = false;
        [SerializeField]
        public bool ColorB = false;
        [SerializeField]
        public bool ColorA = true;
        [SerializeField]
        public Color Color = new Color(0, 0, 0, 0);
        [SerializeField]
        public Color ColorRangeTo = new Color(0, 0, 0, 0);
        #endregion

        #region Collider
        [SerializeField]
        public PolygonCollider2D Collider = null;
        [SerializeField]
        public bool NeedNewCollider = false;
        #endregion

        #region Sprite
        [SerializeField]
        public Sprite img = null;
        #endregion

        #region points
        [SerializeField]
        public Vector3[] points = null;
        [NonSerialized]
        public Vector2[][] cache_points;
        #endregion

        #region rootpos
        [SerializeField]
        public Vector3 rootpos;
        #endregion

        #region debug
        [SerializeField]
        public bool debug;
        #endregion
    }
    #endregion
}
#endif