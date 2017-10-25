using Android.App;
using Android.Widget;
using Android.OS;

namespace Socialtap
{
    [Activity(Label = "Social-tap", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        EditText barNameEditText;
        EditText commentEditText;
        Button submitButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            barNameEditText = FindViewById<EditText>(Resource.Id.barNameEditText);
            commentEditText = FindViewById<EditText>(Resource.Id.commentEditText);
            submitButton = FindViewById<Button>(Resource.Id.submitButton);

            submitButton.Click += delegate {
                Toast.MakeText(Application.Context, "Submit", ToastLength.Short).Show();
            };

        }
    }
}

