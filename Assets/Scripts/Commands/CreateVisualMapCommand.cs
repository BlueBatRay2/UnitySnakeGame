using Map;

namespace Commands
{
    public class CreateVisualMapCommand : ICommand
    {
        private readonly VisualMapGenerator _mapGenerator;
        private int _width;
        private int _height;

        public CreateVisualMapCommand(VisualMapGenerator mapGenerator, int width, int height)
        {
            _mapGenerator = mapGenerator;
            _width = width;
            _height = height;
        }

        public void Execute()
        {
            _mapGenerator.CreateVisualMap(_width, _height);
        }
    }
}