#version 150

varying vec2         v_textCoord0;
uniform sampler2D    u_texture0;
const float blurSizeH = 1.0 / 1366.0;
const float blurSizeV = 1.0 / 768.0;

void main() {
  vec4 sum = vec4(0.0);
  for (int x = -4; x <= 4; x++)
      for (int y = -4; y <= 4; y++)
          sum += texture(
              u_texture0,
              vec2(v_textCoord0.x + x * blurSizeH, v_textCoord0.y + y * blurSizeV)
          ) / 81.0;
  gl_FragColor = sum;
}