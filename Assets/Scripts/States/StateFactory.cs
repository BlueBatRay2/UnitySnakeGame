using Zenject;

namespace States
{ 
    public class StateFactory {
        
        private readonly DiContainer _container;

        protected StateFactory(DiContainer container) {
            this._container = container;
        }

        public T Create<T>() where T : IGameState {
            return _container.Instantiate<T>();
        }
        
        public T CreateWithArgs<T>(params object[] args) where T : IGameState
        {
            return _container.Instantiate<T>(args);
        }
    }
}