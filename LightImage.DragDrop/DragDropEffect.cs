using System;

namespace LightImage.DragDrop
{
    /// <summary>
    /// Effect to be used for the drag action (this typically translates to an an icon or cursor).
    /// </summary>
    [Flags]
    public enum DragDropEffect
    {
        /// <summary>
        /// Don't allow the action.
        /// </summary>
        None = 0,

        /// <summary>
        /// Indicate that a copy of the dragged content will be made in the drop target.
        /// </summary>
        Copy = 1,

        /// <summary>
        /// Indicate that the dragged content will move to the drop target.
        /// </summary>
        Move = 2,

        /// <summary>
        /// Indicate that the drop target will be linked to the dragged item.
        /// </summary>
        Link = 4,
    }
}