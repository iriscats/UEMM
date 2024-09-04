

namespace UEMM.Core.Installer
{
    /// <summary>
    /// Represents the result of the unpacking operation.
    /// </summary>
    public struct ExtractingResult
    {
        /// <summary>
        /// Represents the status of the unpacking operation.
        /// </summary>
        public enum ExtractingStatus
        {
            Unknown,
            Success,
            Failure,
            FileNotExist,
            Unsupported,
            PasswordProtected
        }

        public ExtractingStatus Status = ExtractingStatus.Unknown;

        public string SourceHash = String.Empty;

        public string InPath = String.Empty;

        public string OutPath = String.Empty;

        public ExtractingResult()
        {
        }
    }
}
