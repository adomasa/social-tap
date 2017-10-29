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
        EditText barNameField;
        EditText commentField;
        Button submitButton;
        SeekBar beverageVolumeBar;
        RatingBar ratingBar;
        TextView beverageVolumeLabel;
        MainActivityController controller;

        /// Lango inicializacija
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            controller = MainActivityController.Instance;

            if (savedInstanceState != null) {
                controller.ExtractBarsDataFromMemory(savedInstanceState);
            }

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            GetReferencesFromLayout();

            // Click eventas
            // Todo: iškelti į kontrolerį
            submitButton.Click += (sender, e) =>
            {
                //async
                BarReview barReview = 
                    new BarReview(barNameField.Text, beverageVolumeBar.Progress,
                                  commentField.Text, ratingBar.Progress);

                controller.addBarReview(barReview);
                Toast.MakeText(Application.Context,
                               "Išsaugota", ToastLength.Short).Show();
            };

            // Todo: iškelti į kontrolerį, pakeisti switch'ą
            beverageVolumeBar.ProgressChanged += (sender, e) => {
                switch (beverageVolumeBar.Progress) {
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
        }

        ///Paslepia klaviatūrą paspaudus kitur nei EditText laukas
        public override bool OnTouchEvent(MotionEvent e)
        {
            InputMethodManager imm = 
                (InputMethodManager)GetSystemService(Context.InputMethodService);
            // Veikia su abiem EditText laukais
            imm.HideSoftInputFromWindow(barNameField.WindowToken, 0);
            return base.OnTouchEvent(e);
        }
        /// Prideda funkcionalių UI objektų nuorodas 
        private void GetReferencesFromLayout() {
            barNameField = 
                FindViewById<EditText>(Resource.Id.barNameEditText);
            commentField = 
                FindViewById<EditText>(Resource.Id.commentEditText);
            submitButton = 
                FindViewById<Button>(Resource.Id.submitButton);
            beverageVolumeBar = 
                FindViewById<SeekBar>(Resource.Id.beverageVolumeSeekBar);
            ratingBar = 
                FindViewById<RatingBar>(Resource.Id.ratingBar);
            beverageVolumeLabel = 
                FindViewById<TextView>(Resource.Id.beverageVolumeStatusTextView);
        }
    }
}

