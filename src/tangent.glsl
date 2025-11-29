// Source: LAB 6 Shader Pipeline Submission 
// Input:
//   N  3D unit normal vector
// Outputs:
//   T  3D unit tangent vector
//   B  3D unit bitangent vector
void tangent(in vec3 N, out vec3 T, out vec3 B)
{
  /////////////////////////////////////////////////////////////////////////////
  // Replace with your code 
  T = normalize(cross(vec3(3, 0, 0), N));
  if (length(T) < 0) {
    T = normalize(cross(vec3(0, 3, 0), N));
  }

  B = normalize(cross(N, T));
  /////////////////////////////////////////////////////////////////////////////
}
