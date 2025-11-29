// File Description: Solar system model transformations - Sun is at the center and planets orbit around it without colliding
// Inputs:
//   is_planet  whether we're considering a planet
//   time  seconds on animation clock
// Returns affine model transformation as 4x4 matrix
//
// expects: identity, rotate_about_y, translate, PI
mat4 model(bool is_planet, float time, int planet_index)
{
  /////////////////////////////////////////////////////////////////////////////
  // Early check: if not planet, make sun bigger and return
  if (!is_planet) {
    return uniform_scale(2.0); // Make sun 2x bigger
  }
  
  // Realistic orbital speeds based on actual planetary data from: https://www.jpl.nasa.gov/_edu/pdfs/scaless_reference.pdf
  float orbital_speed;
  if (planet_index == 1) orbital_speed = 47.9 / 10.0; // Mercury 
  else if (planet_index == 2) orbital_speed = 35.0 / 10.0; // Venus
  else if (planet_index == 3) orbital_speed = 29.8 / 10.0; // Earth
  else if (planet_index == 4) orbital_speed = 24.1 / 10.0; // Mars
  else if (planet_index == 5) orbital_speed = 13.1 / 10.0; // Jupiter
  else if (planet_index == 6) orbital_speed = 9.7 / 10.0; // Saturn
  else if (planet_index == 7) orbital_speed = 6.8 / 10.0; // Uranus
  else if (planet_index == 8) orbital_speed = 5.4 / 10.0; // Neptune 
  else orbital_speed = 1.0; 
  
  float angle = time * orbital_speed * 0.1; // Scale down for visuals
  
  float scale;
  if (planet_index == 1) scale = 0.6; // Mercury
  else if (planet_index == 2) scale = 0.65; // Venus
  else if (planet_index == 3) scale = 0.7; // Earth
  else if (planet_index == 4) scale = 0.6; // Mars
  else if (planet_index == 5) scale = 1.2; // Jupiter - largest
  else if (planet_index == 6) scale = 1.0; // Saturn
  else if (planet_index == 7) scale = 0.8; // Uranus
  else if (planet_index == 8) scale = 0.75; // Neptune
  else scale = 0.6; // Default

  // Orbital distances calculated to prevent overlap (distance > sum of adjacent planet radii)
  float distance;
  if (planet_index == 1) distance = 4.0;  
  else if (planet_index == 2) distance = 6.0;  
  else if (planet_index == 3) distance = 8.5;  
  else if (planet_index == 4) distance = 11.0;  
  else if (planet_index == 5) distance = 15.0;
  else if (planet_index == 6) distance = 19.0;
  else if (planet_index == 7) distance = 22.5; 
  else if (planet_index == 8) distance = 25.0; 
  else distance = 8.0; 
  
  return rotate_about_y(angle) * translate(vec3(distance, 0.0, 0.0)) * uniform_scale(scale);
  /////////////////////////////////////////////////////////////////////////////
}
