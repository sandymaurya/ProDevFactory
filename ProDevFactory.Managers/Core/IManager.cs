namespace ProDevFactory.Managers.Core
{
    public interface IManager<TEntity> : ISynchronousManager<TEntity>, IAsynchronousManager<TEntity>
    {
    }
}
