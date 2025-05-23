namespace FaceViewer.ViewModels;
using System.Windows.Media;
using LibCubeIntegration;
using LibCubeIntegration.Services;
using LibNetCube;
using Microsoft.AspNetCore.SignalR.Client;

public class FaceViewModel : BaseViewModel
{
    int[,] _values;
    CubeState? _cubeState;
    public CubeFace Face;

    HubConnection _connection;

    public FaceViewModel(CubeFace face, CubeServiceFacade cubeService)
    {
        Face = face;
        _values = new int[3, 3];

        _connection = CubePublisherService.CreateHubConnection();

        _connection.On<JsonFriendlyCubeState>("CubeStateUpdated", dto =>
        {
            _cubeState = dto.ToCubeState();
            Update();
        });

        _ = StartConnectionAsync();
    }

    private async Task StartConnectionAsync()
    {
        bool connected = false;
        while (!connected)
        {
            try
            {
                await _connection.StartAsync();
                connected = true;
            }
            catch
            {
                await Task.Delay(1000);
            }
        }
    }


    public Brush ColourR0C0 => CalcColour(0, 0);
    public Brush ColourR0C1 => CalcColour(0, 1);
    public Brush ColourR0C2 => CalcColour(0, 2);

    public Brush ColourR1C0 => CalcColour(1, 0);
    public Brush ColourR1C1 => CalcColour(1, 1);
    public Brush ColourR1C2 => CalcColour(1, 2);

    public Brush ColourR2C0 => CalcColour(2, 0);
    public Brush ColourR2C1 => CalcColour(2, 1);
    public Brush ColourR2C2 => CalcColour(2, 2);

    public void Update()
    {
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

    int GetFacePiece(int i, int j)
    {
        if (_cubeState is not { } state) return 0;

        var values = state.GetFace(Face);
        return values[i, j];
    }
}
