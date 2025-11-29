// Source: LAB 6 Shader Pipeline Submission (changes made to include is_planet variable to distinguish whether it's a planet or the sun)
// Create a bumpy surface by using procedural noise to generate a height (
// displacement in normal direction).
//
// Inputs:
//   is_planet  whether we're looking at a planet or the sun
//   s  3D position of seed for noise generation
// Returns elevation adjust along normal (values between -0.1 and 0.1 are
//   reasonable.
float bump_height( bool is_planet, vec3 s)
{
  /////////////////////////////////////////////////////////////////////////////
  if (is_planet) {
    float bumps = improved_perlin_noise(s * 3.0);
    return 0.1 * smooth_heaviside(bumps, 4.0);

  } else {
    float large_bumps = improved_perlin_noise(s * 1.5);  
    float medium_bumps = improved_perlin_noise(s * 4.0);  
    float small_bumps = improved_perlin_noise(s * 8.0);     
    
    float bumps = large_bumps + medium_bumps * 0.7 + small_bumps * 0.5;
    return 0.1 * smooth_heaviside(bumps, 4.5);
  }
  /////////////////////////////////////////////////////////////////////////////
}
