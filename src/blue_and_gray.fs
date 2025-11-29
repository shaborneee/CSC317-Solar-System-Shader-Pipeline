// File Description: Set the pixel color to different colors depending on planet index.
// inputs:
uniform bool is_planet;
uniform int planet_index;
// Outputs:
out vec3 color;
void main()
{
  /////////////////////////////////////////////////////////////////////////////
  if (!is_planet) {
    color = vec3(1.0, 0.9, 0.1); // Bright yellow-orange for Sun
  } else {
    // More realistic planet colors
    if (planet_index == 1) color = vec3(0.5, 0.4, 0.3); 
    else if (planet_index == 2) color = vec3(0.9, 0.8, 0.4);
    else if (planet_index == 3) color = vec3(0.2, 0.5, 0.8); 
    else if (planet_index == 4) color = vec3(0.7, 0.4, 0.2); 
    else if (planet_index == 5) color = vec3(0.8, 0.6, 0.3); 
    else if (planet_index == 6) color = vec3(0.9, 0.8, 0.5); 
    else if (planet_index == 7) color = vec3(0.4, 0.8, 0.9); 
    else if (planet_index == 8) color = vec3(0.2, 0.4, 0.9); 
  }
  /////////////////////////////////////////////////////////////////////////////
}
