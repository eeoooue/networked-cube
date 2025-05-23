using LibCubeIntegration.ResetCubeStrategies;
using LibCubeIntegration.Services;
using LibNetCube;
using Microsoft.AspNetCore.Mvc;

namespace CubeProxy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CubeController : Controller
    {
        readonly CubeServiceFacade _cubeService = new CubeServiceFacade("CubeService");
        readonly IResetCubeStrategy ResetStrategy = new ResetCubeViaAPIStrategy("CubeService");

        [HttpPost("[action]")]
        public async Task<IActionResult> Reset()
        {
            await ResetStrategy.ResetCubeAsync();
            return Ok();
        }

        [HttpPost("[action]")]
        public IActionResult ApplyShuffle([FromQuery] string? shuffle = null)
        {
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> PerformMove([FromQuery] string? move = null)
        {
            if (move is string)
            {
                try
                {
                    //attempt to parse move as Enum
                    CubeMove parsedMove = MoveParser.ParseMove(move)!;
                    await _cubeService.PerformMoveAsync(move);

                    return Ok();
                }
                catch
                {
                    //Not a valid move
                    return BadRequest();
                }
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("")] //follows base route of api/[controller]
        [Route("[action]")]
        public async Task<IActionResult> State()
        {
            CubeState? result = await _cubeService.GetStateAsync();

            if (result is CubeState state)
            {
                Dictionary<string, int[]> faces = CreateDictionaryOfFaces(state);
                return Ok(faces);
            }

            return NotFound();
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> Face([FromQuery] string? face = null)
        {
            CubeState? result = await _cubeService.GetStateAsync();

            try
            {
                if (result is CubeState state)
                {
                    Dictionary<string, int[]> faces = CreateDictionaryOfFaces(state);
                    if (face is string)
                    {
                        return Ok(faces[face]);
                    }
                }
                else
                {
                    return NotFound();
                }

                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }

        private Dictionary<string, int[]> CreateDictionaryOfFaces(CubeState state)
        {
            Dictionary<string, int[]> faces = new Dictionary<string, int[]>();
            foreach (CubeFace face in CubeState.GetFaceNames())
            {
                int[,] ints = state.ReadFace(face);
                int[] formattedInts = ints.Cast<int>().ToArray();
                faces.Add(face.ToString(), formattedInts);
            }

            return faces;
        }
    }
}
