using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Socialtap.Code.Controller;
using Socialtap.Code.Model;

namespace Socialtap.Code.View_.Fragments
{
    public class ReviewFragment : Fragment
    {
        private EditText _barNameField;
        private EditText _commentField;
        private Button _submitButton;
        private SeekBar _beverageVolumeBar;
        private RatingBar _ratingBar;
        private TextView _beverageVolumeLabel;
        private View _rootView;

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

            // Todo: iškelti į kontrolerį, pakeisti switch'ą
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

            // Click eventas
            // Todo: iškelti į kontrolerį, threads

            _submitButton.Click += (sender, e) =>
            {
                MainActivityController.AddBarReview(
                    new BarReview(_beverageVolumeBar.Progress, 
                                  _ratingBar.Progress, _barNameField.Text,
                                  _commentField.Text));
            };

            return _rootView;
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
        }

        ///Paslepia klaviatūrą paspaudus fone
        private void HideKeyboard(object sender, View.TouchEventArgs e)
        {
            var imm = (InputMethodManager)Activity.GetSystemService(Context.InputMethodService);
            imm.HideSoftInputFromWindow(_rootView.WindowToken, 0);
        }
    }
}
