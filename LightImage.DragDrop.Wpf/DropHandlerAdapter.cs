using System;
using GongSolutions.Wpf.DragDrop;

namespace LightImage.DragDrop
{
    /// <summary>
    /// Internal adapter for converting a viewmodel <see cref="DropTarget"/> to a view <see cref="IDropTarget"/>.
    /// </summary>
    internal class DropHandlerAdapter : IDropTarget
    {
        private readonly DropTarget _target;
        private readonly GetDropAdornerDelegate _dropAdorner;
        private IDropInfo _lastInput;
        private IDragDropInfo _lastOutput;

        /// <summary>
        /// Initializes a new instance of the <see cref="DropHandlerAdapter"/> class.
        /// </summary>
        /// <param name="target">The view model drop target.</param>
        /// <param name="dropAdorner">Callback to determine the type of drop adorner to be used.</param>
        public DropHandlerAdapter(DropTarget target, GetDropAdornerDelegate dropAdorner = null)
        {
            _target = target;
            _dropAdorner = dropAdorner ?? GetDefaultDropAdorner;
        }

        /// <inheritdoc/>
        public void DragOver(IDropInfo dropInfo)
        {
            var adapted = Adapt(dropInfo);
            _target.DragOver(adapted);
            dropInfo.DropTargetAdorner = _dropAdorner(adapted);
        }

        /// <inheritdoc/>
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

        private Type GetDefaultDropAdorner(IDragDropInfo info)
        {
            return info.RelativePosition switch
            {
                RelativePosition.Before => DropTargetAdorners.Insert,
                RelativePosition.On => DropTargetAdorners.Highlight,
                RelativePosition.After => DropTargetAdorners.Insert,
                _ => null
            };
        }
    }
}