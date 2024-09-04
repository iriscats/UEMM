

namespace UEMM.Core.Game
{
    /// <summary>
    /// Represents information about the game on the computer.
    /// </summary>
    public interface IGame
    {
        /// <summary>
        /// Name of the instance.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Developer name.
        /// </summary>
        public string Company { get; }

        /// <summary>
        /// Game copyright..
        /// </summary>
        public string Copyright { get; }

        /// <summary>
        /// Current version.
        /// </summary>
        public string Version { get; }

        /// <summary>
        /// Current product version.
        /// </summary>
        public string ProductVersion { get; }

        /// <summary>
        /// The address of the game's executable file.
        /// </summary>
        public string ExecutablePath { get; }

        /// <summary>
        /// Base archive location.
        /// </summary>
        public string BasePath { get; }
    }
}
