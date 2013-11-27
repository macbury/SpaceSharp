using HyperSpace.Core.Utils;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperSpace.Core.Rendering {
  class Shader : Disposable {
    public static String TAG = "Shader";
    private int pgmID;
    private int vsID;
    private int fsID;

    public Shader(String vertex, String fragment) {
      pgmID = GL.CreateProgram();

      compile(vertex, ShaderType.VertexShader, pgmID, out vsID);
      compile(fragment, ShaderType.FragmentShader, pgmID, out fsID);

      GL.LinkProgram(pgmID);
      Game.logger.info(TAG, "Compiled program {0}", pgmID);
      Game.logger.info(TAG, GL.GetProgramInfoLog(pgmID));
    }
    private void compile(String source, ShaderType type, int program, out int address) {
      address = GL.CreateShader(type);
      GL.ShaderSource(address, source);

      GL.CompileShader(address);
      GL.AttachShader(program, address);
      Game.logger.info(TAG, GL.GetShaderInfoLog(address));

      int compileResult;
      GL.GetShader(address, ShaderParameter.CompileStatus, out compileResult);
      if (compileResult != 1) {
        Game.logger.info(TAG, "Compile Error!");
      }
      Game.logger.info(TAG, "Compile result: " + compileResult);
    }
    public void dispose() {
      Game.logger.info(TAG, "Disposing: " + pgmID);
      GL.DeleteShader(pgmID);
    }
    public void use() {
      GL.UseProgram(pgmID);
    }
  }
}
