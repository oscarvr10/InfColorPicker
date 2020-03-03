using Foundation;
using InfColorPickerBinding;
using System;
using UIKit;

namespace InfColorPickerSample
{
    public partial class ViewController : UIViewController
    {
        ColorSelectedDelegate selector;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            ChangeButton.TouchUpInside += HandleTouchUpInsideWithStrongDelegate;   // Option 1: Change background color using strong delegate
            //ChangeButton.TouchUpInside += HandleTouchUpInsideWithWeakDelegate;   // Option 2: Change background color using weak delegate

            selector = new ColorSelectedDelegate(this); //Comment this line and line 21 in order to test it using strong delegate
        }

        [Export("colorPickerControllerDidFinish:")]
        public void ColorPickerControllerDidFinish(InfColorPickerController controller)
        {
            View.BackgroundColor = controller.ResultColor;
            DismissViewController(false, null);
        }

        private void HandleTouchUpInsideWithStrongDelegate(object sender, EventArgs e)
        {
            InfColorPickerController picker = InfColorPickerController.ColorPickerViewController;
            picker.Delegate = selector;
            picker.PresentModallyOverViewController(this);
        }

        private void HandleTouchUpInsideWithWeakDelegate(object sender, EventArgs e)
        {
            InfColorPickerController picker = InfColorPickerController.ColorPickerViewController;
            picker.WeakDelegate = this;
            picker.SourceColor = this.View.BackgroundColor;
            picker.PresentModallyOverViewController(this);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}