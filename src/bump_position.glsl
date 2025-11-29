// Source: LAB 6 Shader Pipeline Submission (changes made to include is_planet variable to distinguish whether it's a planet or the sun)
// Create a bumpy surface by using procedural noise to generate a new 3D position
// via displacement in normal direction.
// 
// Inputs:
//   is_planet  whether we're looking at a planet or the sun
//   s  3D position of seed for noise generation, also assumed to be surface
//     point on the unit spher (and thus also equal to its normal)
// Returns 3D position of p adjusted along n by bump amount
//
// Hint: for a unit sphere object, you might use s=p=n
//
// expects: bump_height
vec3 bump_position(bool is_planet , vec3 s)
{
  /////////////////////////////////////////////////////////////////////////////
  float height = bump_height(is_planet, s);
  return s + height * s;
  /////////////////////////////////////////////////////////////////////////////
}
