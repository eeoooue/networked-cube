namespace FaceViewer.ViewModels;
using System.Windows.Media;
using LibCubeIntegration.GetCubeStrategies;
using LibCubeIntegration.Services;
using LibNetCube;

public class FaceViewModel : BaseViewModel
{
    readonly CubeServiceFacade _cubeService;
    readonly int[,] _values;
    CubeState? _cubeState;
    public CubeFace Face;

    readonly CancellationTokenSource _cts = new();

    public FaceViewModel(CubeFace face, CubeServiceFacade cubeService)
    {
        Face = face;
        _cubeService = cubeService;

        _values = new int[3, 3];

        Task.Run(StateUpdateTicker, _cts.Token);
    }

    public int Row0Col0 => _values[0, 0];
    public int Row0Col1 => _values[0, 1];
    public int Row0Col2 => _values[0, 2];

    public int Row1Col0 => _values[1, 0];
    public int Row1Col1 => _values[1, 1];
    public int Row1Col2 => _values[1, 2];

    public int Row2Col0 => _values[2, 0];
    public int Row2Col1 => _values[2, 1];
    public int Row2Col2 => _values[2, 2];

    public Brush ColourR0C0 => CalcColour(0, 0);
    public Brush ColourR0C1 => CalcColour(0, 1);
    public Brush ColourR0C2 => CalcColour(0, 2);

    public Brush ColourR1C0 => CalcColour(1, 0);
    public Brush ColourR1C1 => CalcColour(1, 1);
    public Brush ColourR1C2 => CalcColour(1, 2);

    public Brush ColourR2C0 => CalcColour(2, 0);
    public Brush ColourR2C1 => CalcColour(2, 1);
    public Brush ColourR2C2 => CalcColour(2, 2);

    public async Task Update()
    {
        _cubeState = await TryGetCubeState();

        for (var i = 0; i < 3; i++)
        for (var j = 0; j < 3; j++)
        {
            _values[i, j] = GetFacePiece(i, j);

            var memberVariableNameA = $"Row{i}Col{j}";
            OnPropertyChanged(memberVariableNameA);

            var memberVariableNameB = $"ColourR{i}C{j}";
            OnPropertyChanged(memberVariableNameB);
        }
    }

    void StateUpdateTicker()
    {
        while (true)
        {
            Thread.Sleep(500);
            _ = Update();
        }
    }

    SolidColorBrush CalcColour(int i, int j)
    {
        var value = _values[i, j];
        return value switch
        {
            1 => Brushes.Gold,
            2 => Brushes.Crimson,
            3 => Brushes.DodgerBlue,
            4 => Brushes.LimeGreen,
            5 => Brushes.Coral,
            0 => Brushes.White,
            _ => Brushes.White
        };
    }

    async Task<CubeState?> TryGetCubeState()
    {
        try
        {
            return await _cubeService.GetStateAsync();
        }
        catch
        {
            return null;
        }
    }

    int GetFacePiece(int i, int j)
    {
        if (_cubeState is not { } state) return 0;

        var values = state.GetFace(Face);
        return values[i, j];
    }
}
