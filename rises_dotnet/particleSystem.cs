using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

// Define a particle class
namespace Rise
{
    public class Particle
    {
        public PointF Position { get; set; }
        public PointF Velocity { get; set; }
        public float Size { get; set; }
        public Color Color { get; set; }
        public int Life { get; set; }
        public float Angle { get; set; }
        public float AngularVelocity { get; set; }
    }

    // Create a particle system class
    public class ParticleSystem
    {
        private List<Particle> particles;
        private Random random;

        public ParticleSystem()
        {
            particles = new List<Particle>();
            random = new Random();
        }
        public void CreateFire(PointF center, float radius, int particleCount, int particleLife)
        {
            for (int i = 0; i < particleCount; i++)
            {
                float angle = (float)(random.NextDouble() * Math.PI * 2);
                float speed = (float)(random.NextDouble() * 2) + 1;
                float size = (float)(random.NextDouble() * 6) + 1;

                int alpha = 255 - (int)((size / 7) * 255); // Adjust alpha based on size for a 3D effect
                Color color = Color.FromArgb(alpha, 255, 150, 0); // Orange fire color

                Particle particle = new Particle
                {
                    Position = center,
                    Velocity = new PointF((float)Math.Cos(angle) * speed, (float)Math.Sin(angle) * speed),
                    Size = size,
                    Color = color,
                    Life = particleLife,
                    Angle = angle,
                    AngularVelocity = (float)(random.NextDouble() * 0.2) - 0.1f // Random angular velocity for rotation
                };

                particles.Add(particle);
            }
        }
        public void CreateExplosion(PointF center, float radius, int particleCount, int particleLife)
        {
            for (int i = 0; i < particleCount; i++)
            {
                float angle = (float)(random.NextDouble() * Math.PI * 2);
                float speed = (float)(random.NextDouble() * 2) + 1;
                float size = (float)(random.NextDouble() * 4) + 1;

                int red = random.Next(128); // Random red value from 0 to 127
                int green = random.Next(128); // Random green value from 0 to 127
                int blue = random.Next(128); // Random blue value from 0 to 127

                Color color = Color.FromArgb(red, green, blue);

                Particle particle = new Particle
                {
                    Position = center,
                    Velocity = new PointF((float)Math.Cos(angle) * speed, (float)Math.Sin(angle) * speed),
                    Size = size,
                    Color = color,
                    Life = particleLife
                };

                particles.Add(particle);
            }
        }
        public void CreateSmoke(PointF center, float radius, int particleCount, int particleLife)
        {
            for (int i = 0; i < particleCount; i++)
            {
                float angle = (float)(random.NextDouble() * Math.PI * 2);
                float speed = (float)(random.NextDouble() * 1) + 0.5f;
                float size = (float)(random.NextDouble() * 8) + 2;

                int alpha = (int)(size / 10 * 255); // Adjust alpha based on size for a 3D effect
                Color color = Color.FromArgb(alpha, 200, 200, 200); // Light gray smoke color

                Particle particle = new Particle
                {
                    Position = center,
                    Velocity = new PointF((float)Math.Cos(angle) * speed, (float)Math.Sin(angle) * speed),
                    Size = size,
                    Color = color,
                    Life =random.Next(0, particleLife),
                    Angle = angle,
                    AngularVelocity = (float)(random.NextDouble() * 0.1) - 0.05f // Random angular velocity for rotation
                };

                particles.Add(particle);
            }
        }
        public void UpdateParticles()
        {
            for (int i = particles.Count - 1; i >= 0; i--)
            {
                Particle particle = particles[i];
                particle.Position = new PointF(particle.Position.X + particle.Velocity.X/1.0f, particle.Position.Y + -MathF.Abs( particle.Velocity.Y));
                particle.Life--;

                if (particle.Life <= 0)
                {
                    particles.RemoveAt(i);
                }
            }
        }

        public void DrawParticles(Graphics graphics)
        {
            foreach (Particle particle in particles)
            {
                Brush brush = new SolidBrush(particle.Color);
                graphics.FillEllipse(brush, particle.Position.X - particle.Size / 2, particle.Position.Y - particle.Size / 2, particle.Size, particle.Size);
                brush.Dispose();
            }
        }
    }

    // Example usage
    public class MainForm : Form
    {
        

        public MainForm()
        {
            
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);


        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            
        }
    }
}
// Create and run the application
