using System.Windows;

namespace LightImage.DragDrop
{
    /// <summary>
    /// Extension methods for converting between Windows and dragdrop coordinates.
    /// </summary>
    public static class ConversionExtensions
    {
        /// <summary>
        /// Convert a <see cref="DropPoint"/> to a <see cref="Point"/>.
        /// </summary>
        /// <param name="pt">The point to be converted.</param>
        /// <returns>The converted point.</returns>
        public static Point AsPoint(this DropPoint pt)
        {
            return new Point(pt.X, pt.Y);
        }

        /// <summary>
        /// Convert a <see cref="Point"/> to a <see cref="DropPoint"/>.
        /// </summary>
        /// <param name="pt">The point to be converted.</param>
        /// <returns>The converted point.</returns>
        public static DropPoint AsDropPoint(this Point pt)
        {
            return new DropPoint(pt.X, pt.Y);
        }
    }
}