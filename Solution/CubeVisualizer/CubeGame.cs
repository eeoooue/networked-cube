using LibNetCube;
using LibCubeIntegration.GetCubeStrategies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;
using System.Threading.Tasks;

namespace CubeVisualizer;
using LibCubeIntegration.PerformMoveStrategies;
using LibCubeIntegration.Services;

public class CubeGame : Game
{
    readonly CubeServiceFacade _cubeService =
        new(new GetCubeViaApiStrategy(), new MoveViaApiStrategy());

    Task _UpdateTask;
    bool _IsRunning = true;
    CubeState _ErrorState;

    GraphicsDeviceManager _graphics;
    Camera _Camera;
    RubiksCube _RubiksCube;

    readonly Color _BackgroundColour;
    float _DistanceFromCube;

    float _YRotation;
    float _XRotation;

    float
        _XRotationFactor; // Factor to control the speed of rotation, also used to keep rotation within 30 and 150 degrees

    public CubeGame(string pWindowName, int pWindowHeight, int pWindowWidth, Color pBGColour)
    {
        Content.RootDirectory = "Content";
        Window.Title = pWindowName;
        IsMouseVisible = true;
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferHeight = pWindowHeight;
        _graphics.PreferredBackBufferWidth = pWindowWidth;
        _BackgroundColour = pBGColour;
    }

    protected override void Initialize()
    {
        _Camera = new Camera(new Vector3(1, 1, 1), new Vector3(1, 1, 1), GraphicsDevice.Viewport.AspectRatio,
            MathHelper.PiOver4);
        _DistanceFromCube = 7.5f;
        _YRotation = 0.0f;
        _XRotation = MathHelper.ToRadians(30);
        _XRotationFactor = 0.25f;
        _ErrorState = new CubeState(new byte[6 * 9]);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        var cube = Content.Load<Model>("3D Objects/Cube");


        _RubiksCube = new RubiksCube(cube, _ErrorState);
        _UpdateTask = Task.Run(CubeStateThread);
    }

    protected async void CubeStateThread()
    {
        while (_IsRunning)
        {
            try
            {
                if (await _cubeService.GetStateAsync() is { } state)
                    _RubiksCube.SetCubeState(state);
                else
                    _RubiksCube.SetCubeState(_ErrorState);
            }

            catch
            {
                _RubiksCube.SetCubeState(_ErrorState);
            }

            Thread.Sleep(250);
        }
    }

    protected override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _YRotation += 1.00f * (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0f;
        _XRotation += _XRotationFactor * (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0f;

        var cameraPos = _Camera.Position;
        cameraPos.X = _DistanceFromCube * (float)System.Math.Sin(_YRotation);
        cameraPos.X += 1; // As centre of cube is (1,1,1)
        cameraPos.Z = _DistanceFromCube * (float)System.Math.Cos(_YRotation) * (float)System.Math.Sin(_XRotation);
        cameraPos.Z += 1;
        cameraPos.Y = _DistanceFromCube * (float)System.Math.Cos(_XRotation);
        cameraPos.Y += 1;
        _Camera.Position = cameraPos;

        if (_XRotation > MathHelper.ToRadians(150) || _XRotation < MathHelper.ToRadians(30)) _XRotationFactor *= -1;

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(_BackgroundColour);

        _RubiksCube.Draw(_Camera);

        base.Draw(gameTime);
    }

    protected override void EndRun()
    {
        _IsRunning = false;
        _UpdateTask.Wait();
        base.EndRun();
    }
}
