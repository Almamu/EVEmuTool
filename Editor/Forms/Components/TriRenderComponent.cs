using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using OpenTK.Mathematics;
using OpenTK.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Editor.Forms.Components
{
    public partial class TriRenderComponent : GLControl
    {
        private Trinity.Model mModel;
        private float[] mVertices;
        private uint[] mIndices;
        private int mVertexBufferObject;
        private int mVertexArrayObject;
        private int mElementBufferObject;
        private Timer mRefreshTimer;
        private Render.Shader mShader;
        private Render.Camera mCamera;
        private Vector3 mCenter;
        private float mRotationX;
        private float mRotationY;
        private float mZoom;
        private bool mMouseClicked;
        private bool mStopAutoRotation = false;
        private Vector2 mMousePos;
        private float mZoomIncrements = 1.0f;
        private float mDistance;

        public TriRenderComponent(Trinity.Model model)
        {
            InitializeComponent();

            this.mModel = model;
            this.mRefreshTimer = new Timer
            {
                Interval = 16,
                Enabled = true
            };

            this.mRefreshTimer.Tick += (_, _) => { Invalidate(); };
        }

        private void CalculateBoundingBox()
        {
            // go through all the vertices and find the right position
            Vector3 min = new Vector3(this.mModel.Vertices[0].position.x, this.mModel.Vertices[0].position.y, this.mModel.Vertices[0].position.z);
            Vector3 max = new Vector3(this.mModel.Vertices[0].position.x, this.mModel.Vertices[0].position.y, this.mModel.Vertices[0].position.z);

            foreach (Trinity.Vertex cur in this.mModel.Vertices)
            {
                Vector3 value = new Vector3(cur.position.x, cur.position.y, cur.position.z);

                min = new Vector3(
                    MathF.Min(value.X, min.X),
                    MathF.Min(value.Y, min.Y),
                    MathF.Min(value.Z, min.Z)
                );
                max = new Vector3(
                    MathF.Max(value.X, max.X),
                    MathF.Max(value.Y, max.Y),
                    MathF.Max(value.Z, max.Z)
                );
            }
            
            // get the center of the object
            this.mCenter = (max + min) / 2.0f;
            this.mDistance = (max + min).Length / 2.0f;
            this.mCamera.Position = this.mCenter + new Vector3(this.mDistance);
            this.mZoomIncrements = 0.05f;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // setup depthtest to prevent displaying parts not visible
            GL.Enable(EnableCap.DepthTest);

            // setup the camera
            this.mCamera = new Render.Camera(Vector3.UnitZ * 10, Size.Width / (float)Size.Height);
            this.CalculateBoundingBox();

            // setup buffers and data
            this.mVertices = new float[this.mModel.Vertices.Length * 6]; // includes normals

            for (int i = 0; i < this.mModel.Vertices.Length; i ++)
            {
                this.mVertices[i * 6 + 0] = this.mModel.Vertices[i].position.x;
                this.mVertices[i * 6 + 1] = this.mModel.Vertices[i].position.y;
                this.mVertices[i * 6 + 2] = this.mModel.Vertices[i].position.z;
                this.mVertices[i * 6 + 3] = this.mModel.Vertices[i].normal.x;
                this.mVertices[i * 6 + 4] = this.mModel.Vertices[i].normal.y;
                this.mVertices[i * 6 + 5] = this.mModel.Vertices[i].normal.z;
            }

            // now setup the triangle indices
            this.mIndices = new uint[this.mModel.Surfaces.Sum(x => x.numTriangles) * 6];
            int current = 0;

            foreach (Trinity.Surface surface in this.mModel.Surfaces)
            {
                foreach (Trinity.Triangle triangle in surface.triangles)
                {
                    // vertices
                    this.mIndices[current++] = (uint) triangle.vertex1;
                    this.mIndices[current++] = (uint) triangle.vertex2;
                    this.mIndices[current++] = (uint) triangle.vertex3;

                    // normals
                    this.mIndices[current++] = (uint) triangle.vertex1;
                    this.mIndices[current++] = (uint) triangle.vertex2;
                    this.mIndices[current++] = (uint) triangle.vertex3;
                }
            }

            // create vertex array
            this.mVertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(this.mVertexArrayObject);

            // prepare vertex buffers
            this.mVertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, this.mVertexBufferObject);
            // copy over list of vertices to the GPU
            GL.BufferData(BufferTarget.ArrayBuffer, this.mVertices.Length * sizeof (float), this.mVertices, BufferUsageHint.StaticDraw);

            // setup the shader
            this.mShader = new Render.Shader(
                @"
#version 330 core

layout(location = 0) in vec3 vertexPosition;
layout(location = 1) in vec3 vertexNormal;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

out vec3 Normal;
out vec3 FragPos;

void main(void)
{
    gl_Position = vec4(vertexPosition, 1.0) * model * view * projection;
    FragPos = vec3(model * vec4 (vertexPosition, 1.0));
    Normal = vertexNormal;
}",
                @"
#version 330

out vec4 outputColor;
in vec3 Normal;
in vec3 FragPos;

uniform vec3 lightPosition;
uniform vec3 cameraPosition;

void main()
{
    // TODO: SUPPORT TEXTURE SAMPLING
    vec3 lightColor = vec3(1.0, 1.0, 1.0);
    vec3 objectColor = vec3(0.3, 0.3, 0.3);
    float specularStrength = 0.5;

    vec3 viewDir = normalize (cameraPosition - FragPos);
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize (lightPosition - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);

    float spec = pow(max(dot(viewDir, reflectDir), 0.0), 4);
    float diff = max(dot(norm, lightDir), 0.0);

    vec3 specular = specularStrength * spec * lightColor;
    vec3 diffuse = diff * lightColor;
    vec3 ambient = 0.9 * lightColor;

    outputColor = vec4((ambient + diffuse + specular) * objectColor, 1.0);
}"
            );
            this.mShader.Use();

            // tell opengl how to handle the buffer (only vertices includes for now, we can include texture coordinates too using the stride)
            // check https://github.com/opentk/LearnOpenTK/blob/master/Chapter1/9-Camera/Window.cs#L94
            int vertexPosition = this.mShader.GetAttribLocation("vertexPosition");
            GL.EnableVertexAttribArray(vertexPosition);
            GL.VertexAttribPointer(vertexPosition, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);

            int vertexNormal = this.mShader.GetAttribLocation("vertexNormal");
            GL.EnableVertexAttribArray(vertexNormal);
            GL.VertexAttribPointer(vertexNormal, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));

            // setup indice buffers
            this.mElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.mElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, this.mIndices.Length * sizeof(uint), this.mIndices, BufferUsageHint.StaticDraw);

            // setup variables
            this.mShader.SetVector3("lightPosition", this.mCamera.Position);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            // update camera's viewport (sometimes this gets called before OnLoad)
            if (this.mCamera is null)
                return;

            this.mCamera.AspectRatio = Size.Width / (float)Size.Height;

            // setup viewport
            GL.Viewport(Location, Size);
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            this.mStopAutoRotation = !this.mStopAutoRotation;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            // zoom increments should depend on the model's size to prevent smaller models from zooming in too fast
            this.mZoom += -e.Delta * this.mZoomIncrements;
            this.mCamera.Position = this.mCenter + new Vector3(this.mDistance + this.mZoom);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left)
                this.mMouseClicked = true;

            this.mMousePos = new Vector2(e.X, e.Y);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (e.Button == MouseButtons.Left)
                this.mMouseClicked = false;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (this.mMouseClicked == false)
                return;

            // calculate the difference and rotate
            float xdelta = e.X - this.mMousePos.X;
            float ydelta = e.Y - this.mMousePos.Y;

            // might sound confusing, x and y rotations are around the axis
            this.mRotationY += xdelta * 0.01f;
            this.mRotationX += ydelta * 0.01f;

            this.mMousePos.X = e.X;
            this.mMousePos.Y = e.Y;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            MakeCurrent();
            // clear framebuffer
            GL.ClearColor(Color.FromArgb(255, 0x64, 0x64, 0xff));
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // draw the buffers
            GL.BindVertexArray(this.mVertexArrayObject);

            if (this.mMouseClicked == false && this.mStopAutoRotation == false)
                this.mRotationY += 0.01f;

            // update matrixes
            this.mShader.Use();
            this.mShader.SetVector3("cameraPosition", this.mCamera.Position);
            this.mShader.SetMatrix4(
                "model",
                Matrix4.Identity *
                Matrix4.CreateTranslation(-this.mCenter) *
                Matrix4.CreateRotationX(this.mRotationX) *
                Matrix4.CreateRotationY(this.mRotationY) *
                Matrix4.CreateTranslation(this.mCenter)
            );
            this.mShader.SetMatrix4("view", this.mCamera.GetViewMatrix(this.mCenter));
            this.mShader.SetMatrix4("projection", this.mCamera.GetProjectionMatrix());

            GL.DrawElements(PrimitiveType.Triangles, this.mIndices.Length, DrawElementsType.UnsignedInt, 0);

            SwapBuffers();
        }
    }
}
