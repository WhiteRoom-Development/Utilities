using UnityEditor;
using UnityEditor.Compilation;

namespace Utilities.Editor.Hotkeys
{
    public static class CompileProject
    {
        [MenuItem("File/Compile _F3")]
        private static void Compile()
        {
            CompilationPipeline.RequestScriptCompilation(RequestScriptCompilationOptions.CleanBuildCache);
        }
    }
}