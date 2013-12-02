#ifdef GL_ES
precision mediump float;
#endif

uniform vec3 u_color;
uniform vec2 u_viewport;

void main() {
  vec2 uv      = gl_FragCoord.xy/u_viewport; 
  float res    = mix(0.0, 0.0001, uv.x);
  gl_FragColor = vec4(u_color, res); 
}