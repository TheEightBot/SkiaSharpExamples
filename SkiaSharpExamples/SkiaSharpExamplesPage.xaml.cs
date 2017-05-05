using Xamarin.Forms;

namespace SkiaSharpExamples
{
    public partial class SkiaSharpExamplesPage : ContentPage
    {
        public SkiaSharpExamplesPage()
        {
            InitializeComponent();
        }

        void RadialProgress_Clicked(object sender, System.EventArgs e)
        {
            this.Navigation.PushAsync(new RadialProgressPage());
        }
        
        void RadialProgressOpenGl_Clicked(object sender, System.EventArgs e)
        {
            this.Navigation.PushAsync(new RadialProgressOpenGLPage());
        }
        
        void LinearProgress_Clicked(object sender, System.EventArgs e)
        {
            this.Navigation.PushAsync(new LinearProgressPage());
        }
    }
}
