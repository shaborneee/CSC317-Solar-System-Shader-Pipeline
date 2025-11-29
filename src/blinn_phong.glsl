// Source: LAB 6 Shader Pipeline Submission
// Compute Blinn-Phong Shading given a material specification, a point on a
// surface and a light direction. Assume the light is white and has a low
// ambient intensity.
//
// Inputs:
//   ka  rgb ambient color
//   kd  rgb diffuse color
//   ks  rgb specular color
//   p  specular exponent (shininess)
//   n  unit surface normal direction
//   v  unit direction from point on object to eye
//   l  unit light direction
// Returns rgb color
vec3 blinn_phong(
  vec3 ka,
  vec3 kd,
  vec3 ks,
  float p,
  vec3 n,
  vec3 v,
  vec3 l)
{
  /////////////////////////////////////////////////////////////////////////////
  // Blinn-Phong shading: ambient + diffuse + specular
  // Assumes n, v, l are already normalized and point outward from surface
  
  // Ambient term
  vec3 ambient = ka;
  
  // Diffuse term (Lambert's law)
  float n_dot_l = max(0.0, dot(n, l));
  vec3 diffuse = kd * n_dot_l;
  
  // Specular term (Blinn-Phong using half-vector)
  vec3 h = normalize(v + l);  // Half-vector between view and light
  float n_dot_h = max(0.0, dot(n, h));
  vec3 specular = ks * pow(n_dot_h, p);
  
  return ambient + diffuse + specular;
  /////////////////////////////////////////////////////////////////////////////
}


