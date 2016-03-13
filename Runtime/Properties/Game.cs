public static class Game
{
    public enum Mode
    {
        FREE
    }

    public enum State
    {
        Paused,
        Running
    }

    public static class Tag
    {
        public static readonly string Civilian = "Civilian";
        public static readonly string Player = "Player";
    }
}