using HyperSpace.Core.Utils;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperSpace.Core.Rendering {
  class Shader : Disposable {
    public static String TAG = "Shader";

    #region Settings
    private int pgmID;
    private int vsID;
    private int fsID;
    private Dictionary<String, int> attributesMapping;
    private Dictionary<String, int> uniformsMapping;
    #endregion

    public Shader(String vertex, String fragment) {
      attributesMapping = new Dictionary<string, int>();
      uniformsMapping   = new Dictionary<string, int>();

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

    #region Shader Uniform/Attribute location
    public int attribute(String name) {
      if (!attributesMapping.ContainsKey(name)) {
        int location = GL.GetAttribLocation(pgmID, name);
        if (location == -1) {
          Game.logger.info(TAG, "Attribute " + name + " not found in shader " + pgmID);
        }
        attributesMapping.Add(name, location);
      }
      return attributesMapping[name];
    }
    public int uniform(String name) {
      if (!uniformsMapping.ContainsKey(name)) {
        int location = GL.GetUniformLocation(pgmID, name);
        if (location == -1) {
          Game.logger.info(TAG, "Uniform " + name + " not found in shader " + pgmID);
        }
        uniformsMapping.Add(name, location);
      }
      return uniformsMapping[name];
    }
    #endregion
    #region Assign uniforms
    public void uniformMatrix4(String name, bool transpond, ref Matrix4 mat) {
      GL.UniformMatrix4(uniform(name), transpond, ref mat);
    }
    #endregion
  }
}
