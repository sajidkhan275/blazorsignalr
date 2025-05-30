namespace BlazorApp1.Data
{
    public class CustomMiddle
    {
        private readonly ISingletonService _singletonService1;
        private readonly ISingletonService _singletonService2;
        private readonly IScopedService _scopedService1;
        private readonly IScopedService _scopedService2;
        private readonly ITransientService _transientService1;
        private readonly ITransientService _transientService2;
        
        public CustomMiddle(ISingletonService singletonService1,
            ISingletonService singletonService2,
            IScopedService scopedService1, 
            IScopedService scopedService2, 
            ITransientService  transientService1,
            ITransientService  transientService2)
        {
            _singletonService1 = singletonService1;
            _singletonService2 = singletonService2;
            _scopedService1 = scopedService1;
            _scopedService2 = scopedService2;
            _transientService1 = transientService1;
            _transientService2 = transientService2;
        }
        
        public void OnGet()
        {
            SingletonId1 = _singletonService1.GetOperationId();
            SingletonId2 = _singletonService2.GetOperationId();
            ScopedId1 = _scopedService1.GetOperationId();
            ScopedId2 = _scopedService2.GetOperationId();
            TransientId1 = _transientService1.GetOperationId();
            TransientId2 = _transientService2.GetOperationId();
        }

        public Guid SingletonId1 { get; set; }
        public Guid SingletonId2 { get; set; }
        public Guid ScopedId1 { get; set; }
        public Guid ScopedId2 { get; set; }
        public Guid TransientId1 { get; set; }
        public Guid TransientId2 { get; set; }
    }
}
