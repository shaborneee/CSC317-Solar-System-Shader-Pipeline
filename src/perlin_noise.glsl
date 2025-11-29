// Source: LAB 6 Shader Pipeline Submission
// Given a 3d position as a seed, compute a smooth procedural noise
// value: "Perlin Noise", also known as "Gradient noise".
//
// Inputs:
//   st  3D seed
// Returns a smooth value between (-1,1)
//
// expects: random_direction, smooth_step
float perlin_noise( vec3 st) 
{
  /////////////////////////////////////////////////////////////////////////////
    vec3 i = floor(st);
    vec3 f = fract(st);
    vec3 u = smooth_step(f);

    // Gradient directions at the 8 cube corners
    vec3 gradient0 = random_direction(i + vec3(0,0,0));
    vec3 gradient1 = random_direction(i + vec3(1,0,0));
    vec3 gradient2 = random_direction(i + vec3(0,1,0));
    vec3 gradient3 = random_direction(i + vec3(1,1,0));
    vec3 gradient4 = random_direction(i + vec3(0,0,1));
    vec3 gradient5 = random_direction(i + vec3(1,0,1));
    vec3 gradient6 = random_direction(i + vec3(0,1,1));
    vec3 gradient7 = random_direction(i + vec3(1,1,1));

    // Offset from each corner
    vec3 offset0 = f - vec3(0,0,0);
    vec3 offset1 = f - vec3(1,0,0);
    vec3 offset2 = f - vec3(0,1,0);
    vec3 offset3 = f - vec3(1,1,0);
    vec3 offset4 = f - vec3(0,0,1);
    vec3 offset5 = f - vec3(1,0,1);
    vec3 offset6 = f - vec3(0,1,1);
    vec3 offset7 = f - vec3(1,1,1);

    // Dot products = gradient contribution
    float dot0 = dot(gradient0, offset0);
    float dot1 = dot(gradient1, offset1);
    float dot2 = dot(gradient2, offset2);
    float dot3 = dot(gradient3, offset3);
    float dot4 = dot(gradient4, offset4);
    float dot5 = dot(gradient5, offset5);
    float dot6 = dot(gradient6, offset6);
    float dot7 = dot(gradient7, offset7);

    float interpolatex0 = mix(dot0, dot1, u.x);
    float interpolatex1 = mix(dot2, dot3, u.x);
    float interpolatex2 = mix(dot4, dot5, u.x);
    float interpolatex3 = mix(dot6, dot7, u.x);

    float interpolatey0 = mix(interpolatex0, interpolatex1, u.y);
    float interpolatey1 = mix(interpolatex2, interpolatex3, u.y);

    return mix(interpolatey0, interpolatey1, u.z);
  /////////////////////////////////////////////////////////////////////////////
}

