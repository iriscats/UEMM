

namespace UEMM.Core.Mods
{
    /// <summary>
    /// Represents instance of the modification.
    /// </summary>
    public interface IMod
    {
        public int Id { get; }

        public int Priority { get; }

        public bool IsValid { get; }

        public bool Enabled { get; }

        public bool Installed { get; }

        public string Name { get; }

        public string Author { get; }

        public string Website { get; }

        public string ArchiveName { get; }

        public string SourcePath { get; }

        public string SourceBackupPath { get; }

        public string TempPath { get; }

        public Version Version { get; }

        public Core.Mods.Location Location { get; }

        public Core.Mods.Category Category { get; }

        public DateTime DateInstalled { get; }

        public DateTime DateModified { get; }

        public IEnumerable<string> Files { get; }

        public IEnumerable<string> FilesOverridden { get; }
    }
}
