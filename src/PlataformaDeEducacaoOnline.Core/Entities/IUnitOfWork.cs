namespace PlataformaDeEducacaoOnline.Core.Entities
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }

    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
