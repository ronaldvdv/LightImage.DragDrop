using System.Collections;
using LightImage.Util.Geometry;

namespace LightImage.DragDrop
{
    /// <summary>
    /// Information on a drag or drop operation.
    /// </summary>
    public interface IDragDropInfo
    {
        /// <summary>
        /// Gets the data being dragged.
        /// </summary>
        object Data { get; }

        /// <summary>
        /// Gets the mouse coordinates relative to the drop target control.
        /// </summary>
        PointD DropPosition { get; }

        /// <summary>
        /// Gets or sets the effect for visualizing the type of action.
        /// </summary>
        DragDropEffect Effect { get; set; }

        /// <summary>
        /// Gets the collection of paths for files being dragged, or NULL if not applicable.
        /// </summary>
        string[] Files { get; }

        /// <summary>
        /// Gets the index within the target container where dropped items would be positioned.
        /// </summary>
        int Index { get; }

        /// <summary>
        /// Gets a value indicating whether the Control key is being pressed during the action.
        /// </summary>
        bool IsCtrlPressed { get; }

        /// <summary>
        /// Gets the collection of items in the source control where the dragging started.
        /// </summary>
        IEnumerable SourceItems { get; }

        /// <summary>
        /// Gets the item in the target control under the mouse cursor.
        /// </summary>
        object TargetItem { get; }

        /// <summary>
        /// Gets the target control.
        /// </summary>
        object VisualTarget { get; }
    }
}