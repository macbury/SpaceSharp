
attribute vec3 a_position;
attribute vec4 a_color;

uniform mat4   u_projection_view;
uniform mat4   u_model_view;

varying   vec4 v_color;
void main() {
  v_color     = a_color;
  gl_Position = u_projection_view * u_model_view * vec4(a_position, 1.0);
}