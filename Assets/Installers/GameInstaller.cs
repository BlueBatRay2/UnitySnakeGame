using Map.MapGeneration;
using Map.MapGeneration.EntityPlacement;
using States;
using States.GameOver;
using States.MainMenu;
using States.Pause;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        public GameObject mainMenuDialog;
        public GameObject pauseMenuDialog;
        public GameObject gameOverDialog;
        
        public override void InstallBindings()
        {
            Container.Bind<IDataMap>()
                .To<DataMap>()
                .AsSingle();
            
            Container.Bind<IEntityPlacerFactory>()
                .To<EntityPlacerFactory>()
                .AsSingle();
            
            Container.Bind<IEntityLayoutStrategyFactory>()
                .To<RandomEntityLayoutStrategyFactory>()
                .AsSingle();
            
            Container.Bind<IMainMenuService>()
                .To<MainMenuService>()
                .AsSingle()
                .WithArguments(mainMenuDialog);
            
            Container.Bind<IPauseService>()
                .To<PauseService>()
                .AsSingle()
                .WithArguments(pauseMenuDialog);
            
            Container.Bind<IGameOverService>()
                .To<GameOverService>()
                .AsSingle()
                .WithArguments(gameOverDialog);
            
            Container.Bind<StateFactory>().AsSingle();
        }
    }
}
