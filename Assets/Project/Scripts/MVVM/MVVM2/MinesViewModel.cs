using Zenject;

namespace Project.Game.Scripts.MVVM
{
    public class MinesViewModel : ViewModel
    {
        [Inject]
        [Model] private Mines _mines;

        [Project] public int CountMines => _mines.CountMines;

        [Command]
        public void Shoot() => _mines.Shoot();
    }
}