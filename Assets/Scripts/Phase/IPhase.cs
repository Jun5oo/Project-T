public interface IPhase 
{
    public PhaseType PhaseType { get; }
    public void EnterPhase();
    public void UpdatePhase();
    public void ExitPhase(); 
}
