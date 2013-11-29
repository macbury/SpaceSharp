#version 150

varying vec2         v_textCoord0;
uniform sampler2D    u_texture0;
void main() {
  gl_FragColor = texture2D(u_texture0, v_textCoord0);
}