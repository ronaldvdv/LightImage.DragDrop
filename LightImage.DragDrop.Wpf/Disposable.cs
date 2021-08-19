using System;

namespace LightImage.DragDrop
{
    /// <summary>
    /// Simple <see cref="IDisposable"/> implementation.
    /// </summary>
    internal class Disposable : IDisposable
    {
        private readonly Action _action;
        private bool _disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="Disposable"/> class.
        /// </summary>
        /// <param name="action">The action to be executed when disposing.</param>
        public Disposable(Action action)
        {
            _action = action;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            if (_disposed)
            {
                throw new InvalidOperationException($"This {nameof(IDisposable)} has already been disposed.");
            }

            _action();
            _disposed = true;
        }
    }
}