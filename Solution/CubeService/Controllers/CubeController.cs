using CubeService.Models;
using LibNetCube;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CubeService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CubeController : Controller
    {
        private CubeGameEngine _cubeEngine;

        public CubeController(CubeGameEngine cubeEngine) : base() 
        { 
            _cubeEngine = cubeEngine;
        }

        [HttpPost("[action]")]
        public IActionResult Reset()
        {
            _cubeEngine.Reset();

            return Ok();
        }


        [HttpPost("[action]")]
        public IActionResult ApplyShuffle([FromQuery] string? shuffle = null)
        {
            List<CubeMove> moves = new List<CubeMove>();

            if (shuffle is string moveString && moveString.Length > 0)
            {
                try
                {
                    moves = MoveParser.ParseMoveSequence(moveString);
                }
                catch
                {
                    return BadRequest();
                }
            }

            if (moves.Count == 0)
            {
                moves = ScrambleAlgorithm.GenerateScramble();
            }

            _cubeEngine.Reset();
            _cubeEngine.PerformMoveSeries(moves);

            return Ok();
        }


        [HttpPost("[action]")]
        public IActionResult PerformMove([FromQuery] string? move = null)
        {
            if (move is string)
            {
                try
                {
                    //attempt to parse move as Enum
                    CubeMove parsedMove = MoveParser.ParseMove(move)!;
                    _cubeEngine.PerformMove(parsedMove);
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
        public IActionResult State()
        {
            CubeState state = _cubeEngine.GetCubeState();
            Dictionary<string, int[]> faces = new Dictionary<string, int[]>();
            foreach (CubeFace face in CubeState.GetFaceNames())
            {
                int[,] ints = _cubeEngine.GetCubeFace(face);
                int[] formattedInts = ints.Cast<int>().ToArray();
                faces.Add(face.ToString(), formattedInts);
            }
            return Ok(faces);
        }


        [HttpGet("[action]")]
        public IActionResult Face([FromQuery] string? face = null)
        {
            //attempt to parse face as Enum
            if (String.IsNullOrEmpty(face))
            {
                //Not a valid face
                return BadRequest();
            }
            //Face value exists
            face = Helper.FirstCharToUpper(face);
            if (!Enum.TryParse<CubeFace>(face, out var parsedFace))
            {
                //Not a valid face
                return BadRequest();
            }

            //Is a valid face
            int[,] ints = _cubeEngine.GetCubeFace(parsedFace);
            int[] formattedInts = ints.Cast<int>().ToArray();
            return Ok(formattedInts);
        }
    }
}
