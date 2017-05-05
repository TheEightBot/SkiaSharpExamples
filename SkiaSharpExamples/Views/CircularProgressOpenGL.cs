using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace SkiaSharpExamples
{
    public class CircularProgressOpenGL : SKGLView
    {
        public static BindableProperty StartingDegreesProperty =
            BindableProperty.Create(nameof(StartingDegrees), typeof(float), typeof(CircularProgress), -90f, 
                propertyChanged: (bindable, oldValue, newValue) => (bindable as SKCanvasView)?.InvalidateSurface());

        public float StartingDegrees {
            get { return (float)GetValue(StartingDegreesProperty); }
            set { SetValue(StartingDegreesProperty, value.Clamp(-359.99f, 359.99f)); }
        }

        public static BindableProperty EndingDegreesProperty =
            BindableProperty.Create(nameof(EndingDegrees), typeof(float), typeof(CircularProgress), -90f, 
                propertyChanged: (bindable, oldValue, newValue) => (bindable as SKCanvasView)?.InvalidateSurface());

        public float EndingDegrees {
            get { return (float)GetValue(EndingDegreesProperty); }
            set { SetValue(EndingDegreesProperty, value.Clamp(-359.99f, 359.99f)); }
        }


        public static BindableProperty ProgressThicknessProperty =
            BindableProperty.Create(nameof(ProgressThickness), typeof(float), typeof(CircularProgress), 12f, 
                propertyChanged: (bindable, oldValue, newValue) => (bindable as SKCanvasView)?.InvalidateSurface());

        public float ProgressThickness {
            get { return (float)GetValue(ProgressThicknessProperty); }
            set { SetValue(ProgressThicknessProperty, value); }
        }
        
        public static BindableProperty ProgressColorProperty =
            BindableProperty.Create(nameof(ProgressColor), typeof(Color), typeof(CircularProgress), Color.Default, 
                propertyChanged: (bindable, oldValue, newValue) => (bindable as SKCanvasView)?.InvalidateSurface());

        public Color ProgressColor {
            get { return (Color)GetValue(ProgressColorProperty); }
            set { SetValue(ProgressColorProperty, value); }
        }

        public CircularProgressOpenGL()
        {
            this.BackgroundColor = Xamarin.Forms.Color.White;
            this.InputTransparent = true;
        }
        
        protected override void OnParentSet()
        {
            base.OnParentSet();

            if (Parent != null)
                this.InvalidateSurface();
        }
        
        protected override void OnPaintSurface(SKPaintGLSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);

            var canvas = e.Surface.Canvas;
            
            var size = Math.Min(e.RenderTarget.Width, e.RenderTarget.Height) - ProgressThickness;

            using (var paint = new SKPaint())
            using (var path = new SKPath()) {
                var left = (e.RenderTarget.Width - size) / 2f;
                var top = (e.RenderTarget.Height - size) / 2f;
                var right = left + size;
                var bottom = top + size;

                path.AddArc(new SKRect(left, top, right, bottom), StartingDegrees, EndingDegrees);

                paint.IsAntialias = true;
                paint.StrokeCap = SKStrokeCap.Round;
                paint.Style = SKPaintStyle.Stroke;
                paint.Color = ProgressColor.ToSKColor();
                paint.StrokeWidth = ProgressThickness;
                
                paint.PathEffect = SKPathEffect.CreateDiscrete(12f, 4f, (uint)Guid.NewGuid().GetHashCode());
                
                canvas.Clear(Color.White.ToSKColor());
                canvas.DrawPath(path, paint);
            }
        }
    }
}
