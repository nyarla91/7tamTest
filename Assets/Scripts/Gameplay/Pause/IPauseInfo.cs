namespace Gameplay.Pause
{
    public interface IPauseInfo
    {
        public bool IsPaused { get; }
        public bool IsUnpaused { get; }
    }
}