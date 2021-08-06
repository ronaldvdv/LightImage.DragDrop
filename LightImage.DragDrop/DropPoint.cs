namespace LightImage.DragDrop
{
    /// <summary>
    /// Point describing drag or drop location.
    /// </summary>
    public readonly struct DropPoint
    {
        /// <summary>
        /// The X coordinate.
        /// </summary>
        public readonly double X;

        /// <summary>
        /// The Y coordinate.
        /// </summary>
        public readonly double Y;

        /// <summary>
        /// Initializes a new instance of the <see cref="DropPoint"/> struct.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        public DropPoint(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}
