using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace LightImage.DragDrop
{
    /// <summary>
    /// Drop target view model, consisting of one or more <see cref="IDropTargetHandler"/> handlers.
    /// </summary>
    public class DropTarget
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DropTarget"/> class.
        /// </summary>
        /// <param name="handlers">Collection of drop handlers.</param>
        public DropTarget(params IDropTargetHandler[] handlers)
        {
            Handlers = new Collection<IDropTargetHandler>(handlers.ToList());
        }

        /// <summary>
        /// Gets the collection of drop handlers.
        /// </summary>
        public ICollection<IDropTargetHandler> Handlers { get; }

        /// <summary>
        /// Handle a drag operation by finding a matching handler and letting it update the <see cref="IDragDropInfo"/>.
        /// </summary>
        /// <param name="info">Drag information.</param>
        public void DragOver(IDragDropInfo info)
        {
            Handlers.FirstOrDefault(h => h.Match(info));
        }

        /// <summary>
        /// Handle a drop operation by finding a matching handler and letting it handle the dropped items asynchronously.
        /// </summary>
        /// <param name="dropInfo">Drop information.</param>
        /// <returns>Task representing the asynchronous handling of the drop action.</returns>
        public Task Drop(IDragDropInfo dropInfo)
        {
            var handler = Handlers.FirstOrDefault(h => h.Match(dropInfo));
            if (handler == null)
            {
                return Task.CompletedTask;
            }

            return handler.Execute(dropInfo);
        }
    }
}