using System.Collections;
using System.Windows;
using GongSolutions.Wpf.DragDrop;

namespace LightImage.DragDrop
{
    /// <summary>
    /// Drag-drop information holder.
    /// </summary>
    internal sealed class DragDropInfo : IDragDropInfo
    {
        private readonly IDropInfo _info;

        /// <summary>
        /// Initializes a new instance of the <see cref="DragDropInfo"/> class.
        /// </summary>
        /// <param name="info">View-related drop info.</param>
        internal DragDropInfo(IDropInfo info)
        {
            _info = info;
            CheckFiles();
            DropPosition = new DropPoint(info.DropPosition.X, info.DropPosition.Y);
            Index = info.InsertIndex;
            if (VisualTarget is IDropPositionTransformer transformer)
            {
                DropPosition = transformer.TransformDropPosition(DropPosition);
            }
        }

        /// <inheritdoc/>
        public object Data => _info.Data;

        /// <inheritdoc/>
        public DropPoint DropPosition { get; }

        /// <inheritdoc/>
        public DragDropEffect Effect
        {
            get => GetEffect();
            set => SetEffect(value);
        }

        /// <inheritdoc/>
        public string[] Files { get; private set; }

        /// <inheritdoc/>
        public int Index { get; }

        /// <inheritdoc/>
        public bool IsCtrlPressed => _info.KeyStates.HasFlag(DragDropKeyStates.ControlKey);

        /// <inheritdoc/>
        public IEnumerable SourceItems => _info.DragInfo?.SourceItems;

        /// <inheritdoc/>
        public object TargetItem
        {
            get
            {
                if (_info.TargetItem != null)
                {
                    return _info.TargetItem;
                }

                if (_info.TargetGroup != null)
                {
                    return _info.TargetGroup.Name;
                }

                return null;
            }
        }

        /// <inheritdoc/>
        public object VisualTarget => _info.VisualTarget;

        /// <inheritdoc/>
        public RelativePosition RelativePosition => ConvertRelativePosition(_info.InsertPosition);

        private RelativePosition ConvertRelativePosition(RelativeInsertPosition insertPosition)
        {
            if (insertPosition.HasFlag(RelativeInsertPosition.TargetItemCenter))
            {
                return RelativePosition.On;
            }

            if (insertPosition.HasFlag(RelativeInsertPosition.BeforeTargetItem))
            {
                return RelativePosition.Before;
            }

            if (insertPosition.HasFlag(RelativeInsertPosition.AfterTargetItem))
            {
                return RelativePosition.After;
            }

            return RelativePosition.None;
        }

        private void CheckFiles()
        {
            if (_info.Data is IDataObject data && data.GetDataPresent(DataFormats.FileDrop))
            {
                Files = (string[])data.GetData(DataFormats.FileDrop);
            }
        }

        private DragDropEffect GetEffect()
        {
            return (DragDropEffect)(int)_info.Effects;
        }

        private void SetEffect(DragDropEffect value)
        {
            _info.Effects = (DragDropEffects)(int)value;
        }
    }
}