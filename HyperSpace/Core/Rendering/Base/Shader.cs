﻿using HyperSpace.Core.Utils;
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
    #region DefaultUniforms
    /** default name for projection matrix uniform **/
    public static String PROJECTION_UNIFORM    = "u_projection_view";
    /** default name for model view matrix uniform **/
    public static String MODEL_VIEW_UNIFORM = "u_model_view";
    public static String TEXTURE_UNIFORM = "u_texture";
    #endregion
    #region Default Attributes
    /** default name for position attributes **/
    public static String POSITION_ATTRIBUTE = "a_position";
    /** default name for normal attribtues **/
    public static String NORMAL_ATTRIBUTE = "a_normal";
    /** default name for color attributes **/
    public static String COLOR_ATTRIBUTE = "a_color";
    /** default name for texcoords attributes, append texture unit number **/
    public static String TEXCOORD_ATTRIBUTE = "a_texCoord";
    
    /** default name for tangent attribute **/
    public static String TANGENT_ATTRIBUTE = "a_tangent";
    /** default name for binormal attribute **/
    public static String BINORMAL_ATTRIBUTE = "a_binormal";
    #endregion
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
      Game.logger.info(TAG, "Created handle {0}, Compiling...", pgmID);

      compile(vertex,   ShaderType.VertexShader, pgmID, out vsID);
      compile(fragment, ShaderType.FragmentShader, pgmID, out fsID);

      GL.LinkProgram(pgmID);
      //Game.logger.info(TAG, "Compiled " + vertex + " and " + fragment);
      Game.logger.info(TAG, "Compiled program {0}", pgmID);
      Game.logger.info(TAG, GL.GetProgramInfoLog(pgmID));

      Game.glResources.Add(this);
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
      Game.glResources.Remove(this);
    }

    #region Rendering
    public void use() {
      GL.UseProgram(pgmID);
    }
    public void begin() {
      GL.UseProgram(pgmID);
    }
    public void end() {
      GL.UseProgram(0);
    }
    #endregion
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
    public void uniformTexture(int unit) {
      GL.Uniform1(uniform(TEXTURE_UNIFORM + unit), unit);
    }
    public void projectUsingCamera(ref PerspecitveCamera perspecitveCamera) {
      uniformMatrix4(PROJECTION_UNIFORM, false, ref perspecitveCamera.combined);
    }
    public void projectUsingCamera(ref Camera2D camera) {
      uniformMatrix4(PROJECTION_UNIFORM, false, ref camera.combined);
    }

    #endregion
  }
}
