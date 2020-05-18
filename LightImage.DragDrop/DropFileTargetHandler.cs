using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LightImage.DragDrop
{
    /// <summary>
    /// Base class for drop handlers that receive files.
    /// </summary>
    /// <typeparam name="TDestination">Type of items in the target control.</typeparam>
    public abstract class DropFileTargetHandler<TDestination> : AbstractDropHandler<TDestination>
        where TDestination : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DropFileTargetHandler{TDestination}"/> class.
        /// </summary>
        /// <param name="acceptNullTarget">Value indicating whether items can be dropped on an empty spot without items.</param>
        /// <param name="allOrNothing">Value indicating whether all dragged items must be accepted in order for the drop action to succeed.</param>
        public DropFileTargetHandler(bool acceptNullTarget, bool allOrNothing)
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

            if (!GetSourceFiles(info, target, out var source))
            {
                throw new InvalidOperationException($"Drop action should have been blocked");
            }

            if (!AcceptFileSet(source, target))
            {
                throw new InvalidOperationException($"Drop action should have been blocked");
            }

            return Handle(source, target);
        }

        /// <inheritdoc/>
        public override bool Match(IDragDropInfo info)
        {
            if (!GetTargetItem(info.TargetItem, out var target))
            {
                return false;
            }

            if (!GetSourceFiles(info, target, out var files))
            {
                return false;
            }

            if (!AcceptFileSet(files, target))
            {
                return false;
            }

            info.Effect = DragDropEffect.Link;
            return true;
        }

        /// <summary>
        /// Check if a given set of files would be accepted for a given target item.
        /// </summary>
        /// <param name="files">The files being dragged.</param>
        /// <param name="target">The hovered item in the target control.</param>
        /// <returns>Value indicating whether the files would be accepted when dropped on the taret.</returns>
        protected virtual bool AcceptFileSet(FileInfo[] files, TDestination target)
        {
            return true;
        }

        /// <summary>
        /// Handle a collection of files being dropped.
        /// </summary>
        /// <param name="source">Files being dropped on the target control.</param>
        /// <param name="target">Item in the target control on which the files are being dropped.</param>
        /// <returns>Task representing the asynchronous handling of the drop action.</returns>
        protected abstract Task Handle(FileInfo[] source, TDestination target);

        /// <summary>
        /// Check if a file is accepted when dropped on the target control.
        /// </summary>
        /// <param name="target">The item in the target control under the mouse cursor.</param>
        /// <param name="file">File being dropped.</param>
        /// <returns>Value indicating whether the file is accepted.</returns>
        protected abstract bool Match(TDestination target, FileInfo file);

        private bool GetFiles(IDragDropInfo info, out string[] files)
        {
            files = info.Files;
            return files != null;
        }

        private bool GetSourceFiles(IDragDropInfo dropInfo, TDestination target, out FileInfo[] files)
        {
            files = null;
            if (!GetFiles(dropInfo, out var source))
            {
                return false;
            }

            files = source.Select(file => new FileInfo(file)).Where(info => Match(target, info)).ToArray();
            var allMatches = source.Count() == files.Count();
            return AllOrNothing ? allMatches : source.Any();
        }
    }
}