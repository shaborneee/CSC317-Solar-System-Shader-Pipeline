// Source: LAB 6 Shader Pipeline Submission (changes made to include is_planet variable to distinguish whether it's a planet or the sun)
// Set the pixel color using Blinn-Phong shading (e.g., with constant blue and
// gray material color) with a bumpy texture.
// 
// Uniforms:
uniform mat4 view;
uniform mat4 proj;
uniform float animation_seconds;
uniform bool is_planet;
// Inputs:
//                     linearly interpolated from tessellation evaluation shader
//                     output
in vec3 sphere_fs_in;
in vec3 normal_fs_in;
in vec4 pos_fs_in; 
in vec4 view_pos_fs_in; 
// Outputs:
//               rgb color of this pixel
out vec3 color;
// expects: model, blinn_phong, bump_height, bump_position,
// improved_perlin_noise, tangent
void main()
{
  /////////////////////////////////////////////////////////////////////////////
  vec3 T, B;
  tangent(normalize(normal_fs_in), T, B);
  
  // Use bump_position for consistent bump mapping coordinates
  vec3 bump_sample_pos = bump_position(is_planet, sphere_fs_in);
  float offset = 0.001;
  
  float center_height = bump_height(is_planet, bump_sample_pos);
  float tangent_height = bump_height(is_planet, bump_sample_pos + offset * T);
  float bitangent_height = bump_height(is_planet, bump_sample_pos + offset * B);

  // Calculate gradients
  float gradient_tangent = (tangent_height - center_height) / offset;
  float gradient_bitangent = (bitangent_height - center_height) / offset;
  
  vec3 perturbed_normal = normalize(normal_fs_in - gradient_tangent * T - gradient_bitangent * B);
  
  // Lighting setup matching the reference
  float theta = (animation_seconds * 2.0 * M_PI) / 8.0;
  vec3 light_world_pos = vec3(3.0 * cos(theta), 2.5, 3.0 * sin(theta));
  vec4 light_view = view * vec4(light_world_pos, 1.0);
  
  // Material properties
  vec3 ka, kd, ks;
  float p;
  vec3 n, v, l;

  if (is_planet) {
    ka = vec3(0.05, 0.05, 0.05);
    kd = vec3(0.4, 0.4, 0.4);
    ks = vec3(0.1, 0.1, 0.1);
    p = 1000.0;
  } else {
    ka = vec3(0.02, 0.05, 0.15);
    kd = vec3(0.1, 0.3, 0.8);
    ks = vec3(0.3, 0.4, 0.5);
    p = 1000.0;
  }

  n = normalize(perturbed_normal);
  v = normalize(-view_pos_fs_in.xyz);
  l = normalize(light_view.xyz - view_pos_fs_in.xyz);
  
  color = blinn_phong(ka, kd, ks, p, n, v, l);
  /////////////////////////////////////////////////////////////////////////////
}