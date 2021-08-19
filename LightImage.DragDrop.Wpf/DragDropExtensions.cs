using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using GongSolutions.Wpf.DragDrop;

using GongDD = GongSolutions.Wpf.DragDrop;

namespace LightImage.DragDrop
{
    /// <summary>
    /// Delegate to determine the type of drop adorner to be used.
    /// </summary>
    /// <param name="info">Current dragdrop information.</param>
    /// <returns>The type of drop adorner to be used.</returns>
    public delegate Type GetDropAdornerDelegate(IDragDropInfo info);

    /// <summary>
    /// Extension methods related to controls and drop targets.
    /// </summary>
    public static class DragDropExtensions
    {
        /// <summary>
        /// Convert a <see cref="DropTarget"/> view model to a <see cref="IDropTarget"/> instance.
        /// </summary>
        /// <param name="target">The target view model to be converted.</param>
        /// <param name="dropAdorner">Callback to determine the type of <see cref="DropTargetAdorner"/> to be used.</param>
        /// <returns>An wrapper instance of a <see cref="IDropTarget"/> for the given view model.</returns>
        public static IDropTarget AsGongHandler(this DropTarget target, GetDropAdornerDelegate dropAdorner = null)
        {
            return new DropHandlerAdapter(target, dropAdorner);
        }

        /// <summary>
        /// Bind a drop target to a control.
        /// </summary>
        /// <param name="control">The control that should get drop target behaviour.</param>
        /// <param name="handler">The drop target handler to be used.</param>
        /// <param name="dropAdorner">Type of drop adorner to be used.</param>
        /// <returns>A disposable that can be used to unassign the behaviour.</returns>
        public static IDisposable BindDropHandler(this Control control, DropTarget handler, GetDropAdornerDelegate dropAdorner = null)
        {
            control.SetValue(GongDD.DragDrop.DropHandlerProperty, handler.AsGongHandler(dropAdorner));
            control.SetValue(GongDD.DragDrop.IsDropTargetProperty, true);

            return new Disposable(() => ClearDragDrop(control));
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
    }
}