/// Source: LAB 6 Shader Pipeline Submission
/ Filter an input value to perform a smooth step. This function should be a
// cubic polynomial with smooth_step(0) = 0, smooth_step(1) = 1, and zero first
// derivatives at f=0 and f=1. 
//
// Inputs:
//   f  input value
// Returns filtered output value
float smooth_step( float f)
{
  /////////////////////////////////////////////////////////////////////////////
  if (f <= 0.0) return 0.0;
  if (f >= 1.0) return 1.0;
  return 3.0 * f * f - 2.0 * f * f * f;
  /////////////////////////////////////////////////////////////////////////////
}

vec3 smooth_step( vec3 f)
{
  /////////////////////////////////////////////////////////////////////////////
  
  return vec3(smooth_step(f.x), smooth_step(f.y), smooth_step(f.z));

  /////////////////////////////////////////////////////////////////////////////
}
