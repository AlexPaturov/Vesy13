namespace Vesy13.Protocol;

public sealed class SimA04DynamicFrame
{
    public byte? B0 { get; private set; }
    public byte? B1 { get; private set; }
    public byte? B2 { get; private set; }
    public byte? B3 { get; private set; }
    public byte? Aux { get; private set; }

    public int? Ch0 { get; private set; }
    public int? Ch1 { get; private set; }

    public int ReceivedByteCount { get; private set; }

    public FrameState State { get; private set; } = FrameState.Created;

    public bool IsValid => State == FrameState.Valid;

    private SimA04DynamicFrame()
    {
    }

    public static SimA04DynamicFrame Parse(byte[]? data)
    {
        const int frameSize = 5;

        var frame = new SimA04DynamicFrame();

        if (data is null)
        {
            frame.TransitionTo(FrameState.NullInput);
            return frame;
        }

        frame.ReceivedByteCount = data.Length;

        if (data.Length == 0)
        {
            frame.TransitionTo(FrameState.Empty);
            return frame;
        }

        if (data.Length > frameSize)
        {
            frame.TransitionTo(FrameState.InvalidLength);
            return frame;
        }

        if (data.Length < frameSize)
        {
            frame.LoadIncomplete(data);
            return frame;
        }

        frame.LoadCandidate(data);
        frame.ValidateCandidate();

        return frame;
    }

    private void LoadIncomplete(byte[] data)
    {
        B0 = data[0];

        if (data.Length == 1)
        {
            TransitionTo(FrameState.Incomplete);
            return;
        }

        B1 = data[1];
        Ch0 = B1.Value * 256 + B0.Value;

        if (data.Length == 2)
        {
            TransitionTo(FrameState.Incomplete);
            return;
        }

        B2 = data[2];

        if (data.Length == 3)
        {
            TransitionTo(FrameState.Incomplete);
            return;
        }

        B3 = data[3];
        Ch1 = B3.Value * 256 + B2.Value;

        TransitionTo(FrameState.Incomplete);
    }

    private void LoadCandidate(byte[] data)
    {
        B0 = data[0];
        B1 = data[1];
        B2 = data[2];
        B3 = data[3];
        Aux = data[4];

        Ch0 = B1.Value * 256 + B0.Value;
        Ch1 = B3.Value * 256 + B2.Value;

        TransitionTo(FrameState.CandidateReady);
    }

    private void ValidateCandidate()
    {
        var checksum =
            (B0!.Value +
             B1!.Value +
             B2!.Value +
             B3!.Value) & 0xFF;

        if (Aux!.Value != checksum)
        {
            TransitionTo(FrameState.InvalidChecksum);
            return;
        }

        TransitionTo(FrameState.Valid);
    }

    private void TransitionTo(FrameState nextState)
    {
        if (!CanTransitionTo(nextState))
        {
            throw new InvalidOperationException(
                $"Invalid frame state transition: {State} -> {nextState}");
        }

        State = nextState;
    }

    private bool CanTransitionTo(FrameState nextState)
    {
        if (State == FrameState.Created)
        {
            return nextState == FrameState.NullInput
                || nextState == FrameState.Empty
                || nextState == FrameState.Incomplete
                || nextState == FrameState.InvalidLength
                || nextState == FrameState.CandidateReady;
        }

        if (State == FrameState.CandidateReady)
        {
            return nextState == FrameState.InvalidChecksum
                || nextState == FrameState.Valid;
        }

        return false;
    }
}

public enum FrameState
{
    Created,
    NullInput,
    Empty,
    Incomplete,
    InvalidLength,
    CandidateReady,
    InvalidChecksum,
    Valid
}
