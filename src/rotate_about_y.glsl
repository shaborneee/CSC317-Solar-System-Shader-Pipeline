// Source: LAB 6 Shader Pipeline Submission
// Inputs:
//   theta  amount y which to rotate (in radians)
// Return a 4x4 matrix that rotates a given 3D point/vector about the y-axis by
// the given amount.
mat4 rotate_about_y(float theta)
{
  /////////////////////////////////////////////////////////////////////////////
  // Replace with your code 
  float c = cos(theta);
  float s = sin(theta);
  return mat4(
    c,0,-s,0, 
    0,1,0,0,  
    s,0,c,0,    
    0,0,0,1);  
  /////////////////////////////////////////////////////////////////////////////
}

