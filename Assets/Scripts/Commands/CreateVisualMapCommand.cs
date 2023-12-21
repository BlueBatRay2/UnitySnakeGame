using Map;

namespace Commands
{
    public class CreateVisualMapCommand : ICommand
    {
        private readonly VisualMapManager _mapManager;
        private readonly int _width;
        private readonly int _height;

        public CreateVisualMapCommand(VisualMapManager mapManager, int width, int height)
        {
            _mapManager = mapManager;
            _width = width;
            _height = height;
        }

        public void Execute()
        {
            _mapManager.CreateVisualMap(_width, _height);
        }
    }
}