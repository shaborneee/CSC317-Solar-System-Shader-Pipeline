// Source: LAB 6 Shader Pipeline Submission (changes made to include is_planet variable to distinguish whether it's a planet or the sun)
// Add (hard code) an orbiting (point or directional) light to the scene. Light
// the scene using the Blinn-Phong Lighting Model.
//
// Uniforms:
uniform mat4 view;
uniform mat4 proj;
uniform float animation_seconds;
uniform bool is_planet;
uniform int planet_index;
// Inputs:
in vec3 sphere_fs_in;
in vec3 normal_fs_in;
in vec4 pos_fs_in; 
in vec4 view_pos_fs_in; 
// Outputs:
out vec3 color;
// expects: PI, blinn_phong
void main()
{
  /////////////////////////////////////////////////////////////////////////////
  // Light orbits 1 revolution per 8 seconds
  float theta = (animation_seconds * 2.0 * M_PI) / 8.0;

  vec3 light_world_pos = vec3(2.0 * cos(theta), 1.5, 2.0 * sin(theta));
  vec4 light_view = view * vec4(light_world_pos, 1.0);

  vec3 ka, ks, kd;
  float p;
  vec3 n,v,l;

  if (is_planet) {
    ka = vec3(0.05, 0.05, 0.05);            
    kd = vec3(0.5, 0.5 ,0.5);             
    ks = vec3(1.0, 1.0, 1.0);              
    p = 500.0;                   
  } else {
      ka = vec3(0.03, 0.03, 0.08);            
      kd = vec3(0.1, 0.1, 0.9);   
      ks = vec3(1.0);             
      p = 1000.0;                   
  }
  n = normalize(normal_fs_in);  
  v = normalize(-view_pos_fs_in.xyz);  
  l = normalize(light_view.xyz - view_pos_fs_in.xyz); 

  color = blinn_phong(ka, kd, ks, p, n, v, l);
  /////////////////////////////////////////////////////////////////////////////
}
