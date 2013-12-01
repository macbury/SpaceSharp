attribute vec4 a_position;
attribute vec2 a_texCoord0;

uniform mat4   u_projection_view;
uniform mat4   u_model_view;

varying   vec2 v_textCoord0;
void main() {
  v_textCoord0 = a_texCoord0;
  gl_Position  = u_projection_view * u_model_view * a_position;
}