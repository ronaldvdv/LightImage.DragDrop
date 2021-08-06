using System.Collections;

namespace LightImage.DragDrop
{
    /// <summary>
    /// Drag info implementation for testing purposes.
    /// </summary>
    public class TestDragDropInfo : IDragDropInfo
    {
        /// <inheritdoc/>
        public object Data { get; set; }

        /// <inheritdoc/>
        public DropPoint DropPosition { get; set; }

        /// <inheritdoc/>
        public DragDropEffect Effect { get; set; } = DragDropEffect.Move;

        /// <inheritdoc/>
        public string[] Files { get; set; }

        /// <inheritdoc/>
        public int Index { get; set; }

        /// <inheritdoc/>
        public bool IsCtrlPressed { get; set; }

        /// <inheritdoc/>
        public IEnumerable SourceItems { get; }

        /// <inheritdoc/>
        public object TargetItem { get; set; }

        /// <inheritdoc/>
        public object VisualTarget { get; set; }

        /// <inheritdoc/>
        public RelativePosition RelativePosition { get; set; }
    }
}