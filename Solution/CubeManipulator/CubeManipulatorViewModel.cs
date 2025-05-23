using LibCubeIntegration.PerformMoveStrategies;

namespace CubeManipulator
{
    public class CubeManipulatorViewModel
    {
        //full path MoveViaApi
        LibCubeIntegration.PerformMoveStrategies.MoveViaApiStrategy moveStrategy = new MoveViaApiStrategy("CubeProxy");

        private RelayCommand moveCommand;
        public RelayCommand MoveCommand //Get command result
        {
            get
            {
                return moveCommand ??
                    (moveCommand = new RelayCommand(obj =>
                    {
                        string parsedObj = obj is string ? (string)obj : string.Empty;

                        Task<bool> task = moveStrategy.PerformMoveAsync(parsedObj);
                    }));
            }
        }

    }
}
