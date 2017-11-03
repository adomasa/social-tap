using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;

namespace Socialtap.Code.Fragments
{
    public class ReviewFragment : Fragment
    {
        EditText barNameField;
        EditText commentField;
        Button submitButton;
        SeekBar beverageVolumeBar;
        RatingBar ratingBar;
        TextView beverageVolumeLabel;
        View rootView;

        public static ReviewFragment NewInstance() {
            return new ReviewFragment();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            rootView = inflater.Inflate(Resource.Layout.fragment_review, container, false);

            GetReferencesFromLayout();

            // Paslepia klaviatūrą užkrovus fragmentą
            Activity.Window.SetSoftInputMode(SoftInput.StateHidden);

            // Paspaudus fone paslepia klaviatūrą                
            rootView.Touch += HideKeyboard;

            // Todo: iškelti į kontrolerį, pakeisti switch'ą
            beverageVolumeBar.ProgressChanged += (sender, e) => {
                switch (beverageVolumeBar.Progress)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                        beverageVolumeLabel.Text =
                            GetString(Resource.String.beverage_volume_low);
                        break;
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                        beverageVolumeLabel.Text =
                            GetString(Resource.String.beverage_volume_medium);
                        break;
                    default:
                        beverageVolumeLabel.Text =
                            GetString(Resource.String.beverage_volume_high);
                        break;
                };
            };

            // Click eventas
            // Todo: iškelti į kontrolerį, threads

            submitButton.Click += (sender, e) =>
            {
                MainActivityController.AddBarReview(barNameField.Text, beverageVolumeBar.Progress,
                                   commentField.Text, ratingBar.Progress);
                Toast.MakeText(Application.Context,
                               "Išsaugota", ToastLength.Short).Show();
            };

            return rootView;
        }

        /// Prideda funkcionalių UI objektų nuorodas 
        private void GetReferencesFromLayout()
        {
            barNameField =
                rootView.FindViewById<EditText>(Resource.Id.barNameEditText);
            commentField =
                rootView.FindViewById<EditText>(Resource.Id.commentEditText);
            submitButton =
                rootView.FindViewById<Button>(Resource.Id.submitButton);
            beverageVolumeBar =
                rootView.FindViewById<SeekBar>(Resource.Id.beverageVolumeSeekBar);
            ratingBar =
                rootView.FindViewById<RatingBar>(Resource.Id.ratingBar);
            beverageVolumeLabel =
                rootView.FindViewById<TextView>(Resource.Id.beverageVolumeStatusTextView);
        }


        ///Paslepia klaviatūrą paspaudus fone
        public void HideKeyboard(object sender, View.TouchEventArgs e)
        {
            var imm = (InputMethodManager)Activity.GetSystemService(Context.InputMethodService);
            imm.HideSoftInputFromWindow(rootView.WindowToken, 0);
        }
    }
}
