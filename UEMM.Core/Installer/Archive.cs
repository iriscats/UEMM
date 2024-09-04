

using UEMM.Core.Common;
using SevenZip;
using System.IO;

namespace UEMM.Core.Installer
{
    /// <summary>
    /// Provides functions for unpacking the archives with the help of LZMA and 7Z.
    /// </summary>
    internal static class Archive
    {
        public static async Task<ExtractingResult> ExtractAsync(string input, string output, string sourceHash = "")
        {
            if (!File.Exists(input))
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine(
                    $"WARNING | {input} does not exist, Thread: {System.Threading.Thread.CurrentThread.ManagedThreadId}",
                    "UEMM.Core");
#endif
                return new ExtractingResult
                {
                    InPath = input,
                    OutPath = output,
                    SourceHash = sourceHash,
                    Status = ExtractingResult.ExtractingStatus.FileNotExist
                };
            }


            if (String.IsNullOrEmpty(sourceHash))
                sourceHash = await IOExtensions.ComputeHashAsync(input);

            using var extractor = new SevenZipExtractor(input);

            if (!extractor.Check())
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine(
                    $"WARNING | {input} is password protected (or another error has occurred), Thread: {System.Threading.Thread.CurrentThread.ManagedThreadId}",
                    "UEMM.Core");
#endif
                return new ExtractingResult
                {
                    InPath = input,
                    OutPath = output,
                    SourceHash = sourceHash,
                    Status = ExtractingResult.ExtractingStatus.PasswordProtected
                };
            }

            if (
                !(extractor.Format == InArchiveFormat.Tar ||
                  extractor.Format == InArchiveFormat.Rar ||
                  extractor.Format == InArchiveFormat.Zip ||
                  extractor.Format == InArchiveFormat.SevenZip)
            )
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine(
                    $"WARNING | {input} archive is unsupported, Thread: {System.Threading.Thread.CurrentThread.ManagedThreadId}",
                    "UEMM.Core");
#endif
                return new ExtractingResult
                {
                    InPath = input,
                    OutPath = output,
                    SourceHash = sourceHash,
                    Status = ExtractingResult.ExtractingStatus.Unsupported
                };
            }

            try
            {
                await extractor.ExtractArchiveAsync(output);

#if DEBUG
                System.Diagnostics.Debug.WriteLine(
                    $"INFO | Archive extracted to {output}, Thread: {System.Threading.Thread.CurrentThread.ManagedThreadId}",
                    "UEMM.Core");
#endif

                return new ExtractingResult
                {
                    InPath = input,
                    OutPath = output,
                    SourceHash = sourceHash,
                    Status = ExtractingResult.ExtractingStatus.Success
                };
            }
            catch (Exception e)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine(
                    $"INFO | Extracting {input} failed | {e}, Thread: {System.Threading.Thread.CurrentThread.ManagedThreadId}",
                    "UEMM.Core");
#endif

                return new ExtractingResult
                {
                    InPath = input,
                    OutPath = output,
                    SourceHash = sourceHash,
                    Status = ExtractingResult.ExtractingStatus.Failure
                };
            }
        }
    }
}