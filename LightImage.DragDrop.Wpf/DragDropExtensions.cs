using System;
using System.Collections;
using System.Reactive.Disposables;
using System.Windows;
using System.Windows.Controls;
using GongSolutions.Wpf.DragDrop;
using LightImage.Util.Geometry;

using GongDD = GongSolutions.Wpf.DragDrop;

namespace LightImage.DragDrop
{
    /// <summary>
    /// Extension methods related to controls and drop targets.
    /// </summary>
    public static class DragDropExtensions
    {
        /// <summary>
        /// Convert a <see cref="DropTarget"/> view model to a <see cref="IDropTarget"/> instance.
        /// </summary>
        /// <param name="target">The target view model to be converted.</param>
        /// <returns>An wrapper instance of a <see cref="IDropTarget"/> for the given view model.</returns>
        public static IDropTarget AsGongHandler(this DropTarget target)
        {
            return new DropHandlerAdapter(target);
        }

        /// <summary>
        /// Bind a drop target to a control.
        /// </summary>
        /// <param name="control">The control that should get drop target behaviour.</param>
        /// <param name="handler">The drop target handler to be used.</param>
        /// <returns>A disposable that can be used to unassign the behaviour.</returns>
        public static IDisposable BindDropHandler(this Control control, DropTarget handler)
        {
            control.SetValue(GongDD.DragDrop.DropHandlerProperty, handler.AsGongHandler());
            control.SetValue(GongDD.DragDrop.IsDropTargetProperty, true);

            return Disposable.Create(() => ClearDragDrop(control));
        }

        /// <summary>
        /// Clear dragdrop behaviour from a control.
        /// </summary>
        /// <param name="control">Control for which the behaviour should be removed.</param>
        public static void ClearDragDrop(this Control control)
        {
            control.SetValue(GongDD.DragDrop.DropHandlerProperty, null);
            control.SetValue(GongDD.DragDrop.IsDropTargetProperty, false);
        }

        private sealed class DragDropInfo : IDragDropInfo
        {
            private readonly IDropInfo _info;

            public DragDropInfo(IDropInfo info)
            {
                _info = info;
                CheckFiles();
                DropPosition = new PointD(info.DropPosition.X, info.DropPosition.Y);
                Index = info.InsertIndex;
                if (VisualTarget is IDropPositionTransformer transformer)
                {
                    DropPosition = transformer.TransformDropPosition(DropPosition);
                }
            }

            public object Data => _info.Data;

            public PointD DropPosition { get; }

            public DragDropEffect Effect
            {
                get => GetEffect();
                set => SetEffect(value);
            }

            public string[] Files { get; private set; }

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

        private class DropHandlerAdapter : IDropTarget
        {
            private readonly DropTarget _target;
            private IDropInfo _lastInput;
            private IDragDropInfo _lastOutput;

            public DropHandlerAdapter(DropTarget target)
            {
                _target = target;
            }

            public void DragOver(IDropInfo dropInfo)
            {
                _target.DragOver(Adapt(dropInfo));
            }

            public void Drop(IDropInfo dropInfo)
            {
                _ = _target.Drop(Adapt(dropInfo));
            }

            private IDragDropInfo Adapt(IDropInfo info)
            {
                if (info != _lastInput)
                {
                    _lastInput = info;
                    _lastOutput = new DragDropInfo(info);
                }

                return _lastOutput;
            }
        }
    }
}