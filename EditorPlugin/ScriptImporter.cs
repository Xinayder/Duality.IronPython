using System.IO;

using Duality;
using Duality.Editor;
using Duality.Resources;
using Duality.Editor.AssetManagement;

using RockyTV.Duality.Plugins.IronPython.Resources;
using System;
using System.Linq;

namespace RockyTV.Duality.Editor.Plugins.IronPython
{
    public class ScriptImporter : IAssetImporter
    {
        public static readonly string SourceFileExtPrimary = ".py";
        private static readonly string[] SourceFileExts = new[] { SourceFileExtPrimary };

        public string Id
        {
            get { return "PythonScriptImporter"; }
        }

        public string Name
        {
            get { return "Python Script Importer"; }
        }

        public int Priority
        {
            get { return 0; }
        }

        public void Export(IAssetExportEnvironment env)
        {
            PythonScript input = env.Input as PythonScript;
            string outputPath = env.AddOutputPath(input.Name + SourceFileExtPrimary);

            SaveScriptData(input.Content, outputPath);
        }

        public void PrepareExport(IAssetExportEnvironment env)
        {
            if (env.Input is PythonScript)
                env.AddOutputPath(env.Input.Name + SourceFileExtPrimary);
        }

        private void SaveScriptData(string data, string filePath)
        {
            File.WriteAllText(filePath, data);
        }

        public void Import(IAssetImportEnvironment env)
        {
            foreach (AssetImportInput input in env.Input)
            {
                ContentRef<PythonScript> targetRef = env.GetOutput<PythonScript>(input.AssetName);

                if (targetRef.IsAvailable)
                {
                    PythonScript target = targetRef.Res;

                    string fileData = File.ReadAllText(input.Path);
                    target.UpdateContent(fileData);

                    env.AddOutput(targetRef, input.Path);
                }
            }
        }

        public void PrepareImport(IAssetImportEnvironment env)
        {
            foreach (AssetImportInput input in env.HandleAllInput(AcceptsInput))
            {
                env.AddOutput<PythonScript>(input.AssetName, input.Path);
            }
        }

        private bool AcceptsInput(AssetImportInput input)
        {
            string inputFileExt = Path.GetExtension(input.Path);
            bool matchingFileExt = SourceFileExts.Any(acceptedExt => string.Equals(inputFileExt, acceptedExt, StringComparison.InvariantCultureIgnoreCase));
            return matchingFileExt;
        }
    }
}
