using Xamarin.Forms;

namespace HxForms.Views
{
    public class Image: Xamarin.Forms.Image
    {
        public static readonly BindableProperty AspectRatioProperty 
            = BindableProperty.Create("AspectRatio", typeof(double), typeof(Image), double.MinValue, 
                BindingMode.OneWay, null, AspectRatioChanged);

        public static readonly BindableProperty OrientationProperty 
            = BindableProperty.Create("Orientation", typeof(AspectRatioOrientation), typeof(Image), 
                AspectRatioOrientation.Horizontal, BindingMode.OneWay, null, AspectRatioOrientationChanged);

        public static void AspectRatioChanged(BindableObject bindableObject, object oldValue, object newValue)
        {
            
        }

        public static void AspectRatioOrientationChanged(BindableObject bindableObject, object oldValue,
            object newValue)
        {
            
        }

        public double AspectRatio
        {
            get => (double) GetValue(AspectRatioProperty);
            set
            {
                SetValue(AspectRatioProperty, value);
                OnPropertyChanged(nameof(AspectRatio));
            }
        }

        public AspectRatioOrientation Orientation
        {
            get => (AspectRatioOrientation) GetValue(OrientationProperty);
            set
            {
                SetValue(OrientationProperty, value);
                OnPropertyChanged(nameof(Orientation));
            }
        }

        public Image(): base()
        {
            
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            if (AspectRatio == double.MinValue)
            {
                return base.OnMeasure(widthConstraint, heightConstraint);
            }
            else if (Orientation == AspectRatioOrientation.Horizontal)
            {
                return new SizeRequest(new Size(widthConstraint, widthConstraint * AspectRatio));
            }
            else
            {
                return new SizeRequest(new Size(heightConstraint * AspectRatio, heightConstraint));
            }
        }
    }
}