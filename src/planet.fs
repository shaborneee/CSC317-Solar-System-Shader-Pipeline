// File Description: Realistic planet and sun shader with dynamic textures and lighting for all 8 planets and the sun
uniform mat4 view;
uniform mat4 proj;
uniform float animation_seconds;
uniform bool is_planet;
uniform int planet_index;
in vec3 sphere_fs_in;
in vec3 normal_fs_in;
in vec4 pos_fs_in; 
in vec4 view_pos_fs_in; 
out vec3 color;
void main()
{
    float theta = (animation_seconds * 2.0 * M_PI) / 8.0;
    vec3 light_world_pos = vec3(2.0 * cos(theta), 1.5, 2.0 * sin(theta));
    vec4 light_view = view * vec4(light_world_pos, 1.0);

    vec3 T, B;
    tangent(normalize(normal_fs_in), T, B);
    
    vec3 bump_sample_pos = bump_position(is_planet, sphere_fs_in);
    float offset = 0.001;
    
    float center_height = bump_height(is_planet, bump_sample_pos);
    float tangent_height = bump_height(is_planet, bump_sample_pos + offset * T);
    float bitangent_height = bump_height(is_planet, bump_sample_pos + offset * B);
    
    float gradient_tangent = (tangent_height - center_height) / offset;
    float gradient_bitangent = (bitangent_height - center_height) / offset;
    
    vec3 perturbed_normal = normalize(normal_fs_in - gradient_tangent * T - gradient_bitangent * B);

    float height = bump_height(is_planet, bump_sample_pos);
    
    vec3 planet_ka, planet_kd, planet_ks;
    float p;
    
    if (!is_planet) {
        // Sun - intense glowing surface with slow rotation
        float slow_rotation = 0.08;
        vec3 rotated_pos = vec3(
            sphere_fs_in.x * cos(slow_rotation * animation_seconds) - sphere_fs_in.z * sin(slow_rotation * animation_seconds),
            sphere_fs_in.y,
            sphere_fs_in.x * sin(slow_rotation * animation_seconds) + sphere_fs_in.z * cos(slow_rotation * animation_seconds)
        );
        
        float yellow_color = improved_perlin_noise(rotated_pos * 12.0 + vec3(animation_seconds * 0.15, 0, 0));
        float orange_color = improved_perlin_noise(rotated_pos * 6.0 + vec3(0, animation_seconds * 0.08, animation_seconds * 0.12));
        float red_color = improved_perlin_noise(rotated_pos * 20.0 + vec3(animation_seconds * 0.3, 0, 0));
        
        float radial_intensity = 1.0 - length(sphere_fs_in.xy) * 0.3;
        radial_intensity = clamp(radial_intensity, 0.3, 1.0);
        
        if (red_color > 0.4) {
            planet_ka = vec3(0.4, 0.3, 0.1) * radial_intensity;
            planet_kd = vec3(1.5, 1.2, 0.6) * radial_intensity;
            planet_ks = vec3(1.2, 1.0, 0.8);
        } else if (yellow_color > 0.2) {
            planet_ka = vec3(0.3, 0.15, 0.05) * radial_intensity;
            planet_kd = vec3(1.3, 0.8, 0.3) * radial_intensity;
            planet_ks = vec3(1.0, 0.7, 0.4);
        } else if (orange_color > 0.1) {
            planet_ka = vec3(0.25, 0.1, 0.03) * radial_intensity;
            planet_kd = vec3(1.1, 0.6, 0.2) * radial_intensity;
            planet_ks = vec3(0.9, 0.5, 0.25);
        } else {
            planet_ka = vec3(0.2, 0.08, 0.02) * radial_intensity;
            planet_kd = vec3(1.0, 0.5, 0.15) * radial_intensity;
            planet_ks = vec3(0.8, 0.4, 0.2);
        }
        p = 100.0;
    } else if (planet_index == 1) {
        // Mercury: Gray bumpy surface
        float theta = (animation_seconds * 2.0 * M_PI) / 52.0;
        vec3 rotated_pos = vec3(
            sphere_fs_in.x * cos(theta) - sphere_fs_in.z * sin(theta),
            sphere_fs_in.y,
            sphere_fs_in.x * sin(theta) + sphere_fs_in.z * cos(theta)
        );
        float dark_gray = improved_perlin_noise(rotated_pos * 8.0);
        float medium_gray = improved_perlin_noise(rotated_pos * 25.0);
        float light_gray = improved_perlin_noise(rotated_pos * 60.0);
        float white_spots = improved_perlin_noise(rotated_pos * 150.0);
        
        float color_variation = (dark_gray * 0.4 + medium_gray * 0.3 + light_gray * 0.2 + white_spots * 0.1);
        
        vec3 base_color = vec3(0.8, 0.7, 0.6);
        vec3 dark_areas = vec3(0.6, 0.5, 0.4); 
        vec3 bright_areas = vec3(0.45, 0.45, 0.42);
        

        vec3 surface_color;
        if (color_variation < -0.2) {
            surface_color = dark_areas;
        } else if (color_variation > 0.2) {
            surface_color = bright_areas;
        } else {
            surface_color = base_color + vec3(color_variation * 0.1);
        }
        
        planet_ka = surface_color * 0.1;
        planet_kd = surface_color;
        planet_ks = vec3(0.05, 0.05, 0.05);
        p = 1000.0;

    } else if (planet_index == 2) {
        // Venus: Golden-yellow atmosphere with cloudy patterns
        float theta = (animation_seconds * 2.0 * M_PI) / 45.0;
        vec3 rotated_pos = vec3(
            sphere_fs_in.x * cos(theta) - sphere_fs_in.z * sin(theta),
            sphere_fs_in.y,
            sphere_fs_in.x * sin(theta) + sphere_fs_in.z * cos(theta)
        );
        float large_swirls = improved_perlin_noise(rotated_pos * 3.0 + vec3(animation_seconds * 0.08, 0, 0));
        float medium_clouds = improved_perlin_noise(rotated_pos * 7.0 + vec3(0, animation_seconds * 0.12, 0));
        float fine_details = improved_perlin_noise(rotated_pos * 15.0 + vec3(animation_seconds * 0.15, 0, animation_seconds * 0.1));
        
        
        vec3 base_gold = vec3(1.0, 0.8, 0.3);     
        vec3 deep_gold = vec3(0.9, 0.6, 0.2);      
        vec3 bright_yellow = vec3(1.0, 0.9, 0.4); 
        vec3 volcanic_orange = vec3(0.8, 0.5, 0.1);
        
        vec3 surface_color;
        surface_color = base_gold;
        surface_color += medium_clouds * 0.1;
        surface_color += fine_details * 0.05;

        float swirl_intensity = sin(large_swirls * 6.28) * 0.1;
        surface_color += vec3(swirl_intensity, swirl_intensity * 0.8, swirl_intensity * 0.3);
        
        planet_ka = surface_color * 0.15;
        planet_kd = surface_color;
        planet_ks = vec3(0.4, 0.35, 0.2); 
        p = 250.0;
    } else if (planet_index == 3) {

        // Earth: Blue oceans, green-brown continents, white polar ice caps, and dynamic cloud cover
        float theta = (animation_seconds * 2.0 * M_PI) / 18.0;
        vec3 rotated_pos = vec3(
            sphere_fs_in.x * cos(theta) - sphere_fs_in.z * sin(theta),
            sphere_fs_in.y,
            sphere_fs_in.x * sin(theta) + sphere_fs_in.z * cos(theta)
        );
        
        float rotated_height = bump_height(true, rotated_pos);
        float latitude = abs(rotated_pos.y); 
        float ice_threshold = 0.85; 
        
        if (latitude > ice_threshold) {
            planet_ka = vec3(0.2, 0.22, 0.25);
            planet_kd = vec3(0.9, 0.95, 1.0);
            planet_ks = vec3(0.7, 0.8, 0.9);
        } else if (rotated_height < 0.02) {
            planet_ka = vec3(0.01, 0.02, 0.05);
            planet_kd = vec3(0.1, 0.3, 0.8);
            planet_ks = vec3(0.3, 0.4, 0.5);
        } else {
            planet_ka = vec3(0.01, 0.05, 0.02);
            planet_kd = vec3(0.2, 0.6, 0.1);
            planet_ks = vec3(0.1, 0.2, 0.1);
        }
        p = 1000.0;
    } else if (planet_index == 4) {
        // Mars: Rusty red surface with bumps 
        float theta = (animation_seconds * 2.0 * M_PI) / 18.5;
        vec3 rotated_pos = vec3(
            sphere_fs_in.x * cos(theta) - sphere_fs_in.z * sin(theta),
            sphere_fs_in.y,
            sphere_fs_in.x * sin(theta) + sphere_fs_in.z * cos(theta)
        );
        
        float rotated_height = bump_height(true, rotated_pos);
        float latitude = abs(rotated_pos.y);
        float longitude = atan(rotated_pos.z, rotated_pos.x);
        float bumpy = improved_perlin_noise(rotated_pos * 8.0);
        float swirl = improved_perlin_noise(rotated_pos * 12.0 + vec3(animation_seconds * 0.05, 0, 0));
        
        vec3 surface_color;
        
        if (rotated_height > 0.03) {
            surface_color = vec3(0.8, 0.5, 0.25) + bumpy * 0.1;

            planet_ka = surface_color * 0.1;
            planet_kd = surface_color;
            planet_ks = vec3(0.15, 0.1, 0.05);
        }

        else if (rotated_height < -0.01) {
            surface_color = vec3(0.45, 0.25, 0.12) + swirl * 0.15;
            planet_ka = surface_color * 0.08;
            planet_kd = surface_color;
            planet_ks = vec3(0.08, 0.04, 0.02);
        }
        else {
            if (swirl > 0.3) {
                surface_color = vec3(0.85, 0.6, 0.3);
            } else {
                surface_color = vec3(0.7, 0.42, 0.18) + bumpy * 0.1;
            }
            planet_ka = surface_color * 0.1;
            planet_kd = surface_color;
            planet_ks = vec3(0.12, 0.08, 0.04);
        }
        
        p = 1000.0;
    } else if (planet_index == 5) {

        // Jupiter: Bands with swirl patterns and the red spot
        float theta = (animation_seconds * 2.0 * M_PI) / 9.7;
        vec3 rotated_pos = vec3(
            sphere_fs_in.x * cos(theta) - sphere_fs_in.z * sin(theta),
            sphere_fs_in.y,
            sphere_fs_in.x * sin(theta) + sphere_fs_in.z * cos(theta)
        );
        
        float major_bands = sin(rotated_pos.y * 12.0) * 0.5 + 0.5;
        float fine_bands = sin(rotated_pos.y * 35.0 + animation_seconds * 0.2) * 0.3 + 0.7;
        float longitude = atan(rotated_pos.z, rotated_pos.x);
        
        float red_spot = improved_perlin_noise(rotated_pos * 6.0 + vec3(animation_seconds * 0.05, 0, 0));
        float white_color = improved_perlin_noise(rotated_pos * 12.0 + vec3(0, animation_seconds * 0.08, 0));
        float swirl_color = improved_perlin_noise(rotated_pos * 20.0 + vec3(animation_seconds * 0.1, 0, animation_seconds * 0.07));
        
        vec3 cream_bands = vec3(0.95, 0.85, 0.7);       
        vec3 orange_bands = vec3(0.9, 0.6, 0.35);        
        vec3 dark_bands = vec3(0.6, 0.45, 0.3);          
        vec3 red_spot_color = vec3(0.8, 0.4, 0.25);     
        vec3 white_swirl = vec3(0.9, 0.9, 0.85);        
        
        vec3 surface_color;
        
        if (red_spot > 0.35 && rotated_pos.y > -0.3 && rotated_pos.y < 0.1) {
            surface_color = mix(red_spot_color, orange_bands, red_spot);
        }
        else if (white_color > 0.4) {
            surface_color = mix(white_swirl, cream_bands, white_color);
        }
        else {
            float band_intensity = major_bands * fine_bands;
            if (band_intensity > 0.7) {
                surface_color = cream_bands;
            } else if (band_intensity > 0.4) {
                surface_color = orange_bands;
            } else {
                surface_color = dark_bands;
            }
        }
        
        planet_ka = surface_color * 0.15;
        planet_kd = surface_color;
        planet_ks = vec3(0.25, 0.2, 0.15); 
        p = 200.0;
    } else if (planet_index == 6) {
        // Saturn: Golden with small white bands 
        float theta = (animation_seconds * 2.0 * M_PI) / 10.9;
        vec3 rotated_pos = vec3(
            sphere_fs_in.x * cos(theta) - sphere_fs_in.z * sin(theta),
            sphere_fs_in.y,
            sphere_fs_in.x * sin(theta) + sphere_fs_in.z * cos(theta)
        );
        
        float gold_bands = sin(rotated_pos.y * 8.0) * 0.2 + 0.8;
        float cream_bands_var = sin(rotated_pos.y * 25.0 + animation_seconds * 0.15) * 0.15 + 0.85;
        float light_gold_var = improved_perlin_noise(rotated_pos * 6.0 + vec3(animation_seconds * 0.05, 0, 0));
        
        vec3 light_gold = vec3(0.95, 0.88, 0.65);
        vec3 cream_white = vec3(0.98, 0.95, 0.85);
        vec3 light_brown = vec3(0.85, 0.75, 0.55);
        vec3 atmospheric_white = vec3(0.96, 0.94, 0.88);
        
        vec3 surface_color;
        
        if (light_gold_var > 0.4) {
            surface_color = mix(atmospheric_white, light_gold,light_gold_var);
        }
        else {
            float band_intensity = gold_bands * cream_bands_var;
            if (band_intensity > 0.85) {
                surface_color = cream_white + light_gold * 0.05;
            } else {
                surface_color = light_brown + light_gold * 0.06;
            }
        }
        
        planet_ka = surface_color * 0.2;
        planet_kd = surface_color;
        planet_ks = vec3(0.4, 0.35, 0.25);
        p = 150.0;
    } else if (planet_index == 7) {
        // Uranus: Blue-green planet with small bands
        float theta = (animation_seconds * 2.0 * M_PI) / 12.6;
        vec3 rotated_pos = vec3(
            sphere_fs_in.x * cos(theta) - sphere_fs_in.z * sin(theta),
            sphere_fs_in.y,
            sphere_fs_in.x * sin(theta) + sphere_fs_in.z * cos(theta)
        );
        
        float blue_green = improved_perlin_noise(rotated_pos * 3.0 + vec3(animation_seconds * 0.03, 0, 0));
        float ice_blue = sin(rotated_pos.y * 6.0) * 0.1 + 0.9;
        
        vec3 surface_color = vec3(0.7, 0.85, 0.9);
        surface_color += blue_green * 0.05;
        
        planet_ka = surface_color * 0.2;
        planet_kd = surface_color;
        planet_ks = vec3(0.4, 0.45, 0.5);
        p = 250.0;
    } else if (planet_index == 8) {
        // Neptune: Deep blue planet with clouds
        float theta = (animation_seconds * 2.0 * M_PI) / 13.4;
        vec3 rotated_pos = vec3(
            sphere_fs_in.x * cos(theta) - sphere_fs_in.z * sin(theta),
            sphere_fs_in.y,
            sphere_fs_in.x * sin(theta) + sphere_fs_in.z * cos(theta)
        );
        
        float dark_blue = improved_perlin_noise(rotated_pos * 5.0 + vec3(animation_seconds * 0.06, 0, 0));
        float bright_blue = improved_perlin_noise(rotated_pos * 12.0 + vec3(0, animation_seconds * 0.12, 0));
        float deep_blue = sin(rotated_pos.y * 8.0 + animation_seconds * 0.1) * 0.3 + 0.7;
      
        
        vec3 surface_color = vec3(0.15, 0.35, 0.9);
            float band_intensity = deep_blue;
            if (band_intensity > 0.75) {
                surface_color += vec3(0.05, 0.1, 0.15);
            } else if (band_intensity > 0.5) {
                surface_color += vec3(0.03, 0.07, 0.1);
            } else {
                surface_color += vec3(0.01, 0.04, 0.05);
            }
        
        planet_ka = surface_color * 0.15;
        planet_kd = surface_color;
        planet_ks = vec3(0.4, 0.5, 0.6);
        p = 350.0;
    } 

    vec3 n = normalize(perturbed_normal);
    vec3 v = normalize(-view_pos_fs_in.xyz);
    vec3 l = normalize(light_view.xyz - view_pos_fs_in.xyz);

    color = blinn_phong(planet_ka, planet_kd, planet_ks, p, n, v, l);

  if (!is_planet) {

    // Sun: Add more complex dynamic lighting effects
    float slow_rotation = 0.3;
    vec3 rotated_solar_pos = vec3(
        sphere_fs_in.x * cos(slow_rotation) - sphere_fs_in.z * sin(slow_rotation),
        sphere_fs_in.y,
        sphere_fs_in.x * sin(slow_rotation) + sphere_fs_in.z * cos(slow_rotation)
    );
    
    vec3 solar_pos = rotated_solar_pos + vec3(animation_seconds * 0.08, 0, 0);
    color = color * 1.3;
  } else if (is_planet && planet_index == 3) {

    // Earth: Cloud
    float rotation_speed = 0.3;
    vec3 rotated_cloud_pos = vec3(
        sphere_fs_in.x * cos(rotation_speed) - sphere_fs_in.z * sin(rotation_speed),
        sphere_fs_in.y,
        sphere_fs_in.x * sin(rotation_speed) + sphere_fs_in.z * cos(rotation_speed)
    );
    vec3 moving_pos = rotated_cloud_pos + vec3(animation_seconds * 0.05, 0, 0);
    vec3 cloud_position = vec3(1.5, 6.0, 1.5);
    float cloud_pattern = improved_perlin_noise(moving_pos * cloud_position * 3.0);

    if (cloud_pattern > 0.1) {
      vec3 cloud_ka = vec3(0.06, 0.06, 0.06) * cloud_pattern;
      vec3 cloud_kd = vec3(1.2, 1.2, 1.2) * cloud_pattern;
      vec3 cloud_ks = vec3(0.2, 0.2, 0.2) * cloud_pattern;

      vec3 cloud_normal = normalize(normal_fs_in);
      vec3 cloud_result = blinn_phong(cloud_ka, cloud_kd, cloud_ks, p, cloud_normal, v, l);
      color = color + cloud_result * 2.5;
    }
  }
}
