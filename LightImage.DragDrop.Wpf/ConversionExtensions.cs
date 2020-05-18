using System.Windows;

namespace LightImage.Util.Geometry
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
        public static Point AsPoint(this PointD pt)
        {
            return new Point(pt.X, pt.Y);
        }

        /// <summary>
        /// Convert a <see cref="Point"/> to a <see cref="PointD"/>.
        /// </summary>
        /// <param name="pt">The point to be converted.</param>
        /// <returns>The converted point.</returns>
        public static PointD AsPointD(this Point pt)
        {
            return new PointD(pt.X, pt.Y);
        }

        /// <summary>
        /// Convert a <see cref="RectD"/> to a <see cref="Rect"/>.
        /// </summary>
        /// <param name="rect">The rectangle to be converted.</param>
        /// <returns>The converted rectangle.</returns>
        public static Rect AsRect(this RectD rect)
        {
            return new Rect(rect.Left, rect.Top, rect.Width, rect.Height);
        }

        /// <summary>
        /// Convert a <see cref="Rect"/> to a <see cref="RectD"/>.
        /// </summary>
        /// <param name="rect">The rectangle to be converted.</param>
        /// <returns>The converted rectangle.</returns>
        public static RectD AsRectD(this Rect rect)
        {
            return new RectD(rect.Left, rect.Top, rect.Width, rect.Height);
        }

        /// <summary>
        /// Convert a <see cref="SizeD"/> to a <see cref="Size"/>.
        /// </summary>
        /// <param name="size">The size to be converted.</param>
        /// <returns>The converted size.</returns>
        public static Size AsSize(this SizeD size)
        {
            return new Size(size.Width, size.Height);
        }

        /// <summary>
        /// Convert a <see cref="Size"/> to a <see cref="SizeD"/>.
        /// </summary>
        /// <param name="size">The size to be converted.</param>
        /// <returns>The converted size.</returns>
        public static SizeD AsSizeD(this Size size)
        {
            return new SizeD(size.Width, size.Height);
        }
    }
}