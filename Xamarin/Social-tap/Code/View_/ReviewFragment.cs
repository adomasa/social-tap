using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Socialtap.Code.Controller;
using Socialtap.Code.Controller.Interfaces;
using Socialtap.Code.Model;

namespace Socialtap.Code.View_.Fragments
{
    public class ReviewFragment : Fragment
    {
        private View _rootView;
        private EditText _barNameField;
        private Button _addPhotoButton;
        private ImageView _beveragePhoto;
        private TextView _beverageVolumeLabel;
        private SeekBar _beverageVolumeBar;
        private EditText _commentField;
        private RatingBar _ratingBar;
        private Button _submitButton;

        const int RequestExternalImage = 0;
        private IMainController _controller;
  

        public static ReviewFragment NewInstance() {
            return new ReviewFragment();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            _controller = MainController.GetInstance(Context);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _rootView = inflater.Inflate(Resource.Layout.fragment_review, container, false);

            GetReferencesFromLayout();

            // Hide keyboard on view created 
            Activity.Window.SetSoftInputMode(SoftInput.StateHidden);

            // Hide keyboard on background tap               
            _rootView.Touch += HideKeyboard;

            _beverageVolumeBar.ProgressChanged += (sender, e) =>
            {
                _beverageVolumeLabel.Text = $"{_beverageVolumeBar.Progress * 10}%";
            };

            _submitButton.Click += SaveReview;
            _addPhotoButton.Click += StartImageSelection;

            return _rootView;
        }
        /// <summary>
        /// Validates the input
        /// </summary>
        /// <returns><c>true</c>, if input valid is valid, <c>false</c> otherwise.</returns>
        /// <param name="progress1">Beverage level</param>
        /// <param name="progress2">Bar rating</param>
        /// <param name="text">Bar title</param>
        private bool IsInputValid(int progress1, int progress2, string text)
        {
            return (progress1 > 0 && progress2 > 0 && text.Length > 0);
        }

        private void SaveReview(object sender, System.EventArgs e)
        {
            if (IsInputValid(_beverageVolumeBar.Progress,
                 _ratingBar.Progress,
                 _barNameField.Text))
            {
                _controller.AddBarReview(new BarReview(

                    _beverageVolumeBar.Progress,
                    _ratingBar.Progress,
                    _barNameField.Text,
                    _commentField.Text));
            }
            else
            {
                Snackbar
                    .Make(_rootView,
                           GetString(Resource.String.invalid_review_format),
                           Snackbar.LengthShort)
                .Show();
            }
        }

        private void StartImageSelection(object sender, System.EventArgs e)
        {
            {
                var imageIntent = new Intent();
                imageIntent.SetAction(Intent.ActionPick);
                imageIntent.SetData(MediaStore.Images.Media.ExternalContentUri);
                StartActivityForResult(imageIntent, RequestExternalImage);
            };
        }
        /// Add references from UI layout
        private void GetReferencesFromLayout()
        {
            _barNameField =
                _rootView.FindViewById<EditText>(Resource.Id.barNameEditText);
            _commentField =
                _rootView.FindViewById<EditText>(Resource.Id.commentEditText);
            _submitButton =
                _rootView.FindViewById<Button>(Resource.Id.submitButton);
            _beverageVolumeBar =
                _rootView.FindViewById<SeekBar>(Resource.Id.beverageVolumeSeekBar);
            _ratingBar =
                _rootView.FindViewById<RatingBar>(Resource.Id.ratingBar);
            _beverageVolumeLabel =
                _rootView.FindViewById<TextView>(Resource.Id.beverageVolumeStatusTextView);
            _addPhotoButton = 
                _rootView.FindViewById<Button>(Resource.Id.addPhotoButton);
            _beveragePhoto = 
                _rootView.FindViewById<ImageView>(Resource.Id.beverageImageView);
        }

        /// Hides the keyboard by default
        private void HideKeyboard(object sender, View.TouchEventArgs e)
        {
            var imm = (InputMethodManager)Activity.GetSystemService(Context.InputMethodService);
            imm.HideSoftInputFromWindow(_rootView.WindowToken, 0);
        }

        /// <summary>
        /// Invoked by _addPhotoButton.Click event
        /// </summary>
        public override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok && requestCode == RequestExternalImage)
            {
                using (var pixelCounter = new PixelCounter(MediaStore.Images.Media.GetBitmap(Activity.ContentResolver, data.Data)))
                {
                    // img processing
                    var percentageOfTargetPixels = pixelCounter
                        .GetPercentageOfTargetPixels();

                    _beveragePhoto.SetImageBitmap(pixelCounter.getProcessedImage());

                    _beverageVolumeBar.Progress = percentageOfTargetPixels / 10;
                    _beverageVolumeLabel.Typeface = Typeface.DefaultBold;
                    _beverageVolumeLabel.Text = $"{percentageOfTargetPixels.ToString()}%";

                    // img processing state window with undo action
                    Snackbar
                        .Make(_rootView, GetString(Resource.String.beverage_volume_updated), Snackbar.LengthLong)
                        .SetAction(GetString(Resource.String.undo), view =>
                        {
                            _beverageVolumeBar.Progress = 0;
                            _beveragePhoto.SetImageURI(null);
                        }).Show();
                }
            }
        }
    }
}
