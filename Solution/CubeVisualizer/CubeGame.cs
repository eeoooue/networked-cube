using LibNetCube;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CubeVisualizer;

public class CubeGame : Game
{
    private GraphicsDeviceManager _graphics;
    private Camera _Camera;
    private RubiksCube _RubiksCube;
    private CubeState _CubeState;

    private readonly Color _BackgroundColour;

    private float _DistanceFromCube;

    private float _YRotation;
    private float _XRotation;
    private float _XRotationFactor; // Factor to control the speed of rotation, also used to keep rotation within 30 and 150 degrees

    public CubeGame(string pWindowName, int pWindowHeight, int pWindowWidth, Color pBGColour, CubeState pCubeState)
    {
        Content.RootDirectory = "Content";
        Window.Title = pWindowName;
        IsMouseVisible = true;
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferHeight = pWindowHeight;
        _graphics.PreferredBackBufferWidth = pWindowWidth;
        _BackgroundColour = pBGColour;
        _CubeState = pCubeState;
    }

    protected override void Initialize()
    {
        _Camera = new Camera(new Vector3(1, 1, 1), new Vector3(1, 1, 1), GraphicsDevice.Viewport.AspectRatio, MathHelper.PiOver4);
        _DistanceFromCube = 7.5f;
        _YRotation = 0.0f;
        _XRotation = MathHelper.ToRadians(30);
        _XRotationFactor = 0.25f;
        base.Initialize();
    }

    protected override void LoadContent()
    {
        Model cube = Content.Load<Model>("3D Objects/Cube");
        _RubiksCube = new RubiksCube(cube, _CubeState);
    }

    protected override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _YRotation += 1.00f * (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0f;
        _XRotation += _XRotationFactor * (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0f;

        Vector3 cameraPos = _Camera.Position;
        cameraPos.X = _DistanceFromCube * (float)System.Math.Sin(_YRotation);
        cameraPos.X += 1; // As centre of cube is (1,1,1)
        cameraPos.Z = _DistanceFromCube * (float)System.Math.Cos(_YRotation) * (float)System.Math.Sin(_XRotation);
        cameraPos.Z += 1;
        cameraPos.Y = _DistanceFromCube * (float)System.Math.Cos(_XRotation);
        cameraPos.Y += 1;
        _Camera.Position = cameraPos;

        if (_XRotation > MathHelper.ToRadians(150) || _XRotation < MathHelper.ToRadians(30))
        {
            _XRotationFactor *= -1;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(_BackgroundColour);

        _RubiksCube.Draw(_Camera);

        base.Draw(gameTime);
    }
}
