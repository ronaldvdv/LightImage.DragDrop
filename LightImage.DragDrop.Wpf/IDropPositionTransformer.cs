namespace LightImage.DragDrop
{
    /// <summary>
    /// Contract for drop target controls that support transforming the drop coordinates to a local coordinate system.
    /// </summary>
    public interface IDropPositionTransformer
    {
        /// <summary>
        /// Transform the system drop coordinates from <see cref="IDragDropInfo.DropPosition"/> to local coordinates.
        /// </summary>
        /// <param name="dropPosition">The original drop coordinates.</param>
        /// <returns>The transformed coordinates.</returns>
        DropPoint TransformDropPosition(DropPoint dropPosition);
    }
}