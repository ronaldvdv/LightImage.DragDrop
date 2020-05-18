using System.Threading.Tasks;

namespace LightImage.DragDrop
{
    /// <summary>
    /// Contract for drop handlers.
    /// </summary>
    public interface IDropTargetHandler
    {
        /// <summary>
        /// Handle a drop operation asynchronously.
        /// </summary>
        /// <param name="info">Info regarding the drop action.</param>
        /// <returns>Task representing the asynchronous handling of a drop action.</returns>
        Task Execute(IDragDropInfo info);

        /// <summary>
        /// Test if a drop operation would be accepted.
        /// </summary>
        /// <param name="info">Info regarding the drop action.</param>
        /// <returns>Value indicating whether the operation would be accepted.</returns>
        bool Match(IDragDropInfo info);
    }
}