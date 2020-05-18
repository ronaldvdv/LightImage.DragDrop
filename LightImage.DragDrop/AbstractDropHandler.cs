using System.Threading.Tasks;

namespace LightImage.DragDrop
{
    /// <summary>
    /// Base class for individual drop handlers.
    /// </summary>
    /// <typeparam name="TDestination">Type of item in the target control.</typeparam>
    public abstract class AbstractDropHandler<TDestination> : IDropTargetHandler
        where TDestination : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractDropHandler{TDestination}"/> class.
        /// </summary>
        /// <param name="acceptNullTarget">Value indicating whether dropping onto an empty space will also be allowed, giving a target=NULL.</param>
        /// <param name="allOrNothing">Value indicating whether drop actions will only be accepted when all selected items pass the given predicate.</param>
        public AbstractDropHandler(bool acceptNullTarget, bool allOrNothing)
        {
            AcceptNullTarget = acceptNullTarget;
            AllOrNothing = allOrNothing;
        }

        /// <summary>
        /// Gets a value indicating whether drop actions will only be accepted when all selected items pass the given predicate.
        /// </summary>
        public bool AllOrNothing { get; }

        /// <summary>
        /// Gets a value indicating whether dropping onto an empty space will also be allowed, giving a target=NULL.
        /// </summary>
        protected bool AcceptNullTarget { get; }

        /// <inheritdoc/>
        public abstract Task Execute(IDragDropInfo info);

        /// <inheritdoc/>
        public abstract bool Match(IDragDropInfo info);

        /// <summary>
        /// Convert an untyped target item to the desired type and check if it would be accepted based on its type and <see cref="AcceptNullTarget"/>.
        /// </summary>
        /// <param name="data">The untyped target item.</param>
        /// <param name="target">The converted target item.</param>
        /// <returns>Value indicating whether the target has the right type or <see cref="AcceptNullTarget"/> is true.</returns>
        protected bool GetTargetItem(object data, out TDestination target)
        {
            target = data as TDestination;
            return target != null || AcceptNullTarget;
        }
    }
}