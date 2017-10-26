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


            // Click eventas
            submitButton.Click += (sender, e) =>
            {
                controller.addBarReview(new BarReview(barNameEditText.Text, 
                                            beverageVolumeSeekbar.Progress, 
                                            commentEditText.Text, 
                                            ratingBar.Progress));
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

