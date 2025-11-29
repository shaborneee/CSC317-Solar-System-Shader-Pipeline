# Solar System Shader Pipeline

![](submission-images/Solar-System.mp4)

## Personal Information
- **Full Name:** Shabornee Saha
- **UtorID:** saahashab
- **Student Number:** 1008200803
- **Assignment Augmented:** Assignment 6 (A6)

## Instructions

To run this solar system shader pipeline:

1. **Clone the repository:**
   ```bash
   git clone https://github.com/ohnooj/computer-graphics-shader-pipeline.git
   ```

2. **Replace source files:**
   Replace all source files with the provided shader files and main.cpp

3. **Build the project:**
   ```bash
   mkdir build
   cd build
   cmake ..
   ```

4. **Build the solution:**
   Build the generated solution file in your preferred IDE or use:
   ```bash
   make
   ```

5. **Run the program:**
   ```bash
   ./shaderpipeline ../data/test-08.json
   ```

## Description

A realistic solar system simulation using OpenGL shaders with dynamic textures, individual rotations, and lighting effects. Features the Sun and 8 planets with unique visual characteristics and orbital mechanics.

### Key Features:
- **Complete Solar System:** Sun + 8 planets with individual rotations and orbital motion
- **Unique Planet Textures:** Each planet has distinct colors and surface patterns
- **Realistic Lighting:** Blinn-Phong shading with orbiting light source
- **Implementation:** `main.cpp`, `src/model.glsl`, `src/planet.fs`

## Acknowledgements

- **Base Framework:** Built upon my Assignment 6 submission for CSC317
- **Teaching Team:** Special thanks to the CSC317 teaching team for the lab setup and shader pipeline framework
- **Planetary Data Reference:** Orbital distances and characteristics referenced from NASA's Scale Solar System guide: https://www.jpl.nasa.gov/_edu/pdfs/scaless_reference.pdf
- **Graphics Libraries:** 
  - OpenGL for rendering

![Solar System Screenshot](submission-images/Screenshot%202025-11-29%20034508.jpg)
[coordinates](https://en.wikipedia.org/wiki/Homogeneous_coordinates) 

Running `./shaderpipeline ../data/test-08.json` should display a creative planet
scene. For example: 

![](submission-images/solar-system-render.jpg)
