// Source: LAB 6 Shader Pipeline Submission
// Generate a pseudorandom unit 3D vector
// 
// Inputs:
//   seed  3D seed
// Returns psuedorandom, unit 3D vector drawn from uniform distribution over
// the unit sphere (assuming random2 is uniform over [0,1]²).
//
// expects: random2.glsl, PI.glsl
vec3 random_direction( vec3 seed)
{
  /////////////////////////////////////////////////////////////////////////////
  // Generate uniform random point on unit sphere using spherical coordinates
  vec2 u = random2(seed);
  return normalize(vec3(sin(M_PI * u.y) * cos(2.0 * M_PI * u.x), sin(M_PI * u.y) * sin(2.0 * M_PI * u.x), cos(M_PI * u.y)));
  /////////////////////////////////////////////////////////////////////////////
}
