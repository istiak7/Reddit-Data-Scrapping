namespace Reddit_Management_System.Domain.Interfaces.Transactions
{
    public interface ITransactionUtil : IDisposable
    {
        Task BeginAsync();
        Task CommitAsync();
        Task RollBackAsync();
    }
}
