using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Java.Lang;
using Socialtap.Code.Controller;
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
  

        public static ReviewFragment NewInstance() {
            return new ReviewFragment();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _rootView = inflater.Inflate(Resource.Layout.fragment_review, container, false);

            GetReferencesFromLayout();

            // Paslepia klaviatūrą užkrovus fragmentą
            Activity.Window.SetSoftInputMode(SoftInput.StateHidden);

            // Paspaudus fone paslepia klaviatūrą                
            _rootView.Touch += HideKeyboard;

            // Todo: pakeisti switch'ą
            _beverageVolumeBar.ProgressChanged += (sender, e) =>
            {
                switch (_beverageVolumeBar.Progress)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                        _beverageVolumeLabel.Text =
                            GetString(Resource.String.beverage_volume_low);
                        break;
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                        _beverageVolumeLabel.Text =
                            GetString(Resource.String.beverage_volume_medium);
                        break;
                    default:
                        _beverageVolumeLabel.Text =
                            GetString(Resource.String.beverage_volume_high);
                        break;
                }
                ;
            };

            // Click eventai

            _submitButton.Click += (sender, e) =>
            {
                if (IsInputValid(_beverageVolumeBar.Progress, 
                                 _ratingBar.Progress, 
                                 _barNameField.Text)) 
                {
                    MainController.AddBarReview(
                        new BarReview(_beverageVolumeBar.Progress,
                        _ratingBar.Progress, _barNameField.Text,
                        _commentField.Text), (MainActivity)this.Activity);
                }
                else 
                {
                    Snackbar
                        .Make (_rootView, GetString(Resource.String.invalid_review_format), Snackbar.LengthShort)
                    .Show (); 
                }

            };

            _addPhotoButton.Click += (sender, e) =>
            {
                var imageIntent = new Intent ();
                imageIntent.SetAction (Intent.ActionPick);
                imageIntent.SetData(MediaStore.Images.Media.ExternalContentUri);
                StartActivityForResult(imageIntent, 0);
            };

            return _rootView;
        }

        private bool IsInputValid(int progress1, int progress2, string text)
        {
            return (progress1 > 0 && progress2 > 0 && text.Length > 0);
        }

        /// Prideda funkcionalių UI objektų nuorodas 
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

        ///Paslepia klaviatūrą paspaudus fone
        private void HideKeyboard(object sender, View.TouchEventArgs e)
        {
            var imm = (InputMethodManager)Activity.GetSystemService(Context.InputMethodService);
            imm.HideSoftInputFromWindow(_rootView.WindowToken, 0);
        }

        /// <summary>
        ///  Iššaukiamas kai gauna CallBack iš aplikacijos, invokinamas 
        /// _addPhotoButton.Click evento
        /// </summary>
        public override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                PixelCounter pixelCounter = new PixelCounter(MediaStore.Images.Media.GetBitmap(Activity.ContentResolver, data.Data));
                // img apdorojimas
                var percentageOfTargetPixels = pixelCounter
                    .GetPercentageOfTargetPixels();

                //_beveragePhoto.SetImageURI (data.Data);
                _beveragePhoto.SetImageBitmap(pixelCounter.getProcessedImage());

                _beverageVolumeBar.Progress = (int) percentageOfTargetPixels / 10;
                _beverageVolumeLabel.Typeface = Typeface.DefaultBold;
                _beverageVolumeLabel.Text = $"Debuginimui. {percentageOfTargetPixels.ToString()}%";

                // Būsenos su anuliavimo veiksmu pranešimas lango apačioje 
                Snackbar
                    .Make (_rootView, GetString(Resource.String.beverage_volume_updated), Snackbar.LengthLong)
                    .SetAction (GetString(Resource.String.undo), view =>
                    {
                        _beverageVolumeBar.Progress = 0;
                        _beveragePhoto.SetImageURI(null);
                    }).Show (); 
            }

        }
    }
}
