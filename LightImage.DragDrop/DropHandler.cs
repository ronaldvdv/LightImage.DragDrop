using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightImage.DragDrop
{
    /// <summary>
    /// Base class for handling drag-drop for specific types of data and target. Subclasses should provide ways
    /// to derive if a specific item+target are accepted and to handle the actual drop response.
    /// </summary>
    /// <typeparam name="TSource">Type of source items being dragged.</typeparam>
    /// <typeparam name="TDestination">Type of items in the target control.</typeparam>
    public abstract class DropHandler<TSource, TDestination> : AbstractDropHandler<TDestination>
        where TSource : class
        where TDestination : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DropHandler{TSource, TDestination}"/> class.
        /// </summary>
        /// <param name="acceptNullTarget">Value indicating whether items can be dropped on an empty spot without items.</param>
        /// <param name="allOrNothing">Value indicating whether all dragged items must be accepted in order for the drop action to succeed.</param>
        public DropHandler(bool acceptNullTarget, bool allOrNothing)
            : base(acceptNullTarget, allOrNothing)
        {
        }

        /// <inheritdoc/>
        public override Task Execute(IDragDropInfo info)
        {
            if (!GetTargetItem(info.TargetItem, out var target))
            {
                throw new InvalidOperationException($"Drop action should have been blocked");
            }

            if (!GetSourceItems(info.SourceItems, target, out var source))
            {
                throw new InvalidOperationException($"Drop action should have been blocked");
            }

            return Handle(source, target, info);
        }

        /// <inheritdoc/>
        public override bool Match(IDragDropInfo info)
        {
            if (info.SourceItems == null)
            {
                return false;
            }

            if (!GetTargetItem(info.TargetItem, out var targetTyped))
            {
                return false;
            }

            var result = GetSourceItems(info.SourceItems, targetTyped, out var sourceTyped);
            OnDrag(targetTyped, sourceTyped, result, info);
            return result;
        }

        /// <summary>
        /// Check if a specific item is allowed to be dropped onto the target.
        /// </summary>
        /// <param name="item">One of the items being dragged.</param>
        /// <param name="target">The item in the target control under the mouse cursor.</param>
        /// <returns>Value indicating the item can be dropped on the target.</returns>
        protected abstract bool Accept(TSource item, TDestination target);

        /// <summary>
        /// Execute the drop execution for a given target and collection of dragged items.
        /// </summary>
        /// <param name="items">The items being dragged.</param>
        /// <param name="target">The item in the target control under the mouse cursor.</param>
        /// <param name="info">Additional information on the drag-drop action.</param>
        /// <returns>Task representing the asynchronous handling of the drop action.</returns>
        protected abstract Task Handle(IEnumerable<TSource> items, TDestination target, IDragDropInfo info);

        /// <summary>
        /// Callback executed when items are dragged above the target control.
        /// </summary>
        /// <param name="target">The item in the target control under the mouse cursor.</param>
        /// <param name="items">The items being dragged.</param>
        /// <param name="accept">The aggregated result of the <see cref="Accept(TSource, TDestination)"/> calls for the given target and items.</param>
        /// <param name="info">Additional information on the drag-drop action.</param>
        protected virtual void OnDrag(TDestination target, IEnumerable<TSource> items, bool accept, IDragDropInfo info)
        {
            info.Effect = accept ? DragDropEffect.Move : DragDropEffect.None;
        }

        private bool GetSourceItems(IEnumerable data, TDestination target, out IEnumerable<TSource> source)
        {
            var objects = data.OfType<object>();
            source = null;
            if (!objects.Any() || !objects.All(m => m is TSource))
            {
                return false;
            }

            source = objects.Cast<TSource>().Where(item => Accept(item, target));
            var allMatches = source.Count() == objects.Count();
            return AllOrNothing ? allMatches : source.Any();
        }
    }
}