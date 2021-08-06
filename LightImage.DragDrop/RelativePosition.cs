namespace LightImage.DragDrop
{
    /// <summary>
    /// Options to describe the mouse drop position relative to the target item.
    /// </summary>
    public enum RelativePosition
    {
        /// <summary>
        /// No relative position.
        /// </summary>
        None = 0,

        /// <summary>
        /// Before the target item.
        /// </summary>
        Before = 1,

        /// <summary>
        /// On top of the targetitem.
        /// </summary>
        On = 2,

        /// <summary>
        /// After the target item.
        /// </summary>
        After = 3,
    }
}