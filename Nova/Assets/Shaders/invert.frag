#version 150

varying vec2         v_textCoord0;
uniform sampler2D    u_texture0;
void main() {
  gl_FragColor = vec4(1.0, 1.0, 1.0, 1.0) - texture2D(u_texture0, v_textCoord0);
}