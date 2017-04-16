using System;

namespace ajf.ns_planner.datalayer
{
    public class UnitOfWork : IDisposable
    {
        private readonly NsContext _dataContext;
        private bool _isDisposing;

        public UnitOfWork()
        {
            _isDisposing = false;
            _dataContext = new NsContext();
        }

        public NsContext Db
        {
            get { return _dataContext; }
        }

        public void Dispose()
        {
            if (_dataContext != null && !_isDisposing)
            {
                _isDisposing = true;
                _dataContext.Dispose();
            }
        }
    }
}