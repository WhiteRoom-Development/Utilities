namespace Utilities.Runtime
{
    /// <summary>
    /// Provides constants representing layer indices for commonly used layers in Unity.
    /// </summary>
    public static partial class LayerConstants
    {
        // Layer indices
        public const int DEFAULT = 0;
        public const int TRANSPARENT_FX = 1;
        public const int IGNORE_RAYCAST = 2;
        public const int WATER = 4;
        public const int UI = 5;


        // Layer masks
        public const int SIMPLE_SOLID_OBJECTS_MASK = 1 << DEFAULT;
    }

    /// <summary>
    /// Provides constants representing commonly used tags in Unity.
    /// </summary>
    public static partial class TagConstants
    {
        // Tag names
        public const string MAIN_CAMERA = "MainCamera";
        public const string PLAYER = "Player";
        public const string GAME_CONTROLLER = "GameController";
    }
    
    /// <summary>
    /// Provides constants representing execution order values for script execution in Unity.
    /// </summary>
    public static partial class ExecutionOrderConstants
    {
        // Execution order values
        public const int SCRIPTABLE_SINGLETON = -100000;
        public const int SCENE_SINGLETON = -10000;
        public const int BEFORE_DEFAULT_3 = -1000;
        public const int BEFORE_DEFAULT_2 = -100;
        public const int BEFORE_DEFAULT_1 = -10;
        public const int AFTER_DEFAULT_1 = 10;
        public const int AFTER_DEFAULT_2 = 100;
    }
}