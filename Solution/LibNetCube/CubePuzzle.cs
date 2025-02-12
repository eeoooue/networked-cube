namespace LibNetCube
{
    public class CubePuzzle
    {

        public CubeState GetState()
        {
            throw new NotImplementedException();
        }

        private void RotateTopFaceClockwise()
        {

        }

        private void RollForwards()
        {

        }

        private void RollBackwards()
        {
            RollForwards();
            RollForwards();
            RollForwards();
        }

        private void RollClockwise()
        {

        }

        private void RollAntiClockwise()
        {
            RollClockwise();
            RollClockwise();
            RollClockwise();
        }
    }
}
