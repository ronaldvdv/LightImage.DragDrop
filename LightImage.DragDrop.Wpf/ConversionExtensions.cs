using System.Windows;

namespace LightImage.DragDrop
{
    /// <summary>
    /// Extension methods for converting between <see cref="Geometry"/> and <see cref="System.Windows"/> geometry.
    /// </summary>
    public static class ConversionExtensions
    {
        /// <summary>
        /// Convert a <see cref="PointD"/> to a <see cref="Point"/>.
        /// </summary>
        /// <param name="pt">The point to be converted.</param>
        /// <returns>The converted point.</returns>
        public static Point AsPoint(this DropPoint pt)
        {
            return new Point(pt.X, pt.Y);
        }

        /// <summary>
        /// Convert a <see cref="Point"/> to a <see cref="PointD"/>.
        /// </summary>
        /// <param name="pt">The point to be converted.</param>
        /// <returns>The converted point.</returns>
        public static DropPoint AsDropPoint(this Point pt)
        {
            return new DropPoint(pt.X, pt.Y);
        }
    }
}