namespace ajf.ns_planner.shared2.NsCommands
{
    public interface INsCommandHandler<in TCommand, out TResponse>
        where TCommand : INsCommand
        where TResponse : INsCommandResponse
    {
        TResponse Handle(TCommand command);
    }
}
