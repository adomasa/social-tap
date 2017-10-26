using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Android.Views.InputMethods;
using Android.Content;

namespace Socialtap
{
    [Activity(Label = "Social-tap", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        EditText barNameEditText;
        EditText commentEditText;
        Button submitButton;
        SeekBar beverageVolumeSeekbar;
        RatingBar ratingBar;
        MainActivityController controller;
        TextView beverageVolumeLabel;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            controller = MainActivityController.Instance;

            if (savedInstanceState != null) {
                controller.ExtractBarsDataFromMemory(savedInstanceState);
            }

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);


            // Prideda funkcionalių UI objektų nuorodas
            barNameEditText = FindViewById<EditText>(Resource.Id.barNameEditText);
            commentEditText = FindViewById<EditText>(Resource.Id.commentEditText);
            submitButton = FindViewById<Button>(Resource.Id.submitButton);
            beverageVolumeSeekbar = FindViewById<SeekBar>(Resource.Id.beverageVolumeSeekBar);
            ratingBar = FindViewById<RatingBar>(Resource.Id.ratingBar);
            beverageVolumeLabel = FindViewById<TextView>(Resource.Id.beverageVolumeStatusTextView);


            // Click eventas
            // Todo: iškelti į kontrolerį
            submitButton.Click += (sender, e) =>
            {
                controller.addBarReview(new BarReview(barNameEditText.Text, 
                                            beverageVolumeSeekbar.Progress, 
                                            commentEditText.Text, 
                                            ratingBar.Progress));
                Toast.MakeText(Application.Context, 
                               "Išsaugota", ToastLength.Short).Show();
            };

            // Todo: iškelti į kontrolerį, pakeisti switch'ą
            beverageVolumeSeekbar.ProgressChanged += (sender, e) => {
                switch (beverageVolumeSeekbar.Progress) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        beverageVolumeLabel.Text =
                            GetString(Resource.String.beverage_volume_low);
                        break;
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
        }

        //Paslepia klaviatūrą paspaudus kitur nei EditText laukas
        public override bool OnTouchEvent(MotionEvent e)
        {
            InputMethodManager imm =
                (InputMethodManager)GetSystemService(Context.InputMethodService);
            // Veikia su abiem EditText laukais
            imm.HideSoftInputFromWindow(barNameEditText.WindowToken, 0);
            return base.OnTouchEvent(e);
        }
    }
}

