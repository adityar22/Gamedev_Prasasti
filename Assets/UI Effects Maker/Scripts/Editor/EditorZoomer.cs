using UnityEngine;

namespace UIEM
{
    public class EditorZoomer
    {
        private const float kEditorWindowTabHeight = 21.0f;
        public float zoom = 1f;
        private Rect zoomArea = new Rect();
        private Vector2 zoomOrigin = Vector2.zero;
        private Vector2 lastMouse = Vector2.zero;
        private Matrix4x4 prevMatrix;

        public Rect Begin(params GUILayoutOption[] options)
        {
            HandleEvents();

            //fill the available area
            var possibleZoomArea = GUILayoutUtility.GetRect(0, 10000, 0, 10000, options);

            if (Event.current.type == EventType.Repaint) //the size is correct during repaint, during layout it's 1,1
            {
                zoomArea = possibleZoomArea;
            }

            GUI.EndGroup(); // End the group Unity begins automatically for an EditorWindow to clip out the window tab. This allows us to draw outside of the size of the EditorWindow.

            Rect clippedArea = zoomArea.ScaleSizeBy(1f / zoom, zoomArea.TopLeft());
            clippedArea.y += kEditorWindowTabHeight;
            GUI.BeginGroup(clippedArea);

            prevMatrix = GUI.matrix;
            Matrix4x4 translation = Matrix4x4.TRS(clippedArea.TopLeft(), Quaternion.identity, Vector3.one);
            Matrix4x4 scale = Matrix4x4.Scale(new Vector3(zoom, zoom, 1.0f));
            GUI.matrix = translation * scale * translation.inverse * GUI.matrix;

            return clippedArea;
        }

        public void End()
        {
            GUI.matrix = prevMatrix; //restore the original matrix
            GUI.EndGroup();
            GUI.BeginGroup(new Rect(0.0f, kEditorWindowTabHeight, Screen.width, Screen.height));
        }

        public void HandleEvents()
        {
            if (Event.current.isMouse)
            {
                if (Event.current.type == EventType.MouseDrag && ((Event.current.button == 0 && Event.current.modifiers == EventModifiers.Alt) || Event.current.button == 2))
                {
                    var mouseDelta = Event.current.mousePosition - lastMouse;

                    zoomOrigin += mouseDelta;

                    Event.current.Use();
                }

                lastMouse = Event.current.mousePosition;
            }

            if (Event.current.type == EventType.ScrollWheel)
            {
                float oldZoom = zoom;

                float zoomChange = 1.10f;

                zoom *= Mathf.Pow(zoomChange, -Event.current.delta.y / 3f);
                zoom = Mathf.Clamp(zoom, 0.1f, 10f);

                bool shouldZoomTowardsMouse = true; //if this is false, it will always zoom towards the center of the content (0,0)

                if (shouldZoomTowardsMouse)
                {
                    //we want the same content that was under the mouse pre-zoom to be there post-zoom as well
                    //in other words, the content's position *relative to the mouse* should not change

                    Vector2 areaMousePos = Event.current.mousePosition - zoomArea.center;

                    Vector2 contentOldMousePos = (areaMousePos / oldZoom) - (zoomOrigin / oldZoom);
                    Vector2 contentMousePos = (areaMousePos / zoom) - (zoomOrigin / zoom);

                    Vector2 mouseDelta = contentMousePos - contentOldMousePos;

                    zoomOrigin += mouseDelta * zoom;
                }

                Event.current.Use();
            }
        }

        public Vector2 GetContentOffset()
        {
            Vector2 offset = -zoomOrigin / zoom; //offset the midpoint

            offset -= (zoomArea.size / 2f) / zoom; //offset the center

            return offset;
        }
    }

    // Helper Rect extension methods
    public static class RectExtensions
    {
        public static Vector2 TopLeft(this Rect rect)
        {
            return new Vector2(rect.xMin, rect.yMin);
        }

        public static Rect ScaleSizeBy(this Rect rect, float scale)
        {
            return rect.ScaleSizeBy(scale, rect.center);
        }

        public static Rect ScaleSizeBy(this Rect rect, float scale, Vector2 pivotPoint)
        {
            Rect result = rect;
            result.x -= pivotPoint.x;
            result.y -= pivotPoint.y;
            result.xMin *= scale;
            result.xMax *= scale;
            result.yMin *= scale;
            result.yMax *= scale;
            result.x += pivotPoint.x;
            result.y += pivotPoint.y;
            return result;
        }
        public static Rect ScaleSizeBy(this Rect rect, Vector2 scale)
        {
            return rect.ScaleSizeBy(scale, rect.center);
        }
        public static Rect ScaleSizeBy(this Rect rect, Vector2 scale, Vector2 pivotPoint)
        {
            Rect result = rect;
            result.x -= pivotPoint.x;
            result.y -= pivotPoint.y;
            result.xMin *= scale.x;
            result.xMax *= scale.x;
            result.yMin *= scale.y;
            result.yMax *= scale.y;
            result.x += pivotPoint.x;
            result.y += pivotPoint.y;
            return result;
        }
    }
}