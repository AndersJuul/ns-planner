namespace ajf.ns_planner.shared2.NsCommands
{
    public interface INsQueryHandler<in TQuery, out TResponse>
        where TQuery : INsQuery
        where TResponse : INsQueryResponse
    {
        TResponse Query(TQuery command);
    }
}