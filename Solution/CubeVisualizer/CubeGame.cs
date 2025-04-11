using LibNetCube;
using LibCubeIntegration.GetCubeStrategies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
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
    const float DistanceFromCube = 7.5f;
    const float MouseSpeed = 0.5f;

    MouseState _PrevMouseState = Mouse.GetState();
    float _YRotation = MathHelper.ToRadians(225);
    float _XRotation = MathHelper.ToRadians(45);

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

        float xDelta = 0.0f;
        float yDelta = 0.0f;
        float rotation = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0f;

        MouseState mouseState = Mouse.GetState();
        if (mouseState.LeftButton == ButtonState.Pressed && _PrevMouseState.LeftButton == ButtonState.Pressed)
        {
            xDelta = mouseState.X - _PrevMouseState.X;
            yDelta = _PrevMouseState.Y - mouseState.Y;
        }
        _PrevMouseState = mouseState;

        _YRotation += xDelta * rotation * MouseSpeed;
        _XRotation += yDelta * rotation * MouseSpeed;
        _XRotation = MathHelper.Clamp(_XRotation, MathHelper.ToRadians(20), MathHelper.ToRadians(160));

        var cameraPos = new Vector3
        {
            X = DistanceFromCube * (float)(Math.Sin(_XRotation) * Math.Cos(_YRotation)),
            Y = DistanceFromCube * (float)Math.Cos(_XRotation),
            Z = DistanceFromCube * (float)(Math.Sin(_XRotation) * Math.Sin(_YRotation))
        };

        cameraPos += new Vector3(1, 1, 1); // As center of cube is (1,1,1)
        _Camera.Position = cameraPos;

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
