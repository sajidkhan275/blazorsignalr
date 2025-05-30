namespace BlazorApp1.Data
{
    public class ServiceLifetime
    {
    }

    public class TaskService : ISingletonService, IScopedService, ITransientService
    {
        Guid id;
        public TaskService()
        {
            id = Guid.NewGuid();
        }

        public Guid GetOperationId()
        {
            return id;
        }
    }
    public interface ISingletonService
    {
        Guid GetOperationId();
    }
 
    public interface IScopedService
    {
        Guid GetOperationId();
    }

    public interface ITransientService
    {
        Guid GetOperationId();
    }

    //public class SingletonService : ISingletonService
    //{
    //    private readonly Guid _operationId;
    //
    //    public SingletonService()
    //    {
    //        _operationId = Guid.NewGuid();
    //    }
    //
    //    public Guid GetOperationId() => _operationId;
    //}
    //
    //public class ScopedService : IScopedService
    //{
    //    private readonly Guid _operationId;
    //
    //    public ScopedService()
    //    {
    //        _operationId = Guid.NewGuid();
    //    }
    //
    //    public Guid GetOperationId() => _operationId;
    //}
    //
    //public class TransientService : ITransientService
    //{
    //    private readonly Guid _operationId;
    //
    //    public TransientService()
    //    {
    //        _operationId = Guid.NewGuid();
    //    }
    //
    //    public Guid GetOperationId() => _operationId;
    //}
}
