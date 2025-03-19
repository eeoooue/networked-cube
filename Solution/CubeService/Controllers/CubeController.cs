using CubeService.Models;
using LibNetCube;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CubeService.Controllers
{
    public class CubeController : BaseController
    {
        readonly CubePuzzle _cubePuzzle;
        public CubeController(SharedError error, CubePuzzle cubePuzzle) : base(error) 
        { 
            _cubePuzzle = cubePuzzle; 
        }

        [HttpPost]
        public IActionResult Reset()
        {
            return Ok();
        }
        [HttpPost]
        public IActionResult ApplyShuffle([FromQuery] string? shuffle = null)
        {
            return Ok();
        }
        [HttpPost]
        public IActionResult PerformMove([FromQuery] string? move = null)
        {
            //attempt to parse move as Enum
            if (String.IsNullOrEmpty(move) || !Enum.TryParse<CubeMove>(move, out var parsedMove))
            {
                //Not a valid move
                Error.StatusCode = 400;
                Error.Message = "Bad request";
                return new EmptyResult();
            }
            //Is a valid move
            _cubePuzzle.PerformMove(move);
            return Ok();
        }
        [HttpGet]
        public IActionResult State()
        {
            CubeState state = _cubePuzzle.GetState();
            Dictionary<string, int[]> faces = new Dictionary<string, int[]>();
            foreach (CubeFace face in CubeState.GetFaceNames())
            {
                int[,] ints = _cubePuzzle.ReadFace(face);
                int[] formattedInts = ints.Cast<int>().ToArray();
                faces.Add(face.ToString(), formattedInts);
            }
            return Ok(faces);
        }
        [HttpGet]
        public IActionResult Face([FromQuery] string? face = null)
        {
            //attempt to parse face as Enum
            if (String.IsNullOrEmpty(face))
            {
                //Not a valid face
                Error.StatusCode = 400;
                Error.Message = "Bad request";
                return new EmptyResult();
            }
            //Face value exists
            face = Helper.FirstCharToUpper(face);
            if (!Enum.TryParse<CubeFace>(face, out var parsedFace))
            {
                //Not a valid face
                Error.StatusCode = 400;
                Error.Message = "Bad request";
                return new EmptyResult();
            }
            //Is a valid face
            CubeState state = _cubePuzzle.GetState();
            int[,] ints = _cubePuzzle.ReadFace(parsedFace);
            int[] formattedInts = ints.Cast<int>().ToArray();
            return Ok(formattedInts);
        }
    }
}
